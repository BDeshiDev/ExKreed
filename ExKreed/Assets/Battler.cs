using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Battler : MonoBehaviour,IComparable<Battler>
{
    public int speed = 5;
    public int delay = 1;
    public string title = "NONE";
    public BattleCommand chosenCommand;
    public BattleCommand testCommand;
    private Stats stats;
    public abstract IEnumerator decideTurn();

    public IEnumerator executeTurn()
    {
        if (chosenCommand != null)
            yield return StartCoroutine(testCommand.execute());
        chosenCommand = null;
    }

    public void takeDamage(int damage)
    {
        stats.curHp = Mathf.Max(0,stats.curHp - (damage > stats.defence? damage - stats.defence : 0 ))
    }

    public int CompareTo(Battler other)
    {
        if ((delay - speed) != (other.delay - other.speed))
            return (delay - speed) - (other.delay - other.speed);
        return other.speed - speed;
    }
}
