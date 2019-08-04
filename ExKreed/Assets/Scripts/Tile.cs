using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Battler occupant = null;
    private SpriteRenderer spriter;
    public Color normalColor = Color.cyan;
    public Color targettedColor = Color.red;
    public Color rangeColor = Color.yellow;
    public int x, y;
    public TileState curState;

    private void Awake()
    {
        spriter = GetComponent<SpriteRenderer>();
        setTileState(TileState.normal);
    }

   

    public void setTileState(TileState newState)
    {
        curState = newState;
        switch (curState)
        {
            case TileState.normal:
                spriter.color = normalColor;
                break;
            case TileState.target:
                spriter.color = targettedColor;
                break;
            case TileState.range:
                spriter.color = rangeColor;
                break;
        }
    }

    public void placeOccupant(Battler newOccupant)
    {
        occupant = newOccupant;
        occupant.transform.position = transform.position;
        occupant.curTile = this;
    }
    public void removeOccupant()
    {
        occupant = null;
    }


    public void handleClick() {
        Debug.Log(gameObject.name + "clicked");
    }
}

public enum TileState
{
    normal,target,range,
}