using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattler : Battler
{
    public List<AttackCommand> commands;
    public MoveCommand moveCommand;
    public WaitCommand waitCommand;
    public CommandHolder chosenCommand;
    public Tile selectedTile;
    public override CommandHolder curCommand => chosenCommand;

    public override IEnumerator executeTurn()
    {
        BattleCommand curCommand = pickAttack();
        if (curCommand == null)//can't attack
        {
            selectedTile = pickRandomMoveTile();
            if (selectedTile == null)
                curCommand = waitCommand;
            else
                curCommand = moveCommand;
        }

        List<Tile> possibleTargetTiles = new List<Tile>();
        curCommand.rangePattern.selectTargets(targeter.grid.tiles, possibleTargetTiles,curTile.x,curTile.y);
        
        for(int i = possibleTargetTiles.Count -1; i >=0 ;i--)
        {
            if (curCommand.isValidTargetTile(this, possibleTargetTiles[i]))
                possibleTargetTiles[i].setTileState(TileState.range);
        }

        Debug.Log("selected " + selectedTile.gameObject);
        yield return new WaitForSeconds(.5f);
        foreach (var possibleTargetTile in possibleTargetTiles)
        {
            possibleTargetTile.setTileState(TileState.normal);
        }
        chosenCommand.init(curCommand, this,selectedTile);

        possibleTargetTiles.Clear();
        possibleTargetTiles.Add(selectedTile);
        chosenCommand.command.damagePattern.selectTargets(targeter.grid.tiles, possibleTargetTiles,selectedTile.x,selectedTile.y);
        foreach (var possibleTargetTile in possibleTargetTiles)
        {
            possibleTargetTile.setTileState(TileState.target);
            Debug.Log("target" + possibleTargetTile.gameObject);
        }

        yield return StartCoroutine(chosenCommand.command.playFX(possibleTargetTiles));

        yield return StartCoroutine(curCommand.execute(chosenCommand.user,chosenCommand.target));
        foreach (var possibleTargetTile in possibleTargetTiles)
        {
            possibleTargetTile.setTileState(TileState.normal);
        }
        yield return new WaitForSeconds(.1f);
        stats.increaseStrain(chosenCommand.command.strainBoost);
        delay = chosenCommand.command.calcDelay(this);
    }

    public override string BattleTag => "Enemy";

    public BattleCommand pickAttack()
    {
        int maxDamageScore = 0;
        BattleCommand result = null;
        List<Tile> tilesInRange = new List<Tile>();
        List<Tile> damagableTiles = new List<Tile>();
        foreach (var battleCommand in commands)
        {
            battleCommand.rangePattern.selectTargets(targeter.grid.tiles,tilesInRange,curTile.x, curTile.y);
            foreach (var tile in tilesInRange)
            {
                damagableTiles.Clear();
                battleCommand.damagePattern.selectTargets(targeter.grid.tiles,damagableTiles,tile.x,tile.y);
                int damageScore = 0;
                foreach (var damagableTile in damagableTiles)
                {
                    if (damagableTile.occupant != null)
                    {
                        if (damagableTile.occupant.BattleTag != this.BattleTag)
                        {
                            damageScore += damagableTile.occupant.calcDamage(this.stats.attack + battleCommand.damageBonus);
                            if (damageScore > maxDamageScore)
                            {
                                maxDamageScore = damageScore;
                                result = battleCommand;
                                selectedTile = damagableTile;
                            }
                        }
                        else
                        {
                            damageScore -= damagableTile.occupant.calcDamage(this.stats.attack + battleCommand.damageBonus);
                        }
                    }

                }
            }
        }
        return result;
    }

    public Tile pickRandomMoveTile()
    {
        List<Tile> moveableTiles = new List<Tile>();
        moveCommand.rangePattern.selectTargets(targeter.grid.tiles,moveableTiles,curTile.x,curTile.y);
        for (int i = moveableTiles.Count - 1; i >= 0; i--)
        {
            if (moveCommand.isValidTargetTile(this, moveableTiles[i]))
                moveableTiles[i].setTileState(TileState.range);
            else
            {
                moveableTiles.Remove(moveableTiles[i]);
            }
        }
        return moveableTiles.Count > 0 ? moveableTiles[Random.Range(0, moveableTiles.Count)] : null;
    }

    public override void init()
    {
        stats.init();
    }

    public override bool isDead()
    {
        return stats.curHp <= 0;
    }
}

