using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapScript : MonoBehaviour
{
    public Tilemap tm;
    public GameObject p;

    int dice1,dice2,currentDice;
    int xPos = 0,yPos = 0;
    Vector2 worldPoint;
    int[,] array = new int[14, 11];

    public float speed = 1.0f;


    void Start() {
        for (int i  = 0; i < array.GetUpperBound(0); i++)
        {
            for (int j = 0; j < array.GetUpperBound(1); j++)
            {
                array[i,j] = 0;
            }
        }
        array[0,0] = 1;

        currentDice = 3;
    }
    void Update()
    {   
        
        if (Input.GetMouseButtonDown(0))
        {
            changeDice();
            worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = tm.WorldToCell(worldPoint);
            Debug.Log(cellPosition);
            if (cellPosition[0]>=0 && cellPosition[0]<array.GetUpperBound(0) && cellPosition[1]>=0 && cellPosition[1]<array.GetUpperBound(1))
                {
                if(array[cellPosition[0],cellPosition[1]] == -1){
                    p.transform.position = tm.GetCellCenterWorld(cellPosition);
                    //var step =  speed * Time.deltaTime; 
                    //p.transform.position = Vector3.MoveTowards(p.transform.position, tm.GetCellCenterWorld(cellPosition), step);

                    array[xPos,yPos] = 0;
                    xPos = cellPosition[0];
                    yPos = cellPosition[1];
                    array[cellPosition[0],cellPosition[1]] = 1;
                    }
                }
        }       
    }

    private void RollDice(){
        //dice1 = random.Next(7);
        //dice2 = random.Next(7);

    }

    private void changeDice(){
        for (int i  = 0; i < array.GetUpperBound(0); i++)
        {
            for (int j = 0; j < array.GetUpperBound(1); j++)
            {   
                if(array[i,j] == -1){
                    array[i,j] = 0;
                    Debug.Log(i + " " + j);
                }
            }
        }
        GetValidPositions();
    }

    private void GetValidPositions(){
        
        int auxXPos;
        int auxYPos;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {   
                auxXPos = xPos+i*currentDice;
                auxYPos = yPos+j*currentDice;
                if (auxXPos>=0 && auxXPos<array.GetUpperBound(0) && auxYPos>=0 && auxYPos<array.GetUpperBound(1))
                {   
                    if(array[auxXPos,auxYPos] == 0){
                        array[auxXPos,auxYPos] = -1;
                    } 
                    
                }
            }
        }
    }
}

