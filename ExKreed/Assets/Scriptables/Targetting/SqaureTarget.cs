using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Sqaure Targetting")]
public class SqaureTarget : TargettingPattern
{
    public int len = 1;
    public override void selectTargets(Tile[,] tiles, List<Tile> targets, Battler user, int startX, int startY)
    {
        for (int y = startY - (len); y <= (startY + (len)); y++)
        {
            for (int x = startX - (len); x <= (startX + (len)); x++)
            {
                if (x < tiles.GetLength(0) && x >= 0 &&
                    (y) < tiles.GetLength(1) && (y) >= 0)
                    targets.Add(tiles[x, y]);
            }
        }
    }
}