using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapScript : MonoBehaviour
{
    public Tilemap tm;
    public GameObject p;

    int dice1,dice2;
    Vector2 worldPoint;
    int[,] array = new int[10, 11];
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Try to get a tile from cell position
            Vector3Int cellPosition = tm.WorldToCell(worldPoint);
            p.transform.position = tm.GetCellCenterWorld(cellPosition);
            Tile tm.GetTile(cellPosition);
            //p.transform.position = worldPoint;
        }
    }

    void RollDice(){
        //dice1 = random.Next(7);
        //dice2 = random.Next(7);
    }
}

