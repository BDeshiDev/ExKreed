using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargettingSystem : MonoBehaviour
{
    public TileGrid grid;
    public Tile selectedTile;
    public List<Tile> targetPreviewList;

    public IEnumerator getTargets(CommandHolder command)
    {
        Debug.Log("await targets");
        while (selectedTile == null)
        {
            yield return null;
        }

        command.target = selectedTile;
        targetPreviewList.Clear();
        command.command.damagePattern.selectTargets(grid.tiles,targetPreviewList,selectedTile.x, selectedTile.y);
        foreach (var tile in targetPreviewList)
        {
            tile.setTileState(TileState.target);
        }
        
        Debug.Log("received targets");
        selectedTile = null;
    }

    public void handleTileSelection(Tile tile)
    {
        selectedTile = tile;
    }

    private void OnEnable()
    {
        grid.onTileSelected += handleTileSelection;
    }
    private void OnDisable()
    {
        grid.onTileSelected -= handleTileSelection;
    }
}
