using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum ItemType{
    None = 0,
    Bomb = 1,
    Bow,
    Potion,
    Dash,
    Wall,
}


public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    Item[] usedItems = new Item[2];

    void Awake(){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else{
            Destroy(gameObject);
        }
    }
    void Start(){
        //Test assigning values:
        usedItems[0] = new BombItem();
        usedItems[1] = new EmptyItem();
    }
    public void Update(){
        for (int i = 0; i < usedItems.Count(); i++){
            float cooldown = usedItems[i].GetCooldown();
            if (cooldown <= 0){
                continue;
            }
            usedItems[i].SetCooldown(cooldown - 0.0165f);
        }
    }

    //Assigns a new item to a given item slot
    public void SetUsedItem(int itemNumber, Item item){
        if (itemNumber >= usedItems.Count()){
            return;
        }
        for (int i = 0; i < usedItems.Count(); i++){
            if (usedItems[i].GetString() == item.GetString() && item.GetString() != "Item"){
                usedItems[i] = new EmptyItem();
                break;
            }
        }
        usedItems[itemNumber] = item;
    }
    public Item GetUsedItem(int itemNumber){
        //Debug.Log("items count: " + usedItems.Count());
        //Debug.Log("item number: " + usedItems[itemNumber].GetString());
        if (itemNumber >= usedItems.Count()){
            return null;
        }
        return usedItems[itemNumber];
    }
    public void UseItem(int itemNumber){
        if (itemNumber >= usedItems.Count()){
            return;
        }
        //Debug.Log("using item " + usedItems[itemNumber].GetString());
        usedItems[itemNumber].UseItem();
    }
}