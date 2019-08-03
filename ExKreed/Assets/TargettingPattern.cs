using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargettingPattern : ScriptableObject
{
    public abstract void selectTargets(Tile[,] tiles,List<Tile> targets, int startX, int startY);
}
