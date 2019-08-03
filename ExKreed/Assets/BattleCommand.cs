using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleCommand : ScriptableObject
{
    public string title = "nameless technique";
    public int delay = 1;
    public float delayPerStrain = .5f;
    public int strainBoost = 10;


    public TargettingPattern rangePattern;
    public TargettingPattern damagePattern;

    public abstract IEnumerator execute(Battler user, Tile targets);
    public abstract bool isValidTargetTile(Battler user, Tile targets);

    public int calcDelay(Battler user)
    {
        return  Mathf.FloorToInt(delay + (user.stats.strain / Stats.maxStrain + 1) * delayPerStrain);
    }
}
