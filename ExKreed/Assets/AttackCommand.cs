using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "new attack command")]
public class AttackCommand : BattleCommand
{
    public int damageBonus = 10;
    public override IEnumerator execute(Battler user, Tile target)
    {
        Debug.Log(title + "was executed by" + user.title);
        List<Tile> targets = new List<Tile>();
        targets.Add(target);
        damagePattern.selectTargets(user.targeter.grid.tiles,targets,target.x, target.y);
        foreach (var tile in targets)
        {
            if(tile.occupant != null)
                tile.occupant.takeDamage(damageBonus + user.stats.attack);
        }
        yield return null;
    }

    public override bool isValidTargetTile(Battler user, Tile targets)
    {
        return true;//attacking any tile is allowed... for now
    }
}
