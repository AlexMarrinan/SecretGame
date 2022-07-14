using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//enum indicating who TAKES damage from a hitbox
public enum HitboxType {
    Player,
    Enemy,
    All,
}


//Hitbox used for damaging, 
[RequireComponent(typeof(Collider))]
public class Hitbox : MonoBehaviour
{
    //damage amount
    public int damage;
    //enum indicating who TAKES damage from a hitbox
    public HitboxType damageWho;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && (damageWho == HitboxType.Player || damageWho == HitboxType.All)){
            PlayerStats.instance.TakeDamage(damage);
        }else if (other.CompareTag("Player") && (damageWho == HitboxType.Enemy || damageWho == HitboxType.All)){
            //Deal Damage to enemy !!!
        }
    }
}