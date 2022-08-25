using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TwoDItem : Item
{
    public override void UseItem(){
        Debug.Log("Went 2D");
        CameraController.instance.is2d = !CameraController.instance.is2d;
        CameraController.instance.isTopDown = false;
        // if (!CameraController.instance.isTopDown){
        //     CameraController.instance.cd = CameracDirection.Infront;
        //     CameraController.instance.transform.rotation = Quaternion.Euler(45, 0, 0);
        // }else{
        //     CameraController.instance.transform.rotation = Quaternion.Euler(90, 0, 0);
        // }
    }
    public override string GetString(){
        return "TwoD";
    }
}