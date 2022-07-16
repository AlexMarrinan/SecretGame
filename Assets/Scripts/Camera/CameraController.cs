using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameracDirection{
    Behind = 0,
    Left = 1,
    Infront = 2,
    Right = 3,
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
    const int CAMERA_DISTANCE = 20;
    const float ROTATION_SPEED = 8f;
    public Camera c;
    public CameracDirection cd;
    public Transform player;
    private Vector3 orbit;
    private Vector3 cameraOffset;
    private bool lockX;
    private bool lockZ;
    public bool isTopDown;

    // Start is called before the first frame update
    void Start(){
        c = GetComponent<Camera>();
        cd = CameracDirection.Infront;
        cameraOffset = new Vector3(0, CAMERA_DISTANCE, -CAMERA_DISTANCE);
        orbit = player.transform.position;
        isTopDown = false;
    }

    // Update is called once per frame
    void Update(){
        if (Input.anyKey){
            //TODO: make not horrible !!!
            //Rotate camera left
            if (Input.GetKeyDown(KeyCode.Q)){
                //c.transform.Rotate(0f, 90.0f, 0.0f, Space.World);
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
                //c.transform.Rotate(0.0f, -90.0f, 0.0f, Space.World);
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
            Debug.Log(cd);
        }
        if (isTopDown){
            switch (cd){
                case CameracDirection.Behind: 
                    SetCameraPosition(0, 0, 1);
                    SetCameraRotation(90, 180, 0);
                    break;
                case CameracDirection.Left: 
                    SetCameraPosition(1, 0, 0);
                    SetCameraRotation(90, -90, 0);
                    break;
                case CameracDirection.Right: 
                    SetCameraPosition(-1, 0, 0);
                    SetCameraRotation(90, 90, 0);
                    break;
                case CameracDirection.Infront: 
                    SetCameraPosition(0, 0, -1);
                    SetCameraRotation(90, 0, 0);
                    break;
            }
            SetCameraPosition(0, 1, 0);
        }else{
            switch (cd){
                case CameracDirection.Behind: 
                    SetCameraPosition(0, 1, 1);
                    SetCameraRotation(45, 180, 0);
                    break;
                case CameracDirection.Left: 
                    SetCameraPosition(1, 1, 0);
                    SetCameraRotation(45, -90, 0);
                    break;
                case CameracDirection.Right: 
                    SetCameraPosition(-1, 1, 0);
                    SetCameraRotation(45, 90, 0);
                    break;
                case CameracDirection.Infront: 
                    SetCameraPosition(0, 1, -1);
                    SetCameraRotation(45, 0, 0);
                    break;
            }
        }

        if (!lockX){
            orbit.x = player.transform.position.x;
        }
        orbit.y = player.transform.position.y;
        if (!lockZ){
            orbit.z = player.transform.position.z;
        }
        
        transform.position = Vector3.Lerp(transform.position, orbit + cameraOffset, ROTATION_SPEED * Time.deltaTime);
    }
    public void SetXLock(bool newLock){
        lockX = newLock;
    }
    public void SetZLock(bool newLock){
        lockZ = newLock;
    }
    private void SetCameraPosition(float x, float y, float z){
        cameraOffset = new Vector3(x,y,z)*CAMERA_DISTANCE;
    }
    private void SetCameraRotation(float x, float y, float z){
        transform.rotation =  Quaternion.Lerp(transform.rotation, 
                                            Quaternion.Euler(x, y, z),
                                            ROTATION_SPEED *  Time.deltaTime);
    }
}
