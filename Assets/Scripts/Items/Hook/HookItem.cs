using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HookItem : Item
{
    public override void UseItem(){
        if (PlayerController.instance.inHook){
            return;
        }
        Debug.Log("Shot Hook!");
        //TODO:
        //  -Fix not colliding with some walls at faster velocities
        //  -Fixed distance so dont freeze player forever
        
        //PlayerController.instance.FreezeControl();
        Instantiate(ItemManager.instance.itemDatabase.hookPrefab, 
            PlayerController.instance.projectilePos, 
            Quaternion.identity);
        //Instantiate(ItemManager.instance.bombPrefab, PlayerController.instance.transform.position, Quaternion.identity);
        return;
    }
    public override string GetString(){
        return "HookItem";
    }
}
