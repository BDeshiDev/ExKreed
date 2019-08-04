using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleGroup<T> : Battler where T: Battler
{
    public List<T> battlers;
}
