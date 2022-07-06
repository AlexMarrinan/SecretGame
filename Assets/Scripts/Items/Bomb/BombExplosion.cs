using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class BombExplosion : MonoBehaviour
{
    public GameObject parent;
    public float explosionTime = 5f;
    void Start(){

    }
    void Update(){
        explosionTime -= 0.0125f;
        if (explosionTime <= 0){
            Destroy(parent);            
        }
    }
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player"))
        {
            //TODO: DO DAMAGE;
        }
        if (other.CompareTag("Breakable")){
            Destroy(other.gameObject);
        }
    }
}
