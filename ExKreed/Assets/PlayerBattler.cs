using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattler : Battler
{
    public List<BattleCommand> commands;
    public CommandHolder chosenCommand;
    public override CommandHolder curCommand => chosenCommand;

    public void chooseTurn(BattleCommand command)
    {
        chosenCommand.init(command, this, null);
        Debug.Log(chosenCommand.command.title + "selected by " + chosenCommand.user.title );
    }


    public override IEnumerator executeTurn()
    {
        if (chosenCommand != null)
            yield return StartCoroutine(chosenCommand.command.execute(chosenCommand.user,chosenCommand.target));
        chosenCommand.command = null;
    }
}
