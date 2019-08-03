using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Stats : MonoBehaviour
{
    public int attack;
    public int defence;
    public int maxHp;
    public int curHp { get; private set; }

    private void Awake()
    {
        curHp = maxHp;
    }
}
