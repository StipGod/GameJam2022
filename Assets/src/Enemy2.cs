using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

public class Enemy2 : MonoBehaviour
{
    // Start is called before the first frame update
    public int xPos,yPos;
    public Tilemap tm;
    public TilemapScript tmScript;
    int[,] array;
    Vector3 moveToPlayer;
    bool moving = false;
    
    void Start()
    {   //array = tmScript.array;
        Random rnd = new Random();
        while(true){
            xPos = rnd.Next(1, 15);
            yPos = rnd.Next(1, 12);
            if(tmScript.GetMartrix(xPos,yPos) == 0){
                break;
            }
        }
        Vector3Int cellPosition;
        cellPosition = new Vector3Int(xPos,yPos,0);
        transform.position = tm.GetCellCenterWorld(cellPosition);
    }

    // Update is called once per frame
    void Update()
    {   
        if(!tmScript.turn && (!moving)){
            move();
        }
        if(moving){
            var step =  tmScript.speed * Time.deltaTime; 
            transform.position = Vector3.MoveTowards(transform.position, moveToPlayer, step);
            if(System.Math.Sqrt(System.Math.Pow(transform.position[0] - moveToPlayer[0],2)+System.Math.Pow(transform.position[1] - moveToPlayer[1],2))<0.001f){
                    moving = false;
                } 
        }
    }

        
    void move(){
        Random rnd = new Random();
        int seed = rnd.Next(1, 5);
        if(seed == 1) xPos+=6;else if(seed == 2) xPos-=6;else if(seed == 3) yPos+=6;else yPos-=6; //puede tener errores

        if (xPos<0)
        {
            xPos = 0;
        }else if(xPos>=tmScript.array.GetUpperBound(0)){
            xPos = tmScript.array.GetUpperBound(0)-1;
        }
        if (yPos<0)
        {
            yPos = 0;
        }else if(yPos>=tmScript.array.GetUpperBound(1)){
            yPos = tmScript.array.GetUpperBound(1)-1;
        }

        Vector3Int cellPosition;
        cellPosition = new Vector3Int(xPos,yPos,0);
        moveToPlayer = tm.GetCellCenterWorld(cellPosition);
        moving = true;
        tmScript.ChangeTurn();
    } 
}