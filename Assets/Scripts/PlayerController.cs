using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    int speed = 5;
    [SerializeField]
    float punchCooldown = 0.3f;
    float punchCooldownTimer;

    bool isDucking = false;

    [SerializeField]
    Sprite sprite;

    [SerializeField]
    GameObject arm;

    bool hasReleasedJumpButton = true;

    [SerializeField]
    int jumpForce = 300;
    new Rigidbody2D rigidbody;

    [SerializeField]
    Transform feet;

    [SerializeField]
    float groundRadius = 0.2f;

    [SerializeField]
    LayerMask groundLayer;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandlePunch();
        SetCameraPosition();
        HandleDucking();
        CheckPlayerDirection();
        float movementX = Input.GetAxisRaw("Horizontal");

        transform.Translate(speed * Time.deltaTime * new Vector2(movementX, 0).normalized);

        //Debug.Log(movementX);
        //Debug.Log(rigidbody.velocity.x);


        // rigidbody.velocity = new Vector2(rigidbody.velocity.x + movementX * 5 * Time.deltaTime, rigidbody.velocity.y);

        /*
        if(Input.GetAxisRaw("Jump") > 0)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y + speedPerTick * 3);
        }
        */

        // bool isTouchingGround = Physics2D.OverlapCircle(GetFootPosition(), groundRadius, groundLayer);
        bool isTouchingGround = Physics2D.OverlapBox(GetFootPosition(), GetFootSize(), 0, groundLayer);

        if(Input.GetAxisRaw("Jump") > 0 && isTouchingGround && hasReleasedJumpButton)
        {
            rigidbody.AddForce(Vector2.up * jumpForce);
            hasReleasedJumpButton = false;

            //rigidbody.velocity = new Vector3(0, 5, 0);
        }
        

        if(Input.GetAxisRaw("Jump") == 0)
        {
            hasReleasedJumpButton = true;
        }
    }

    private Vector2 GetFootPosition()
    {
        float height1 = GetComponent<Collider2D>().bounds.extents.y;
        float height = GetComponent<Collider2D>().bounds.size.y;
        return transform.position + Vector3.down * height / 2;
    }
    private Vector2 GetFootSize()
    {
        return new(GetComponent<Collider2D>().bounds.size.x * 0.9f, 0.1f);
    }
    private void SetCameraPosition()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.transform.position = new(transform.position.x, 0, -10);
    }
    private void CheckPlayerDirection()
    {
        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            if(transform.childCount == 0)
            {
                transform.localScale = new(1, gameObject.transform.localScale.y);
            }
        }
        else if(Input.GetAxisRaw("Horizontal") < 0)
        {
            if(transform.childCount == 0)
            {
                transform.localScale = new(-1, gameObject.transform.localScale.y);
            }
        }
    }
    private void HandlePunch()
    {
        punchCooldownTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E) && punchCooldownTimer <= 0)
        {
            Instantiate(arm, transform);
            punchCooldownTimer = punchCooldown;
        }
    }
    private void HandleDucking()
    {
        // duckFactor is how much of the sprites original height the sprite will have when ducking
        float duckFactor = 0.3f;
        Vector3 movement = new();
        if(transform.childCount == 0)
        {
            if(Input.GetKey(KeyCode.S) && !isDucking)
            {
                speed = 3;
                transform.localScale = new(transform.localScale.x, duckFactor, 1);
                movement = new(0, -sprite.bounds.size.y * (1 - duckFactor));
                isDucking = true;
            }
            else if (!Input.GetKey(KeyCode.S) && isDucking)
            {
                speed = 5;
                transform.localScale = new(transform.localScale.x, 1, 1);
                movement = new(0, sprite.bounds.size.y * (1 - duckFactor));
                isDucking = false;
            }
            transform.Translate(movement);
        }
    }
    private void OnDrawGizmosSelected() {

        // Gizmos.DrawCube(Vector2.zero, Vector2.one * 5);
        if (feet)
        {
            Gizmos.DrawWireCube(GetFootPosition(), GetFootSize());
            // Gizmos.DrawWireSphere(GetFootPosition(), groundRadius);
        }
    }
}
