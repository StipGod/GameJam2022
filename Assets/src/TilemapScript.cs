using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

public class TilemapScript : MonoBehaviour
{
    public Tilemap tm;
    public GameObject p,bullet,xxx;
    GameObject newBullet;
    public float speed = 10f,bulletSpeed = 10f;
    public int dice1,dice2;
    int currentDice, usedDice = 0;
    public int xPos = 0,yPos = 0;
    Vector2 worldPoint;
    Vector3 moveToPlayer,moveToBullet;
    public int[,] array = new int[14, 11];
    public List<GameObject> hexList;    
    bool shot = false,moving = false,dice = false;
    public bool turn = true;
    void Start() {

        hexList = new List<GameObject>();

        for (int i  = 0; i < array.GetUpperBound(0); i++)
        {
            for (int j = 0; j < array.GetUpperBound(1); j++)
            {
                array[i,j] = 0;
            }
        }
        array[xPos,yPos] = 1;

        RollDice();
        currentDice = dice1;
        moveToPlayer =  tm.GetCellCenterWorld(tm.WorldToCell(Vector3.zero));
        
        GetValidPositions();
        DrawValidPos();
    }
    void Update()
    {   if(turn){
            if (Input.GetMouseButtonDown(0))
            {   
                worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int cellPosition = tm.WorldToCell(worldPoint);
                if (cellPosition[0]>=0 && cellPosition[0]<array.GetUpperBound(0) && cellPosition[1]>=0 && cellPosition[1]<array.GetUpperBound(1))
                    {
                    if(array[cellPosition[0],cellPosition[1]] == -1){
                        //p.transform.position = tm.GetCellCenterWorld(cellPosition);
                        moveToPlayer = tm.GetCellCenterWorld(cellPosition);

                        array[xPos,yPos] = 0;
                        xPos = cellPosition[0];
                        yPos = cellPosition[1];
                        array[cellPosition[0],cellPosition[1]] = 1;
                        moving = true;
                        }
                    }
            }else if(Input.GetMouseButtonDown(1)){
                worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int cellPosition = tm.WorldToCell(worldPoint);
                if (cellPosition[0]>=0 && cellPosition[0]<array.GetUpperBound(0) && cellPosition[1]>=0 && cellPosition[1]<array.GetUpperBound(1))
                    {
                    if(array[cellPosition[0],cellPosition[1]] == -1 && !shot){
                        newBullet = Instantiate(bullet,p.transform);
                        moveToBullet = tm.GetCellCenterWorld(cellPosition);
                        shot = true;
                        }
                    }
                
            }else if(Input.GetMouseButtonDown(2)){
                ChangeDice();
            }

            if(moving){
                var step =  speed * Time.deltaTime; 
                p.transform.position = Vector3.MoveTowards(p.transform.position, moveToPlayer, step);
                if(System.Math.Sqrt(System.Math.Pow(p.transform.position[0] - moveToPlayer[0],2)+System.Math.Pow(p.transform.position[1] - moveToPlayer[1],2))<0.001f){
                    moving = false;
                    CleanMatrix();
                    GetValidPositions();
                    DrawValidPos();
                    if (usedDice == 0)
                    {
                        if(dice){
                            usedDice = 1;
                        }else{
                            usedDice = 2;
                        }
                    }else{
                        RollDice(); 
                    }
                    ChangeDice();
                } 
            }
            if(shot){
                var step =  bulletSpeed * Time.deltaTime;
                newBullet.transform.position = Vector3.MoveTowards(newBullet.transform.position, moveToBullet, step);
                if(System.Math.Sqrt(System.Math.Pow(newBullet.transform.position[0] - moveToBullet[0],2)+System.Math.Pow(newBullet.transform.position[1] - moveToBullet[1],2))<0.001f){
                    Destroy(newBullet);
                    shot = false;
                    CleanMatrix();
                    GetValidPositions();
                    DrawValidPos();
                    if (usedDice == 0)
                    {
                        if(dice){
                            usedDice = 1;
                        }else{
                            usedDice = 2;
                        }
                    }else{
                        RollDice(); 
                    }
                    ChangeDice();
                }  
            }
        }
    }
    private void RollDice(){
        Random rnd = new Random();
        dice1 = rnd.Next(1, 7);
        dice2 = rnd.Next(1, 7);
        usedDice = 0;
        dice = true;
        currentDice = dice1;
        ChangeTurn();
    }

    private void ChangeDice(){
        if(usedDice == 0){
            if(dice){
                currentDice = dice1;
            }else{
                currentDice = dice2;
            }
            dice = !dice;

        }else if(usedDice == 1){
            currentDice = dice1;
        }else{
            currentDice = dice2;
        }

        CleanMatrix();
        GetValidPositions();
        DrawValidPos();
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
                if((i==0 || j ==0)){
                    if (auxXPos<0)
                    {
                        auxXPos = 0;
                    }else if(auxXPos>=array.GetUpperBound(0)){
                        auxXPos = array.GetUpperBound(0)-1;
                    }
                    if (auxYPos<0)
                    {
                        auxYPos = 0;
                    }else if(auxYPos>=array.GetUpperBound(1)){
                        auxYPos = array.GetUpperBound(1)-1;
                    }
                    if(array[auxXPos,auxYPos] == 0){
                        array[auxXPos,auxYPos] = -1;
                    } 
                }else{
                    while(!(auxXPos>=0 && auxXPos<array.GetUpperBound(0) && auxYPos>=0 && auxYPos<array.GetUpperBound(1)) ){
                        auxXPos -= i;
                        auxYPos -= j;
                    }
                    if(array[auxXPos,auxYPos] == 0){
                        array[auxXPos,auxYPos] = -1;
                    }
                }
                
                
            }
            }
        }
    private void DrawValidPos(){
        Vector3Int auxPos;
        for (int i  = 0; i < array.GetUpperBound(0); i++)
        {
            for (int j = 0; j < array.GetUpperBound(1); j++)
            {
                if(array[i,j] == -1){
                    auxPos = new Vector3Int(i,j,0); 
                    GameObject hexItem = Instantiate(xxx,tm.GetCellCenterWorld(auxPos),Quaternion.identity);
                    hexList.Add(hexItem);    
                }
            }
        }
    }

    private void CleanMatrix(){
        for (int i  = 0; i < array.GetUpperBound(0); i++)
        {
            for (int j = 0; j < array.GetUpperBound(1); j++)
            {   
                if(array[i,j] == -1){
                    array[i,j] = 0;
                }
            }
        }
        foreach (var obj in hexList)
        {
            Destroy(obj);
        }
    }

    public void ChangeTurn(){
        turn = !turn;
    }

    public int GetMartrix(int i,int j){
        return array[i,j];
    }
    public void EndGame(){

    }
}


