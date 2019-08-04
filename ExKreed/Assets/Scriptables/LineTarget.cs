using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Line Targetting")]
public class LineTarget : TargettingPattern
{
    public int lineLen = 1;

    public override void selectTargets(Tile[,] tiles, List<Tile> targets, Battler user, int startX, int startY)
    {
        for (int i = 1; i <= lineLen; i++)
        {
            if (startX == user.curTile.x && startY != user.curTile.y)//up || down
            {
                int x = user.curTile.x;
                int y = user.curTile.y + (startY > user.curTile.y ? i : -i);

                if(x < tiles.GetLength(0) && x >= 0 &&
                   y < tiles.GetLength(1) && y >= 0)
                    targets.Add(tiles[x,y]);
            }

            else if(startY == user.curTile.y && startX != user.curTile.x)//left || right
            {
                int x = user.curTile.x + (startX > user.curTile.x ? i : -i);
                int y = user.curTile.y;

                if (x < tiles.GetLength(0) && x >= 0 &&
                    y < tiles.GetLength(1) && y >= 0)
                    targets.Add(tiles[x, y]);
            }
        }
    }

}