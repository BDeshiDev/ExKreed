using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePanelUI : MonoBehaviour
{
    public ButtonHolder buttonPrefab;

    public void createButtonList(PlayerBattler pb,List<BattleCommand> commandsList)
    {
        foreach (var battleCommand in commandsList)
        {
            ButtonHolder bh = Instantiate(buttonPrefab, transform);
            bh.text.text = battleCommand.title;
            bh.button.onClick.AddListener(()=>pb.chooseTurn(battleCommand));
        }
    }
}
