using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class CameraHider: MonoBehaviour{
    public List<CameracDirection> hiddenDirections;
    private MeshRenderer mesh;
    private bool fadeOut = false;
    public int fadeSpeed = 5;
    public void Start() {
        mesh = GetComponent<MeshRenderer>();
    }
    public void OnCameraChange(CameracDirection newDirection){
        if (hiddenDirections.Contains(newDirection)){
            fadeOut = true;
            return;
        }
        fadeOut = false;
    }
    public void Update(){
        if (!fadeOut && mesh.material.color.a < 1){
            for (int i = 0; i < mesh.materials.Length; i++){
                Color c = mesh.materials[i].color;
                Debug.Log("FadeIN " + c.a);
                float fadeAmount = c.a + fadeSpeed*Time.deltaTime;
                c = new Color(c.r, c.g, c.b, fadeAmount);
                mesh.materials[i].color = c;
            }
        }else if (fadeOut && mesh.material.color.a > 0){
           for (int i = 0; i < mesh.materials.Length; i++){
                Color c = mesh.materials[i].color;
                Debug.Log("FadeOUT " + c.a);
                float fadeAmount = c.a - fadeSpeed*Time.deltaTime;
                c = new Color(c.r, c.g, c.b, fadeAmount);
                mesh.materials[i].color = c;
            }
        }
    }
}