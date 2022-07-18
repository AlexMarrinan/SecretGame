using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookProjectile : MonoBehaviour
{
    public float velocity = 10f;
    
    private Rigidbody rb;
    public void Start(){
        rb = GetComponent<Rigidbody>();
        rb.velocity = velocity*PlayerController.instance.ProjectilePosRelative();
    }
    public void Update(){

    }
    public void OnCollisionEnter(Collision other) {
        if (other.collider.tag == "Player"){
            return;
        }
        PlayerController.instance.SetPostition(this.transform.position);
        PlayerController.instance.inHook = false;
        Destroy(this.gameObject);
        PlayerController.instance.UnfreezeControl();
    }
}