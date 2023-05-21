using UnityEngine;

// Ensure the component is present on the gameobject the script is attached to
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerController : MonoBehaviour
{
    // Local rigidbody variable to hold a reference to the attached Rigidbody2D component
    new Rigidbody2D rigidbody2D;
    new Animator animation;
    SpriteRenderer sprite;
    
    public bool lockControls = false;

    public float movementSpeed;

    void Awake()
    {
        // Setup Rigidbody for frictionless top down movement and dynamic collision
        rigidbody2D = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        rigidbody2D.isKinematic = false;
        rigidbody2D.angularDrag = 0.0f;
        rigidbody2D.gravityScale = 0.0f;
    }

    public void Lock(){
        lockControls = true;
    }

    public void Unlock(){
        lockControls = false;
    }

    void Start(){
        lockControls = false;
    }

    void Update()
    {
        // Handle user input
        Vector2 targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(!lockControls) Move(targetVelocity);
        else rigidbody2D.velocity = targetVelocity * 0;
    }

    void Move(Vector2 targetVelocity)
    {
        // Set rigidbody velocity
        rigidbody2D.velocity = (targetVelocity * movementSpeed); //* Time.deltaTime; // Multiply the target by deltaTime to make movement speed consistent across different framerates
        if(targetVelocity.magnitude != 0)
        {
            animation.Play("Walk");
            if (targetVelocity.x < 0)
                sprite.flipX = true;
            else if (targetVelocity.x > 0)
                sprite.flipX = false;
        }
        else
            animation.Play("Idle");
    }
}