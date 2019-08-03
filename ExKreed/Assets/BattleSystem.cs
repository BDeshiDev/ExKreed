using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public List<Battler> battlers;

    private void Awake()
    {
        StartCoroutine(battleLoop());
    }

    public IEnumerator battleLoop()
    {
        Debug.Log("deciding");
        foreach (var battler in battlers)
        {
            yield return StartCoroutine(battler.decideTurn());
        }
        battlers.Sort();
        Debug.Log("executing");
        foreach (var battler in battlers)
        {
            Debug.Log(battler.title + "'s turn");
            yield return  StartCoroutine( battler.executeTurn());
        }
    }
}
