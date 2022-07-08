using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public GameObject bombPrefab;
    public GameObject arrowPrefab;
    [HideInInspector]
    public ItemDatabase itemDatabase;
    public ItemMenu itemMenu;
    
    public Item[] usedItems = new Item[2];
    public List<ItemHudIcon> hudIcons = new List<ItemHudIcon>();
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
        itemDatabase = GetComponent<ItemDatabase>();
        for (int i = 0; i < usedItems.Length; i++){
            if (usedItems[i] != null){
                hudIcons[i].ChangeItem(usedItems[i]);
            }
        }
    }
    public void Update(){
        for (int i = 0; i < usedItems.Count(); i++){
            float cooldown = GetUsedItem(i).GetCooldown();
            if (cooldown <= 0){
                continue;
            }
            usedItems[i].SetCooldown(cooldown - 0.0125f);
        }
    }

    //Assigns a new item to a given item slot
    public void SetUsedItem(int itemNumber, Item item){
        if (itemNumber >= usedItems.Count()){
            return;
        }
        for (int i = 0; i < usedItems.Count(); i++){
            if (usedItems[i] == null){
                usedItems[i] = itemDatabase.emptyItem;
            }
            if (usedItems[i].GetString() == item.GetString() && item.type != ItemType.Empty){
                usedItems[i] = itemDatabase.emptyItem;
                hudIcons[i].itemImage.enabled = false;
                break;
            }
        }
        usedItems[itemNumber] = item;
        hudIcons[itemNumber].ChangeItem(item);
        hudIcons[itemNumber].itemImage.enabled = true;
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
        Debug.Log("using item " + usedItems[itemNumber].type.ToString());
        usedItems[itemNumber].UseItem();
    }
    public void ShowMenu(){
        itemMenu.gameObject.SetActive(true);
        PlayerController.instance.FreezeControl();
        //Freeze game logic (yes TUNIC you should have done this too :))) 
    }
    public void HideMenu(){
        itemMenu.gameObject.SetActive(false);
        PlayerController.instance.UnfreezeControl();
        //Unfreeze game logic
    }
}