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
    public int strainRecovThreshold = 2;
    public int strainAccumulator = 0;

    public GameObject nextSceneLoader;
    public GameObject failScreenLoader;

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
        }
        battlers.Sort();
        StartCoroutine(battleLoop());
    }

    public IEnumerator battleLoop()
    {
        while (battlers.Count > 0)
        {
            battlers.Sort();
            int delayRecoveryAmount = battlers[0].delay;
            Debug.Log("extracted delay " + delayRecoveryAmount);
            foreach (var battler in battlers)
            {
                battler.delay -= delayRecoveryAmount;
                strainAccumulator += delayRecoveryAmount;
                battler.recoverStrain(strainAccumulator / strainRecovThreshold);
                strainAccumulator = strainAccumulator % strainRecovThreshold;
            }

            for (int i = 0; i < turnPanelHolder.childCount; i++)
            {
                Destroy(turnPanelHolder.GetChild(i).gameObject);
            }

            foreach (var battler in battlers)
            {
                TurnPanel panel = Instantiate(turnPanelPrefab, turnPanelHolder);
                panel.updateVal(battler);
            }

            yield return StartCoroutine(battlers[0].executeTurn());

            bool allPLayerDead = true;
            foreach (var playerPartyBattler in playerParty.battlers)
            {
                if (!playerPartyBattler.isDead())
                {
                    allPLayerDead = false;
                }
                else
                {
                    playerPartyBattler.setState(PlayerCharState.dead);
                }
            }

            if (allPLayerDead)
            {
                Debug.Log("Player Loses");
                failScreenLoader.SetActive(true);
                break;
            }else
            {
                bool allEnemiesDead = true;
                for (int i = enemies.Count - 1; i >= 0; i--)
                {
                    var enemyBattler = enemies[i];
                    if (!enemyBattler.isDead())
                    {
                        allEnemiesDead = false;
                    }
                    else if (battlers.Contains(enemyBattler))
                    {
                        enemyBattler.showDead();
                        battlers.Remove(enemyBattler);
                    }
                }

                if (allEnemiesDead)
                {
                    Debug.Log("Player wins");
                    nextSceneLoader.SetActive(true);
                    break;
                }
            }
        }
    }
}
