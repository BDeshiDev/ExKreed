using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattler : Battler
{
    public BattlePanelUI panelPrefab;
    public BattlePanelUI panelUI;
    public List<BattleCommand> commands;
    public Transform panelParent;

    private void Awake()
    {
        panelUI = Instantiate(panelPrefab, panelParent);
        panelUI.createButtonList(this,commands);
    }

    public void chooseTurn(BattleCommand command)
    {
        chosenCommand = command;
        Debug.Log(chosenCommand.title + "selected");
    }

    public override IEnumerator decideTurn()
    {   
        
        while (chosenCommand == null)
        {
            yield return null;
        }
        Debug.Log(chosenCommand.title + "chosen");
    }
}
