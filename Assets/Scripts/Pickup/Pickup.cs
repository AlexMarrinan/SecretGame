using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Pickup: MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.CompareTag("Player"))
        {
            OnPickup();
        }
    }
    protected abstract void OnPickup();
}