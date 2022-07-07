using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ItemHudIcon : MonoBehaviour
{
    public Image itemImage;
    public int itemNumber;
    public Item item;
    public void ChangeItem(Item item){
        if (item.type == ItemType.Empty){
            SetImageAlpha(0);
            return;
        }
        SetImageAlpha(1);
        Debug.Log("Changed item to " + item);
        this.item = item;
        itemImage.sprite = item.sprite;
    }
    public void SetImageAlpha(float a){
        float r = itemImage.color.r;
        float g = itemImage.color.g;
        float b = itemImage.color.b;
        itemImage.color = new Color(r, g, b, a);

    }
}