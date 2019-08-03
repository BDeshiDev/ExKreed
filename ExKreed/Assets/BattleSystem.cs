using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public List<Battler> battlers;
    public bool isOver = false;

    private void Start()
    {
        StartCoroutine(battleLoop());
    }

    public IEnumerator battleLoop()
    {
        while (!isOver)
        {
            foreach (var battler in battlers)
            {
                yield return StartCoroutine(battler.executeTurn());
            }
        }
    }
}
