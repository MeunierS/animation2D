using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public class InputActionMap : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputAction move;
    public float speed;
    private SpriteRenderer rend;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        move = inputActions.FindActionMap("Player").FindAction("Move");
        rend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(move.ReadValue<float>(), 0);
        if (movement.x ==0){
            animator.SetBool("isMoving", false);
        }
        else{
            animator.SetBool("isMoving", true);
            rend.flipX = movement.x < 0;
            movement *= speed * Time.deltaTime;
            transform.Translate(movement);
        }
    }
    void OnEnable(){
        inputActions.FindActionMap("Player").Enable();
    }
    void OnDisable(){
        inputActions.FindActionMap("Player").Disable();
    }
}
