using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class InputActionMap : MonoBehaviour
{
    Rigidbody2D rb;
    public InputActionAsset inputActions;
    private InputAction move;
    private InputAction jump;
    public float speed;
    private SpriteRenderer rend;
    private Animator animator;
    private bool isGrounded;
    private bool isJumping;
    [SerializeField] private float jumpHeight = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        move = inputActions.FindActionMap("Player").FindAction("Move");
        jump = inputActions.FindActionMap("Player").FindAction("Jump");
        jump.performed += ctx => {OnJump(ctx);};
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
        Vector2 origin = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 0.02f);
        isGrounded = hit.collider != null;
        isJumping = !isGrounded && isJumping;
        animator.SetBool("isJumping", !isGrounded);
    }
    void OnEnable(){
        inputActions.FindActionMap("Player").Enable();
    }
    void OnDisable(){
        inputActions.FindActionMap("Player").Disable();
    }
    public void OnJump(InputAction.CallbackContext ctx){
        if (!isGrounded){
            return;
        }
        rb.velocity += jumpHeight * Vector2.up;
        isJumping = true;
    }
}
