using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameracDirection{
    Behind = 0,
    Left = 1,
    Infront = 2,
    Right = 3
}

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{

    public static CameraController instance;
    void Awake(){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else{
            Destroy(gameObject);
        }
    }
    const int CAMERADIRECTION_MAX = 3;
    const int CAMERA_DISTANCE = 30;
    public Camera c;
    public CameracDirection cd;
    public Transform player;
    public Vector3 cameraOffset;


    // Start is called before the first frame update
    void Start()
    {
        c = GetComponent<Camera>();
        cd = CameracDirection.Infront;
        cameraOffset = new Vector3(0, CAMERA_DISTANCE, -CAMERA_DISTANCE);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey){
            
            //TODO: make not horrible !!!
            //Rotate camera left
            if (Input.GetKeyDown(KeyCode.Q)){
                c.transform.Rotate(0f, 90.0f, 0.0f, Space.World);
                SpriteRenderer[] sprites = FindObjectsOfType<SpriteRenderer>();
               foreach (SpriteRenderer s in sprites){
                    if (s.tag != "Player"){
                        s.transform.Rotate(0f, 90.0f, 0.0f, Space.World);
                    }
                }
                player.transform.Rotate(0f, 90.0f, 0.0f, Space.World);
                PlayerController.instance.RotateLeft();
                //PlayerController.instance.startRotation = player.transform.rotation;
                cd++;
            }
            //Rotate Camera right
            if (Input.GetKeyDown(KeyCode.E)){
                c.transform.Rotate(0.0f, -90.0f, 0.0f, Space.World);
                SpriteRenderer[] sprites = FindObjectsOfType<SpriteRenderer>();
                foreach (SpriteRenderer s in sprites){
                    if (s.tag != "Player"){
                        s.transform.Rotate(0f, -90.0f, 0.0f, Space.World);
                    }
                }
                player.transform.Rotate(0f, -90.0f, 0.0f, Space.World);
                PlayerController.instance.RotateRight();
                //PlayerController.instance.startRotation = player.transform.rotation;
                cd--;
            }
            if ((int) cd > CAMERADIRECTION_MAX){
                cd = 0;
            }else if (cd < 0){
                cd = (CameracDirection)CAMERADIRECTION_MAX;
            }
            switch (cd){
                case CameracDirection.Behind: 
                    cameraOffset = new Vector3(0, CAMERA_DISTANCE, CAMERA_DISTANCE);
                    break;
                case CameracDirection.Left: 
                    cameraOffset = new Vector3(CAMERA_DISTANCE, CAMERA_DISTANCE, 0);
                    break;
                case CameracDirection.Right: 
                    cameraOffset = new Vector3(-CAMERA_DISTANCE, CAMERA_DISTANCE, 0);
                    break;
                case CameracDirection.Infront: 
                    cameraOffset = new Vector3(0, CAMERA_DISTANCE, -CAMERA_DISTANCE);
                    break;
            }
        }

        transform.position = player.transform.position + cameraOffset;
    }
}
