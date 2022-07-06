using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class AreaLoadTrigger : MonoBehaviour
{
    public GameObject target;
    
    public bool show = true;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target.SetActive(show);
        }
    }

    public void OnEvent(){
        target.SetActive(show);
    }
}