using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CommandHolder
{
    public BattleCommand command;
    public Battler user;
    public List<Battler> target;

    public CommandHolder(BattleCommand command, Battler user, List<Battler> target)
    {
        init(command, user, target);
    }

    public void init(BattleCommand command, Battler user, List<Battler> target)
    {
        this.command = command;
        this.user = user;
        this.target = target;
    }
}
