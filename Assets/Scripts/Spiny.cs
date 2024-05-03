using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Spiny : MonoBehaviour
{
    public float speed;
    private SpriteRenderer rend;
    private Animator animator;
    [SerializeField] private Vector2 direction = Vector2.left;
    //* for collisionenter2d
    //[SerializeField] private float collisionThreshold = 0.1f;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInFront();
        CheckHoles();
        moveDirection(direction);
    }
    public void moveDirection(Vector2 direction)
    {
        Vector2 movement = direction;
        rend.flipX = movement.x > 0;
        movement *= speed * Time.deltaTime;
        transform.Translate(movement);
    }
    //* trigger version (needs trigger on walls)
    // void OnTriggerEnter2D(Collider2D other){
    //     direction *= -1;
    // }
    void CheckHoles(){
        Vector2 offset = Vector2.left * 0.5f;
        if (direction.x > 0){
            offset = Vector2.left * -0.5f;
        }
        Vector2 origin = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin + offset, -Vector2.up, 2);
        if (hit.collider is null)
        {
            direction *= -1;
        }
    }
        //* test for clission with raycast    
        // Vector2 offset = Vector2.up * 0.2f;
        // Vector2 front = Vector2.left;
        // if (direction == 1){
        //     front = -Vector2.left;
        // }
        // Vector2 origin = transform.position;
        // RaycastHit2D hit = Physics2D.Raycast(origin+offset, front, 1);
        // if (hit.collider != null)
        // {
        //     direction *= -1;
        // }
    //* Collision version    
    // void OnCollisionEnter2D(Collision2D collision){
    //     if(collision.transform.position.y - transform.position.y >= collisionThreshold){
    //         direction *=-1;
    //     }
    // }
    void CheckInFront(){
        Vector2 origin = transform.position;
        origin.y +=0.3f;
        origin.x += 0.5f * direction.x;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, 0.1f);
        if (hit.collider != null){
            direction *=-1;
        }  
    }
}
