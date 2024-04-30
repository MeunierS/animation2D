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
        moveDirection(direction);
    }
    public void moveDirection(float direction)
    {
        Vector2 movement = new Vector2(direction, 0);
        rend.flipX = movement.x > 0;
        movement *= speed * Time.deltaTime;
        transform.Translate(movement);
    }
    void OnCollisionEnter2D(Collision2D other){
        direction *= -1;
    }
}
