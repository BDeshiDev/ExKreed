using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public Battler battler;
    public TextMeshProUGUI healthText;
    public Slider healthSlider;
    public bool hasInit = false;

    public void OnEnable()
    {
        if (battler == null)
            return;

        Stats stats = battler.stats;

        if (!hasInit)
        {
            stats.onHealthChangeEvent += updateVal;
            hasInit = true;
        }

        updateVal(stats);
    }

    public void updateVal(Stats stats) {
        healthText.text = stats.curHp + "/" + (int)stats.maxHp;
        healthSlider.value = ((float)stats.curHp) / stats.maxHp;
    }
    private void OnDisable()
    {
        if (battler != null && hasInit)
        {
            battler.stats.onHealthChangeEvent -= updateVal;
            hasInit = false;
        }
    }
}
