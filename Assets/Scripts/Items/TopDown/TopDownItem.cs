using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TopDownItem : Item
{
    public override void UseItem(){
        Debug.Log("Went Topdown");
        CameraController.instance.isTopDown = !CameraController.instance.isTopDown;

        // if (!CameraController.instance.isTopDown){
        //     CameraController.instance.cd = CameracDirection.Infront;
        //     CameraController.instance.transform.rotation = Quaternion.Euler(45, 0, 0);
        // }else{
        //     CameraController.instance.transform.rotation = Quaternion.Euler(90, 0, 0);
        // }
    }
    public override string GetString(){
        return "TopDown";
    }
}