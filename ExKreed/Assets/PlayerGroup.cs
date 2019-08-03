using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroup : BattleGroup<PlayerBattler>
{
    public BattlePanelUI panelPrefab;
    public BattlePanelUI panelUI;
    public Transform panelParent;

    
    public int curBattlerIndex = 0;
    public bool canChangebattler = true;
    public bool hasConfirmedTurn = false;

    public PlayerBattler curBattler;

    public override CommandHolder curCommand => chosenCommand;
    public CommandHolder chosenCommand;

    public void shiftBattler(int offset = 1)
    {
        if (battlers.Count > 0)
        {
            if (curBattler != null)
            {//clear existing panel
                for (int i = 0; i < panelUI.transform.childCount; i++)
                {
                    Destroy(panelUI.transform.GetChild(i).gameObject);
                }
            }

            curBattlerIndex = (curBattlerIndex + offset + battlers.Count) % battlers.Count;
            curBattler = battlers[curBattlerIndex];
            panelUI.createButtonList(curBattler,curBattler.commands);
        }
    }

    private void Awake()
    {
        panelUI = Instantiate(panelPrefab, panelParent);
    }

    public void confirmTurn()
    {
        if(curBattler != null && curBattler.chosenCommand != null)
        {
            hasConfirmedTurn = true;
            chosenCommand = curBattler.chosenCommand;
        }
    }


    public override IEnumerator executeTurn()
    {
        hasConfirmedTurn = false;
        shiftBattler(0);
        while (!hasConfirmedTurn)
            yield return null;
        delay = curCommand.command.calcDelay(curBattler);
        yield return StartCoroutine(curBattler.executeTurn());
    }

    private void Update()
    {
        if (canChangebattler)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                shiftBattler(-1);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                shiftBattler(1);
            }
        }
    }
}
