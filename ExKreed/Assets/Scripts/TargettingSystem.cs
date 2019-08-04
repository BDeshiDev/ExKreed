using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargettingSystem : MonoBehaviour
{
    public TileGrid grid;
    public Tile selectedTile;
    public List<Tile> targetPreviewList;
    public bool canSelectTiles = false;

    public IEnumerator getTargets(CommandHolder command)
    {
        Debug.Log("await targets");
        canSelectTiles = true;
        while (selectedTile == null)
        {
            yield return null;
        }

        command.target = selectedTile;
        targetPreviewList.Clear();
        targetPreviewList.Add(selectedTile);
        command.command.damagePattern.selectTargets(grid.tiles,targetPreviewList,command.user,selectedTile.x, selectedTile.y);

        foreach (var tile in targetPreviewList)
        {
            tile.setTileState(TileState.target);
        }
        
        Debug.Log("received targets");
        selectedTile = null;
        canSelectTiles = false;
    }

    public void handleTileSelection(Tile tile)
    {
        if(canSelectTiles)
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
