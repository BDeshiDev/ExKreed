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
        chosenCommand.init(testCommand,this,null);
        yield return StartCoroutine(testCommand.execute(chosenCommand.user,chosenCommand.target));

        delay = curCommand.command.calcDelay(this);
    }
}
