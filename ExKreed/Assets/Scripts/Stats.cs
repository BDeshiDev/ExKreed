using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Stats
{
    public int attack = 15;
    public int defence = 10;
    public int maxHp=30;
    public int strain = 0;
    public static int maxStrain = 100;
    public bool isOverStrained = false;
    public int curHp { get; private set; }

    public event Action<Stats> onHealthChangeEvent;
    public event Action<int> onStrainChangeEvent;

    public void init()
    {
        curHp = maxHp;
        onHealthChangeEvent?.Invoke(this);
    }

    public void increaseStrain(int amount)
    {
        strain = Mathf.Clamp(strain + amount,0,maxStrain);
        onStrainChangeEvent?.Invoke(strain);
        if (strain >= maxStrain)
            isOverStrained = true;
    }

    public int calcDamage(int amount)
    {
        return Mathf.Max(0, curHp - (amount > defence ? (amount - defence) : 0));
    }

    public void takeDamage(int amount)
    {
        curHp = calcDamage(amount);
        onHealthChangeEvent?.Invoke(this);
    }
}
