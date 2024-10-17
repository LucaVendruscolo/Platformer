using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        // Check if the object we hit has the "enemy" tag
        if(other.CompareTag("enemy")){
            // Get the parent object of the hitbox (the enemy object)
            GameObject enemy = other.transform.parent.gameObject;

            print("Hit " + enemy.name + "!");
            
            // Destroy the parent enemy object
            Destroy(enemy);  
            
            // Destroy the bullet
            Destroy(gameObject); 
        }
    }
}
