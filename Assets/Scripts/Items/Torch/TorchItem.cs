using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TorchItem : Item
{
    
    public override void UseItem(){
        Debug.Log("Blunt smoked");
    }
    public override string GetString(){
        return "TorchItem";
    }
}