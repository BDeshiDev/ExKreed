using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CommandHolder
{
    public BattleCommand command;
    public Battler user;
    public Tile target;

    public CommandHolder(BattleCommand command, Battler user, Tile target)
    {
        init(command, user, target);
    }

    public void init(BattleCommand command, Battler user, Tile target)
    {
        this.command = command;
        this.user = user;
        this.target = target;
    }

    bool isvalid()
    {
        return command != null && user != null && target != null;
    }
}
