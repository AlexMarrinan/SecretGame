using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BowItem : Item
{
    public override void UseItem(){
        Debug.Log("Shot Bow!");
        //Instantiate(ItemManager.instance.bombPrefab, PlayerController.instance.transform.position, Quaternion.identity);
        return;
    }
    public override string GetString(){
        return "Bowitem";
    }
}
