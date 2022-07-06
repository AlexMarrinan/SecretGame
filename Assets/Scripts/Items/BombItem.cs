using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BombItem : Item
{
    public BombItem () {
        cooldownMax = 30f;
        cooldown = 0;
        count = -1;
    }
    public override void UseItem(){
        if (cooldown > 0 || count == 0){
            Debug.Log("on cooldown or out of items: " + cooldown + " " + count);
            return;
        }
        count--;
        cooldown = cooldownMax;

        Debug.Log("Threw bomb!");
        Instantiate(ItemManager.instance.bombPrefab, PlayerController.instance.transform.position, Quaternion.identity);
        return;
    }
    public override string GetString(){
        return "BombItem";
    }
}
