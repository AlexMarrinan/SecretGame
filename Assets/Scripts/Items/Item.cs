using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum ItemType{
    Invalid = -1,
    Empty = 0,
    Bomb = 1,
    Bow,
    Torch,
    Potion,
    Dash,
    Wall,
}

public abstract class Item: MonoBehaviour
{
    public ItemType type = ItemType.Invalid;
    public float cooldownMax = 0;
    protected float cooldown = 0;
    public int count = -1;
    public Sprite sprite;
    public abstract void UseItem();
    public float GetCooldown(){
        return cooldown;
    }
    public void SetCooldown(float newCooldown){
        cooldown = newCooldown;
    }
    public abstract string GetString();
    
    protected Sprite LoadSprite( string imageName, string spriteName)
    {
        Sprite[] all = Resources.LoadAll<Sprite>(imageName);
        Debug.Log(all.Length);
        foreach( var s in all){
            if (s.name == spriteName){
                return s;
            }
        }
        return null;
    }
}
