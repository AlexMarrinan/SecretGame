using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemDatabase : MonoBehaviour
{
    void Awake(){
        DontDestroyOnLoad(this);
    }
    public EmptyItem emptyItem;
    public BombItem bombItem;
    public TorchItem torchItem;
    public BowItem bowItem;
    public TopDownItem topDownItem;
    public HookItem hookItem;
    
    //Prefabs
    public GameObject bombPrefab;
    public GameObject hookPrefab;

}