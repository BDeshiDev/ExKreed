using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleCommand : ScriptableObject
{
    public string title = "nameless technique";
    public int delay = 1;
    public float delayPerStrain = .5f;
    public abstract IEnumerator execute(Battler user, Tile targets);

    public TargettingPattern rangePattern;
    public int rangeLen = 1;
    public TargettingPattern damagePattern;
    public int damageRangeLen = 1;

    public int calcDelay(Battler user)
    {
        return  Mathf.FloorToInt(delay + (user.stats.strain / Stats.maxStrain + 1) * delayPerStrain);
    }
}
