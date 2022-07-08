using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Linq;

public class ItemMenuIcon : MonoBehaviour
{
    public Item item;
    public Image itemImage;

    void Start(){
        itemImage.sprite = item.sprite;
    }
    public void SetItem(int itemNumber){
        ItemManager.instance.SetUsedItem(itemNumber, this.item);
    }
}