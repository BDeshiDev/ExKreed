using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Battler : MonoBehaviour,IComparable<Battler>
{
    public int delay = 1;
    public string title = "NONE";
    public abstract CommandHolder curCommand { get; }
    public Stats stats;
    public abstract IEnumerator executeTurn();
    public TargettingSystem targeter;
    public Tile curTile;

    public bool canTakeTurn => stats.curHp > 0 && !stats.isOverStrained;

    public abstract void init();
    public void takeDamage(int damage)
    {
        stats.takeDamage(damage);
    }


    public int CompareTo(Battler other)
    {
        return this.delay - other.delay;
    }
}
