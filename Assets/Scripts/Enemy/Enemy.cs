using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState {
     Idle,
     Walking,
     Attacking,
}
public class Enemy : MonoBehaviour
{
     public int health;

     private void OnCollisionEnter(Collision other) {

     }

     public void TakeDamage(int damage){
          health -= damage;
          if (health <= 0){
               gameObject.SetActive(false);
          }
     }
}