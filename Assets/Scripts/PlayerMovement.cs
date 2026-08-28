using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Analytics;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    InputAction moveAction;
    InputAction jumpAction;
    InputAction attackAction;
    public GameObject bulletPrefab;
    [SerializeField] float jumpForce = 6;
    [SerializeField] float acceleration = 5;
    [SerializeField] float maxSpeed = 2;

    float xMovement;
    bool jumpValue;
    bool attackValue;
    Rigidbody2D rb;
    BoxCollider2D bc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        attackAction = InputSystem.actions.FindAction("Attack");
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        print(Grounded());
        xMovement = moveAction.ReadValue<Vector2>().x;
        if (jumpAction.WasPressedThisFrame() && Grounded()){
            // print(jumpValue);
            rb.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
        }
        if (attackAction.WasPressedThisFrame())
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }
    }

    void FixedUpdate()
    {
        if (xMovement != 0)
        {
            rb.linearVelocityX += acceleration*xMovement;
            rb.linearVelocityX = math.clamp(rb.linearVelocityX, -maxSpeed, maxSpeed);
            // print(xMovement);
        }

    }
    bool Grounded()
    {
        return Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y - bc.size.y/2), new Vector2(bc.size.x, 0.1f), 0, Vector2.down, 0.2f);
    }
}
