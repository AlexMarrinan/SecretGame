using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HPPickup: Pickup
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.CompareTag("Player"))
        {
            OnPickup();
        }
    }
    protected override void OnPickup(){
        PlayerStats.instance.IncreaseHP();
        Debug.Log(PlayerStats.instance.maxHP);
        Destroy(this.gameObject);
    }
}