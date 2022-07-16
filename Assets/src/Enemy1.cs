using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

public class Enemy1 : MonoBehaviour
{
    // Start is called before the first frame update
    public int xPos,yPos;
    public Tilemap tm;
    public TilemapScript tmScript;
    int[,] array;
    void Start()
    {   array = tmScript.GetMartrix();
        Random rnd = new Random();
        while(true){
            xPos = rnd.Next(1, 15);
            yPos = rnd.Next(1, 12);
            if(array[xPos,yPos] == 0){
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
        if(!tmScript.turn){
            if(tmScript.xPos == xPos){
                //shoot x 
            }else if(tmScript.yPos == yPos){
                //shoot y
            }else if(tmScript.xPos - xPos == tmScript.yPos - yPos){
                //shoot diag
            }

        }
    }
}
