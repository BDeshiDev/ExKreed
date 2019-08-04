using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroup : BattleGroup<PlayerBattler>
{
    public BattlePanelUI panelPrefab;
    public BattlePanelUI panelUI;
    public Transform panelParent;
    public Transform statsPanelHolder;
    public StatsPanel statsPanelPrefab;

    public bool hasCancelled = false;
    public int curBattlerIndex = 0;
    public bool canChangebattler = true;
    public bool hasConfirmedTurn = false;
    public GameObject cancelButton;
    public GameObject confirmButton;
    public SkillPreview skillPreviewer;

    public PlayerBattler curBattler;

    public override CommandHolder curCommand => curBattler == null? null: curBattler.chosenCommand;
    public List<Tile> rangePreviewList;

    public override void recoverStrain(int amount)
    {
        foreach (var battler in battlers)
        {
            battler.recoverStrain(amount);
        }
    }

    public void shiftBattler(int offset = 1)
    {
        if (battlers.Count > 0)
        {
            if (curBattler != null)
            {//clear existing panel
                for (int i = 0; i < panelUI.buttonParent.childCount; i++)
                {
                    Destroy(panelUI.buttonParent.transform.GetChild(i).gameObject);
                }
            }

            curBattlerIndex = (curBattlerIndex + offset + battlers.Count) % battlers.Count;
            curBattler = battlers[curBattlerIndex];

            if (!curBattler.stats.canTakeTurn)
            {
                for (int i = 1; i <= battlers.Count; i++)
                {
                    int index = (curBattlerIndex + (offset >= 0 ? i : -i) + battlers.Count) % battlers.Count;
                    if (battlers[index].canTakeTurn)
                    {
                        curBattlerIndex = index;
                        curBattler = battlers[curBattlerIndex];
                        break;
                    }
                }
            }

            panelUI.createButtonList(curBattler,curBattler.commands);
        }
    }

    private void Awake()
    {
        Debug.Log("panels created");
        panelUI = Instantiate(panelPrefab, panelParent);
        foreach (var battler in battlers)
        {
            Instantiate(statsPanelPrefab,statsPanelHolder).init(battler);
        }
    }

    public void confirmTurn()
    {
        if(curBattler != null && curBattler.chosenCommand != null)
        {
            hasConfirmedTurn = true;
        }
    }

    public void cancelTurn()
    {
        hasCancelled = true;
    }


    public override IEnumerator executeTurn()
    {
        hasConfirmedTurn = false;
        canChangebattler = true;
        while (!hasConfirmedTurn)
        {
            hasCancelled = false;
            canChangebattler = true;
            panelUI.gameObject.SetActive(true);
            cancelButton.gameObject.SetActive(false);
            confirmButton.gameObject.SetActive(false);
            hasConfirmedTurn = false;
            shiftBattler(0);

            while (curCommand.command == null)
                yield return null;
            canChangebattler = false;

            panelUI.gameObject.SetActive(false);

            skillPreviewer.gameObject.SetActive(true);
            skillPreviewer.skillNameText.text = curBattler.chosenCommand.command.title;
            delay = curBattler.chosenCommand.command.calcDelay(curBattler);
            skillPreviewer.expectedDelayText.text = "Time Till Next Turn: " + delay;

            rangePreviewList.Clear();
            curBattler.chosenCommand.command.rangePattern.selectTargets(targeter.grid.tiles, rangePreviewList,curBattler,
                curBattler.curTile.x, curBattler.curTile.y);

            foreach (var tile in rangePreviewList)
            {
                if (curBattler.chosenCommand.command.isValidTargetTile(curBattler, tile))
                    tile.setTileState(TileState.range);
            }

            yield return StartCoroutine(targeter.getTargets(curBattler.chosenCommand));

            cancelButton.gameObject.SetActive(true);
            confirmButton.gameObject.SetActive(true);


            while (!hasConfirmedTurn)
            {
                if (hasCancelled)
                {
                    Debug.Log("cancel");
                    foreach (var tile in rangePreviewList)
                    {
                        tile.setTileState(TileState.normal);
                    }
                    foreach (var tile in targeter.targetPreviewList)
                    {
                        tile.setTileState(TileState.normal);
                    }

                    curCommand.command = null;
                    break;
                }
                yield return null;
            }
            skillPreviewer.gameObject.SetActive(false);
        }

        var command = curCommand.command;
        foreach (var tile in targeter.targetPreviewList)
        {
            tile.setTileState(TileState.target);
        }
        yield return StartCoroutine(curBattler.chosenCommand.command.playFX(targeter.targetPreviewList));
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

    public override string BattleTag => "Player";

    public void handleDead()
    {
        foreach (var playerBattler in battlers)
        {
            if(playerBattler.isDead())
                playerBattler.setState(PlayerCharState.dead);
        }
    }

    public override void init()
    {
        foreach (var playerBattler in battlers)
        {
            playerBattler.init();
        }
    }

    public override bool isDead()
    {
        foreach (var playerBattler in battlers)
        {
            if (!playerBattler.isDead())
                return false;
        }

        return true;
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
