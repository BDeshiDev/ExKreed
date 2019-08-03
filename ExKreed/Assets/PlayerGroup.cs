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

    public override CommandHolder curCommand => curBattler == null? null: curBattler.chosenCommand;
    public List<Tile> rangePreviewList;

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
        Debug.Log("panels created");
        panelUI = Instantiate(panelPrefab, panelParent);
    }

    public void confirmTurn()
    {
        if(curBattler != null && curBattler.chosenCommand != null)
        {
            hasConfirmedTurn = true;
        }
    }


    public override IEnumerator executeTurn()
    {
        panelUI.gameObject.SetActive(true);
        hasConfirmedTurn = false;
        shiftBattler(0);

        while (curCommand.command == null)
            yield return null;
        panelUI.gameObject.SetActive(false);
        rangePreviewList.Clear();
        curBattler.chosenCommand.command.rangePattern.selectTargets(targeter.grid.tiles,rangePreviewList,curBattler.curTile.x , curBattler.curTile.y);
        foreach (var tile in rangePreviewList)
        {
            tile.setTileState(TileState.range);
        }
        yield return StartCoroutine(targeter.getTargets(curBattler.chosenCommand));

        while (!hasConfirmedTurn)
            yield return null;
        delay = curCommand.command.calcDelay(curBattler);
        foreach (var tile in targeter.targetPreviewList)
        {
            tile.setTileState(TileState.target);
        }
        yield return StartCoroutine(curBattler.executeTurn());
        foreach (var tile in rangePreviewList)
        {
            tile.setTileState(TileState.normal);
        }
        foreach (var tile in targeter.targetPreviewList)
        {
            tile.setTileState(TileState.normal);
        }

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
