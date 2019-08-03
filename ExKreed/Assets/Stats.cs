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

    private void Awake()
    {
        curHp = maxHp;
    }

    public void takeDamage(int amount)
    {
        curHp = Mathf.Max(0, curHp - (amount > defence ? (amount - defence) : 0));
    }
}
