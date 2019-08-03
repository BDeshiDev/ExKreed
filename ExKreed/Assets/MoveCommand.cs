using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "move command")]
public class MoveCommand : BattleCommand
{
    public override IEnumerator execute(Battler user, Tile target)
    {
        user.curTile.removeOccupant();
        target.placeOccupant(user);
        yield return null;
    }

    public override bool isValidTargetTile(Battler user, Tile target)
    {
        return target.occupant == null;
    }
}
