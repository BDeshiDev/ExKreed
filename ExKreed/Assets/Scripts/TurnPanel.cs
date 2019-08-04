using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnPanel : MonoBehaviour
{
    public Image background;
    public TextMeshProUGUI title;
    public TextMeshProUGUI delayText;

    public void updateVal(Battler battler)
    {
        title.text = battler.title;
        delayText.text = battler.delay.ToString();
    }
}
