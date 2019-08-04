using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "new strain recovery attack")]
public class StrainRecoveryAttack : BattleCommand
{
    public int healAmount = 50;

    public override IEnumerator execute(Battler user, Tile target)
    {
        Debug.Log(title + "was executed by" + user.title);
        List<Tile> targets = new List<Tile>();
        targets.Add(target);
        damagePattern.selectTargets(user.targeter.grid.tiles, targets, user, target.x, target.y);

        foreach (var tile in targets)
        {
            if (tile.occupant != null)
                tile.occupant.recoverStrain(healAmount);
        }
        yield return null;
    }

    public override bool isValidTargetTile(Battler user, Tile targets)
    {
        return true;//attacking any tile is allowed... for now
    }
}