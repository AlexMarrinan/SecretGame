using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudUI : MonoBehaviour
{
    public static HudUI instance;
    void Awake()
    {
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(instance);
        }else{
            Destroy(this.gameObject);
        }   
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
