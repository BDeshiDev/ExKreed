using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "new attack command")]
public class AttackCommand : BattleCommand
{

    public override IEnumerator execute(Battler user, Tile target)
    {
        Debug.Log(title + "was executed");
        yield return null;
    }
}
