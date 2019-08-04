using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wait command")]
public class WaitCommand : BattleCommand
{
    public override IEnumerator execute(Battler user, Tile targets)
    {
        yield return null;
    }

    public override bool isValidTargetTile(Battler user, Tile targets)
    {
        return true;
    }
}
