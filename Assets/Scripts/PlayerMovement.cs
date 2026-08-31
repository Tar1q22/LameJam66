using System;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Analytics;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    float xScale;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction attackAction;
    InputAction lookAction;
    public GameObject bulletPrefab;
    public GameObject headSprite;
    private AudioSource audioSource;
    [SerializeField] AudioClip shootSound;

    [SerializeField] float jumpForce = 6;
    [SerializeField] float acceleration = 5;
    [SerializeField] float maxSpeed = 3;
    [SerializeField] float shootCooldown = 0.5f;

    float xMovement;
    bool jumpValue;
    bool attackValue;
    Rigidbody2D rb;
    BoxCollider2D bc;
    float nextShotTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        attackAction = InputSystem.actions.FindAction("Attack");
        lookAction = InputSystem.actions.FindAction("Look");
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        xScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        //print(Grounded());
        xMovement = moveAction.ReadValue<Vector2>().x;
        if (xMovement < 0)
        {
            transform.localScale = new Vector2(-1*xScale, transform.localScale.y);
        }
        else if (xMovement > 0)
        {
            transform.localScale = new Vector2(xScale, transform.localScale.y);
        }
        if (jumpAction.WasPressedThisFrame() && Grounded()){
            // print(jumpValue);
            rb.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
        }
        if (attackAction.WasPressedThisFrame() && Time.time > nextShotTime)
        {
            Instantiate(bulletPrefab, headSprite.transform.position, Quaternion.identity); 
            nextShotTime = Time.time + shootCooldown;
            audioSource.PlayOneShot(shootSound);
        }
        Vector2 dirToMouse = ((Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - (Vector2)transform.position).normalized;
        float mouseDirDeg = (float)Math.Atan2(dirToMouse.y, dirToMouse.x) * Mathf.Rad2Deg;
        headSprite.transform.rotation = Quaternion.Euler(0, 0, mouseDirDeg);
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
