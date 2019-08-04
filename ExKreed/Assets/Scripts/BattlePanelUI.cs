using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattlePanelUI : MonoBehaviour
{
    public ButtonHolder buttonPrefab;
    public Transform buttonParent;
    public TextMeshProUGUI titleText;
    public void createButtonList(PlayerBattler pb,List<BattleCommand> commandsList)
    {
        titleText.text = pb.title + ":";
        foreach (var battleCommand in commandsList)
        {
            ButtonHolder bh = Instantiate(buttonPrefab, buttonParent);
            bh.text.text = battleCommand.title;
            bh.button.onClick.AddListener(()=>pb.chooseTurn(battleCommand));
        }
    }
}
