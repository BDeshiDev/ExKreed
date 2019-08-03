using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattler : Battler
{
    public BattleCommand testCommand;
    public CommandHolder chosenCommand;
    public override CommandHolder curCommand => chosenCommand;

    public override IEnumerator executeTurn()
    {
        List<Tile> possibleTargetTiles = new List<Tile>();
        testCommand.rangePattern.selectTargets(targeter.grid.tiles, possibleTargetTiles,curTile.x,curTile.y);
        chosenCommand.init(testCommand,this,possibleTargetTiles[Random.Range(0,possibleTargetTiles.Count)]);
        yield return StartCoroutine(testCommand.execute(chosenCommand.user,chosenCommand.target));

        delay = curCommand.command.calcDelay(this);
    }
}
