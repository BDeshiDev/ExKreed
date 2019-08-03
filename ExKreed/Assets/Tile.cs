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
    public int x, y;

    private void Awake()
    {
        spriter = GetComponent<SpriteRenderer>();
        toggleTarget(false);
    }

    public void toggleTarget(bool canBeTargetted)
    {
        spriter.color = canBeTargetted ? targettedColor : normalColor;
    }

    public void placeOccupant(Battler newOccupant)
    {
        occupant = newOccupant;
        occupant.transform.position = transform.position;
    }
    public void removeOccupant(Battler newOccupant)
    {
        occupant = null;
    }


    public void handleClick() {
        Debug.Log(gameObject.name + "clicked");
    }
}
