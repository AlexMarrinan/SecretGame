using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EmptyItem : Item
{
    public override void UseItem(){
    
    }
    public override string GetString(){
        return "EmptyItem";
    }
}
