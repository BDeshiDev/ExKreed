 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Rhombus Targetting")]
public class RhombusTargetting : TargettingPattern
{
    public int xLen = 1;
    public override void selectTargets(Tile[,] tiles, List<Tile> targets, Battler user, int startX, int startY)
    {
        for (int y = 0; y <= xLen; y++)
        {
            for (int x = startX  - (xLen -y) ; x <= (startX + (xLen-y)); x++)
            {
                if (x < tiles.GetLength(0) && x >= 0 &&
                    (startY + y) < tiles.GetLength(1) && (startY + y) >= 0)
                    targets.Add(tiles[x, startY + y]);

                if (y != 0 &&  x < tiles.GetLength(0) && x >= 0 &&
                    (startY - y) < tiles.GetLength(1) && (startY - y) >= 0)
                    targets.Add(tiles[x, startY - y]);
            }
        }
    }
}