using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public Tile[,] tiles;
    public int gridSize = 10;
    public float tileSize = .90f;
    public Tile tilePrefab;
    public Transform bottomLeft;
    public Transform upperRight;
    public Transform background;

    public event Action<Tile> onTileSelected;
    public PlayerGroup playerGroup;

    public List<PartyMember> playerParty;
    public List<PartyMember> enemies;
    public Transform gridHolder;
    public int gridSizeMultiplier = 2;

    private void Awake()
    {
        transform.localScale = Vector3.one * gridSize * gridSizeMultiplier;
        tiles = new Tile[gridSize,gridSize];
        Vector3 startPos = bottomLeft.position;
        background.position = (bottomLeft.position + upperRight.position) / 2;
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                tiles[i, j] = Instantiate(tilePrefab, 
                    bottomLeft.position + gridSizeMultiplier * .5f *Vector3.one  + gridSizeMultiplier * new Vector3(i, j),
                    Quaternion.identity);
                tiles[i,j].transform.localScale = Vector3.one * tileSize * gridSizeMultiplier;
                tiles[i,j].gameObject.name = i + "," + j;
                tiles[i,j].transform.parent = gridHolder;
                tiles[i, j].x = i;
                tiles[i, j].y = j;
            }
        }

        foreach (var partyMember in playerParty)
        {
            tiles[partyMember.startX,partyMember.startY].placeOccupant(partyMember.battler);
        }

        foreach (var enemy in enemies)
        {
            tiles[enemy.startX, enemy.startY].placeOccupant(enemy.battler);
        }
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit,1000))
            {
                Tile t = hit.transform.GetComponent<Tile>();
                if (t != null && t.curState == TileState.range)
                {
                    t.handleClick();
                    onTileSelected?.Invoke(t);
                }
            }
        }
    }
}
