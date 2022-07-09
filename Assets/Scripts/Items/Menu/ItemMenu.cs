using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ItemMenu : MonoBehaviour
{
    public GameObject menuUI;
    public static ItemMenu instance;
    public Image hoverImage;
    public List<ItemMenuIcon> icons;
    int selectionIndex;
    void Awake(){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
    void Start(){
        selectionIndex = 0;
        icons = GetComponentsInChildren<ItemMenuIcon>().ToList();
    }
    void Update()
    {
        GetMenuInput();
        hoverImage.transform.position = icons[selectionIndex].transform.position;
        //sprite.transform.rotation = startRotation;
    }

    void GetMenuInput(){
        if (Input.GetKeyDown(KeyCode.Tab)){
            ItemManager.instance.HideMenu();
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)){
            selectionIndex--;
            if (selectionIndex < 0){
                selectionIndex = icons.Count() - 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){
            selectionIndex++;
            if (selectionIndex >= icons.Count()){
                selectionIndex = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            icons[selectionIndex].SetItem(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            icons[selectionIndex].SetItem(1);
        }
    }
}