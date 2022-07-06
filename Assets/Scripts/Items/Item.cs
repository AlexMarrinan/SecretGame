using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Item : Object
{
    protected float cooldownMax = 0;
    protected float cooldown = 0;
    protected int count = -1;
    public abstract void UseItem();
    public float GetCooldown(){
        return cooldown;
    }
    public void SetCooldown(float newCooldown){
        cooldown = newCooldown;
    }
    public abstract string GetString();
}
