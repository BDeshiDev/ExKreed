using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Cross Targetting")]
public class CrossTargetting : TargettingPattern
{
    public int crossLen = 1;
    public override void selectTargets(Tile[,] tiles,List<Tile> targets, Battler user, int startX, int startY)
    {
        for (int i = 1; i <= crossLen; i++)
        {
            int x = startX + i;
            int y = startY;
            if (x < tiles.GetLength(0) && x >= 0 &&
                y < tiles.GetLength(1) && y >= 0)
                targets.Add(tiles[x, y]);

            x = startX;
            y = startY + i;
            if (x < tiles.GetLength(0) && x >= 0 &&
                y < tiles.GetLength(1) && y >= 0)
                targets.Add(tiles[x, y]);
            x = startX - i;
            y = startY;
            if (x < tiles.GetLength(0) && x >= 0 &&
                y < tiles.GetLength(1) && y >= 0)
                targets.Add(tiles[x, y]);

            x = startX;
            y = startY - i;
            if (x < tiles.GetLength(0) && x >= 0 &&
                y < tiles.GetLength(1) && y >= 0)
                targets.Add(tiles[x, y]);
        }
    }
}
