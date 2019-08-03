using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    private Tile[,] tiles;
    public int gridSize = 10;
    public Tile tilePrefab;
    public float tileDist;

    private void Awake()
    {
        tiles = new Tile[gridSize,gridSize];
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                tiles[i, j] = Instantiate(tilePrefab, 
                    transform.position + tileDist * new Vector3(i, j),
                    Quaternion.identity);
                tiles[i,j].gameObject.name = i + "," + j;
            }
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
                if(t != null)
                    t.handleClick();
            }
        }
    }
}
