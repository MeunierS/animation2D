using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Spiny : MonoBehaviour
{
    public float speed;
    private SpriteRenderer rend;
    private Animator animator;
    public float direction;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckHoles();
        moveDirection(direction);
    }
    public void moveDirection(float direction)
    {
        Vector2 movement = new Vector2(direction, 0);
        rend.flipX = movement.x > 0;
        movement *= speed * Time.deltaTime;
        transform.Translate(movement);
    }
    void OnTriggerEnter2D(Collider2D other){
        direction *= -1;
    }
    void CheckHoles(){
        Vector2 offset = Vector2.left * 0.5f;
        if (direction == 1){
            offset = Vector2.left * -0.5f;
        }
        Vector2 origin = transform.position ;
        RaycastHit2D hit = Physics2D.Raycast(origin + offset, -Vector2.up);
        if (hit.collider is null)
        {
            direction *= -1;
        }
    }
}
