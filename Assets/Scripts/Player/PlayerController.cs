using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PlayerState {
     //Idle,
     Walking,
     Attacking,
}
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private CharacterController controller;
    private Animator animator;
    public GameObject swordAttack;
    private float walkSpeed = 12;
    private float sprintSpeed = 24;
    private bool sprinting = false;
    private bool frozen = false;
    private float damageCooldownMax = 3;
    private float damageCooldown = 0;
    public Vector3 projectilePos = new Vector3(0, 0, -1);
    private Vector3 gravity = new Vector3(0, -25f, 0);
    private PlayerState playerState;
    //private string currentState;
    void Awake(){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else{
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponentInChildren<CharacterController>();
        playerState = PlayerState.Walking;
        swordAttack.SetActive(false);
        //startRotation = sprite.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
        UpdatePlayerState();
        //sprite.transform.rotation = startRotation;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.collider.tag == "Enemy"){
            TakeDamage(1);
        }
    }
    public void GetPlayerInput(){
        if (frozen) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && playerState != PlayerState.Attacking){
            //StartCoroutine(Attack());
            animator.SetBool("attacking", true);
            playerState = PlayerState.Attacking;
            swordAttack.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Space)){
            animator.SetBool("attacking", false);
            playerState = PlayerState.Walking;
            swordAttack.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            sprinting = true;
            //Debug.Log("Started sprinting");
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)){
            sprinting = false;
            //Debug.Log("Stopped sprinting");
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            //Debug.Log("Item 0: " + ItemManager.instance.GetUsedItem(0));
            ItemManager.instance.UseItem(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            //Debug.Log("Item 1: " + ItemManager.instance.GetUsedItem(1));
            ItemManager.instance.UseItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)){
            //Debug.Log("Item 2: " + ItemManager.instance.GetUsedItem(2));
            ItemManager.instance.UseItem(2);
        }
        if (Input.GetKeyDown(KeyCode.Tab)){
            ItemManager.instance.ShowMenu();
        }
        UpdateAnimationsAndMove();
    }
    private void UpdatePlayerState(){
        damageCooldown -= Time.deltaTime;
        if (damageCooldown < 0){
            damageCooldown = 0;
        }
        controller.Move(gravity*Time.deltaTime);
    }


    /*
    private IEnumerator Attack() {
        animator.SetBool("attacking", true);
        playerState = PlayerState.Attacking;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.33f);
        playerState = PlayerState.Walking;
    }*/
    public void UpdateAnimationsAndMove(){
        Vector2 input = Vector2.zero; 
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        input = Vector2.ClampMagnitude(input, 1);
        
        if (input != Vector2.zero){
            
            Vector3 forward = CameraController.instance.transform.forward;
            Vector3 right = CameraController.instance.transform.right;
            
            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();
            

            MovePlayer((forward*input.y + right*input.x)*Time.deltaTime*(sprinting ? sprintSpeed : walkSpeed));

            animator.SetFloat("MoveX", input.x);
            animator.SetFloat("MoveY", input.y);
            animator.SetBool("Walking", true);

            if (input.x < 0){
                input.x--;
            }
            if (input.y < 0){
                input.y--;
            }
            projectilePos = new Vector3 (Mathf.Ceil(input.x), 0, Mathf.Ceil(input.y)) + transform.position;
            swordAttack.transform.position = projectilePos;
        }else{
            animator.SetBool("Walking", false);
        }
    }
    public void MovePlayer(Vector3 direction){
        controller.Move(direction);
    }
    public void RotateRight(){
        float x = animator.GetFloat("MoveX");
        float y = animator.GetFloat("MoveY");

        if (x > 0){
            animator.SetFloat("MoveY", -1);
            animator.SetFloat("MoveX", 0);
        }
        else if (x < 0){
            animator.SetFloat("MoveY", 1);
            animator.SetFloat("MoveX", 0);
        }
        else if (y > 0){
            animator.SetFloat("MoveY", 0);
            animator.SetFloat("MoveX", 1);
        }
        else if (y < 0){
            animator.SetFloat("MoveY", 0);
            animator.SetFloat("MoveX", -1);
        }else{//if not moved at all
            animator.SetFloat("MoveY", 0);
            animator.SetFloat("MoveX", -1);
        }
    }
    public void RotateLeft(){
        float x = animator.GetFloat("MoveX");
        float y = animator.GetFloat("MoveY");

        if (x < 0){
            animator.SetFloat("MoveY", -1);
            animator.SetFloat("MoveX", 0);
        }
        else if (x > 0){
            animator.SetFloat("MoveY", 1);
            animator.SetFloat("MoveX", 0);
        }
        else if (y < 0){
            animator.SetFloat("MoveY", 0);
            animator.SetFloat("MoveX", 1);
        }
        else if (y > 0){
            animator.SetFloat("MoveY", 0);
            animator.SetFloat("MoveX", -1);
        }else{//if not moved at all
            animator.SetFloat("MoveY", 0);
            animator.SetFloat("MoveX", 1);
        }
    }

    //Freeze player control and stop animations
    public void FreezeControl(){
        animator.StartPlayback(); //yes this freezes the animations, yes its name is *correct*
        frozen = true;
    }
    //Resume player control and start animations
     public void UnfreezeControl(){
        animator.StopPlayback();
        frozen = false;
    }
    public void TakeDamage(int damage){
        if (damageCooldown > 0){
            return;
        }
        PlayerStats.instance.TakeDamage(damage);
        damageCooldown = damageCooldownMax;
    }
    public void SetPostition(Vector3 newPos){
        controller.Move(newPos - transform.position);
    }
}
