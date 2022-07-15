using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//enum indicating who TAKES damage from a hitbox
public enum HitboxType {
    Player,
    Enemy,
    All,
}


//Hitbox used for damaging from attacks, touching enemies or enviromental hazards are seperate
[RequireComponent(typeof(Collider))]
public class AttackHitbox : MonoBehaviour
{
    //damage amount
    public int damage;
    //enum indicating who TAKES damage from a hitbox
    public HitboxType damageWho;
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other);
        //if it damages an player: do damage to the player
        if (other.CompareTag("Player") && (damageWho == HitboxType.Player || damageWho == HitboxType.All)){
            Debug.Log("Attacked player");
            PlayerStats.instance.TakeDamage(damage);
        //if it damages an enemy, do damage to the enemy
        }else if (other.CompareTag("Enemy") && (damageWho == HitboxType.Enemy || damageWho == HitboxType.All)){
            Enemy e = other.gameObject.GetComponent<Enemy>();
            if (e != null){
                Debug.Log("Attacked enemy");
                e.TakeDamage(damage);
            }
        }
    }
}