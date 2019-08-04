using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public List<Battler> battlers;
    public PlayerGroup playerParty;
    public List<EnemyBattler> enemies;
    
    public bool isOver = false;

    public Transform turnPanelHolder;
    public TurnPanel turnPanelPrefab;

    private void Start()
    {
        battlers.Add(playerParty);
        foreach (var enemyBattler in enemies)
        {
            battlers.Add(enemyBattler);
        }
        foreach (var battler in battlers)
        {
            battler.init();
            TurnPanel panel = Instantiate(turnPanelPrefab, turnPanelHolder);
            panel.updateVal(battler);
        }
        StartCoroutine(battleLoop());
    }

    public IEnumerator battleLoop()
    {
        while (battlers.Count > 0)
        {
            int delayRecoveryAmount = battlers[0].delay;
            Debug.Log("recovery amount " + delayRecoveryAmount);
            foreach (var battler in battlers)
            {
                battler.delay -= delayRecoveryAmount;
                battler.recoverStrain(delayRecoveryAmount);
            }
            yield return StartCoroutine(battlers[0].executeTurn());

            battlers.Sort();

            for (int i = 0; i < turnPanelHolder.childCount; i++)
            {
                Destroy(turnPanelHolder.GetChild(i).gameObject);
            }

            foreach (var battler in battlers)
            {
                TurnPanel panel = Instantiate(turnPanelPrefab, turnPanelHolder);
                panel.updateVal(battler);
            }


            if (playerParty.isDead())
            {
                Debug.Log("Player loses");
                break;
            }
            else
            {
                bool allEnemiesDead = true;
                foreach (var enemyBattler in enemies)
                {
                    if (!enemyBattler.isDead())
                    {
                        allEnemiesDead = false;
                        break;
                    }
                }

                if (allEnemiesDead)
                {
                    Debug.Log("Player wins");
                    break;
                }
            }
        }
    }
}
