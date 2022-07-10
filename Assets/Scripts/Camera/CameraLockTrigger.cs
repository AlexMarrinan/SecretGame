using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CameraLockTrigger : MonoBehaviour
{
    public bool lockX;
    public bool lockZ;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("triggered");
            CameraController.instance.SetXLock(lockX);
            CameraController.instance.SetZLock(lockZ);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("exited");
            CameraController.instance.SetXLock(false);
            CameraController.instance.SetZLock(false);
        }
    }
}