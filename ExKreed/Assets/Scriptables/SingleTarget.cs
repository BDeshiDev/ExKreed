using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "single targetting")]
public class SingleTarget : TargettingPattern
{
    public override void selectTargets(Tile[,] tiles, List<Tile> targets, Battler user, int startX, int startY)
    {
        if(startX < tiles.GetLength(0) && startX >= 0 &&
           startY < tiles.GetLength(1) && startY >= 0)
        targets.Add(tiles[startX,startY]);
    }
}
