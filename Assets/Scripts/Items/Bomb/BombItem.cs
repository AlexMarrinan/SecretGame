using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BombItem : Item
{
    public override void UseItem(){
        if (cooldown > 0 || count == 0){
            Debug.Log("on cooldown or out of items: " + cooldown + " " + count);
            return;
        }
        count--;
        cooldown = cooldownMax;

        Debug.Log("Threw bomb!");
        Instantiate(ItemManager.instance.itemDatabase.bombPrefab, 
            PlayerController.instance.projectilePos, 
            Quaternion.identity);
        return;
    }
    public override string GetString(){
        return "BombItem";
    }
}
