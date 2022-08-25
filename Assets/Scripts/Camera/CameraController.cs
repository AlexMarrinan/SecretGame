using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    public int CAMERA_DISTANCE = 20;
    public int CAMERA_ANGLE = 45;
    public float ROTATION_SPEED;
    public float TRAVEL_SPEED;
    public Camera c;
    public CameracDirection cd;
    public Transform player;
    private Vector3 orbit;
    private Vector3 cameraOffset;
    private bool lockX;
    private bool lockZ;
    public bool isTopDown;
    public bool is2d;
    public bool isSideScroll;

    // Start is called before the first frame update
    void Start(){
        c = GetComponent<Camera>();
        cd = CameracDirection.Infront;
        cameraOffset = new Vector3(0, CAMERA_DISTANCE, -CAMERA_DISTANCE);
        orbit = player.transform.position;
        isTopDown = false;
        is2d = false;
    }

    // Update is called once per frame
    void Update(){
        if (Input.anyKey){
            //TODO: make not horrible !!!
            //Rotate camera left
            bool movedCamera = false;
            if (Input.GetKeyDown(KeyCode.Q)){
                //c.transform.Rotate(0f, 90.0f, 0.0f, Space.World);
                PlayerController.instance.RotateLeft();
                //PlayerController.instance.startRotation = player.transform.rotation;
                cd++;
                movedCamera = true;
            }
            //Rotate Camera right
            if (Input.GetKeyDown(KeyCode.E)){
                //c.transform.Rotate(0.0f, -90.0f, 0.0f, Space.World);
                PlayerController.instance.RotateRight();
                //PlayerController.instance.startRotation = player.transform.rotation;
                cd--;
                movedCamera = true;

            }
            if ((int) cd > CAMERADIRECTION_MAX){
                cd = 0;
            }else if (cd < 0){
                cd = (CameracDirection)CAMERADIRECTION_MAX;
            }
            if (movedCamera){
                FindObjectsOfType<CameraHider>().ToList().ForEach(hider => hider.OnCameraChange(cd));
            }
            //Checks every CameraHider too see if it should be visible
            //Debug.Log(cd);
        }
        if (isTopDown){
            //TODO: MAKE PLAYER MOVMENT WORK WHEN TOPDOWN !!!
            switch (cd){
                case CameracDirection.Behind: 
                    SetCameraPosition(0, 0, 1);
                    SetCameraRotation(90, 180, 0);
                    SetSpriteRotations(180);
                    break;
                case CameracDirection.Left: 
                    SetCameraPosition(1, 0, 0);
                    SetCameraRotation(90, -90, 0);
                    SetSpriteRotations(-90);
                    break;
                case CameracDirection.Right: 
                    SetCameraPosition(-1, 0, 0);
                    SetCameraRotation(90, 90, 0);
                    SetSpriteRotations(90);
                    break;
                case CameracDirection.Infront: 
                    SetCameraPosition(0, 0, -1);
                    SetCameraRotation(90, 0, 0);
                    SetSpriteRotations(0);
                    break;
            }
            SetCameraPosition(0, 1, 0);
        } else if (is2d){
            //TODO: MAKE PLAYER MOVMENT WORK WHEN TOPDOWN !!!
            switch (cd){
                case CameracDirection.Behind: 
                    SetCameraPosition(0, 0.4f, 3);
                    SetCameraRotation(0, 180, 0);
                    SetSpriteRotations(180);
                    break;
                case CameracDirection.Left: 
                    SetCameraPosition(3, 0.4f, 0);
                    SetCameraRotation(0, -90, 0);
                    SetSpriteRotations(-90);
                    break;
                case CameracDirection.Right: 
                    SetCameraPosition(-3, 0.4f, 0);
                    SetCameraRotation(0, 90, 0);
                    SetSpriteRotations(90);
                    break;
                case CameracDirection.Infront: 
                    SetCameraPosition(0, 0.4f, -3);
                    SetCameraRotation(0, 0, 0);
                    SetSpriteRotations(0);
                    break;
            }
        }else{
            switch (cd){
                case CameracDirection.Behind: 
                    SetCameraPosition(0, 1, 1);
                    SetCameraRotation(CAMERA_ANGLE, 180, 0);
                    SetSpriteRotations(180);
                    break;
                case CameracDirection.Left: 
                    SetCameraPosition(1, 1, 0);
                    SetCameraRotation(CAMERA_ANGLE, -90, 0);
                    SetSpriteRotations(-90);
                    break;
                case CameracDirection.Right: 
                    SetCameraPosition(-1, 1, 0);
                    SetCameraRotation(CAMERA_ANGLE, 90, 0);
                    SetSpriteRotations(90);
                    break;
                case CameracDirection.Infront: 
                    SetCameraPosition(0, 1, -1);
                    SetCameraRotation(CAMERA_ANGLE, 0, 0);
                    SetSpriteRotations(0);
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
        
        transform.position = Vector3.Lerp(transform.position, orbit + cameraOffset, TRAVEL_SPEED * Time.deltaTime);
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
        SetTransformRotation(transform, x, y, z);
    }
    private void SetSpriteRotations(float y){
        SpriteRenderer[] sprites = FindObjectsOfType<SpriteRenderer>();
        foreach (SpriteRenderer s in sprites){
            if (s.tag != "Player" && s.tag != "NonRotate"){
                SetTransformRotation(s.transform, 60, y, 0);
            }
        }
        SetTransformRotation(player, 0, y, 0);
    }

     private void SetTransformRotation(Transform trans, float x, float y, float z){
        trans.rotation = Quaternion.Lerp(trans.rotation,
                                Quaternion.Euler(x, y, z),
                                ROTATION_SPEED *  Time.deltaTime);
     }
}
