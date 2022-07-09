using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : MonoBehaviour
{
    public float fuseTime = 60f;
    private float remainingFuse;
    public GameObject explosion;
    public BombProjectile(){
        remainingFuse = fuseTime;
    }

    public void Update(){
        //Debug.Log(remainingFuse);
        if (remainingFuse <= 0){
            //TODO: Add explosion
            //transform.localScale = new Vector3(10,10,10);
            explosion.SetActive(true);
            return;
        }
        remainingFuse -= 0.0125f;
    }
}