using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleCommand : ScriptableObject
{
    public string title;
    public int delay = 1;
    public abstract IEnumerator execute();
}
