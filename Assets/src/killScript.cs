using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killScript : MonoBehaviour
{   
    public bool playerBullet = true;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other){
    if (other.tag == "Enemy" && playerBullet){

        Destroy(other.gameObject);
    }else if(other.tag == "Player" && !playerBullet){
        Destroy(other.gameObject);
        }
    }
}
