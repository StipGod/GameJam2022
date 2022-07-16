using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

public class Enemy1 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyBullet;
    GameObject newBullet;
    public int xPos,yPos;
    public Tilemap tm;
    public TilemapScript tmScript;
    int[,] array;
    Vector3 moveToPlayer,moveToBullet;
    bool shot = false,moving = false;
    
    void Start()
    {   //array = tmScript.GetMartrix();
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
        if(!tmScript.turn && (!shot && !moving)){
            if(tmScript.xPos == xPos ){
                shoot();
            }else if(tmScript.yPos == yPos && !shot){
                shoot();
            }else if(System.Math.Abs(tmScript.xPos - xPos) == System.Math.Abs(tmScript.yPos - yPos) && !shot){
                shoot();
            }else{
                move();
            }

        }
        if(shot){
                var step =  tmScript.bulletSpeed * Time.deltaTime;
                newBullet.transform.position = Vector3.MoveTowards(newBullet.transform.position, moveToBullet, step);
                if(System.Math.Sqrt(System.Math.Pow(newBullet.transform.position[0] - moveToBullet[0],2)+System.Math.Pow(newBullet.transform.position[1] - moveToBullet[1],2))<0.001f){
                    Destroy(newBullet);
                    shot = false;
                    Debug.Log("kill");
                    tmScript.EndGame();
                    //kill
                
                }  
        }
        if(moving){
            var step =  tmScript.speed * Time.deltaTime; 
            transform.position = Vector3.MoveTowards(transform.position, moveToPlayer, step);
            if(System.Math.Sqrt(System.Math.Pow(transform.position[0] - moveToPlayer[0],2)+System.Math.Pow(transform.position[1] - moveToPlayer[1],2))<0.001f){
                    moving = false;
                    tmScript.ChangeTurn();
                } 
        }
    }

    void shoot(){
        Vector3Int cellPosition;
        cellPosition = new Vector3Int(tmScript.xPos,tmScript.yPos,0);
        newBullet = Instantiate(enemyBullet,transform);
        moveToBullet = tm.GetCellCenterWorld(cellPosition);
        shot = true;
        }  
        
    void move(){
        Random rnd = new Random();
        int seed = rnd.Next(1, 5);
        if(seed == 1) xPos++;else if(seed == 2) xPos--;else if(seed == 3) yPos++;else yPos--; //puede tener errores
        Vector3Int cellPosition;
        cellPosition = new Vector3Int(xPos,yPos,0);
        moveToPlayer = tm.GetCellCenterWorld(cellPosition);
        moving = true;
    } 
}


    

