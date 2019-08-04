using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsPanel : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI strainText;
    public HealthUI healthBar;
    public Battler battler;
    private bool hasInitStrain = false;

    private void Awake()
    {
        if(battler != null)
            init(battler);
    }

    public void init(Battler battler)
    {
        if (titleText != null)
            titleText.text = battler.title;
        healthBar.battler = battler;
        this.battler = battler;
        updateStrain(battler.stats.strain);
        healthBar.updateVal(battler.stats);
        Debug.Log("init");
        if (!hasInitStrain)
        {
            battler.stats.onStrainChangeEvent += updateStrain;
            hasInitStrain = true;
        }

        if (!healthBar.hasInit)
        {
            healthBar.hasInit = true;
            battler.stats.onHealthChangeEvent += healthBar.updateVal;
        }
    }

    private void OnEnable()
    {
        if (battler != null && !hasInitStrain)
        {
            battler.stats.onStrainChangeEvent += updateStrain;
            hasInitStrain = true;
        }
    }

    private void OnDisable()
    {
        if (battler != null && hasInitStrain)
        {
            battler.stats.onStrainChangeEvent += updateStrain;
            hasInitStrain = false;
        }
    }

    public void updateStrain(int strain)
    {
        strainText.text = "Strain: " + strain;
    }
}
