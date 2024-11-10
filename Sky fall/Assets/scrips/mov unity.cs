using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 100f;
    public float jumpForce = 5f;
    public int maxJumps = 2;

    private Rigidbody rb;
    public Animator anima;

    private int jumpCount = 0;
    private bool canJump = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Maneja la rotación del personaje
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Rotar al personaje
        transform.Rotate(0f, horizontalInput * rotationSpeed * Time.deltaTime, 0f);

        // Manejo del salto
        if (Input.GetButtonDown("Jump") && (canJump || jumpCount < maxJumps))
        {
            PerformJump();
            anima.SetBool("salto", true);
        }

        // Actualizar animación de correr
        if (verticalInput != 0)
        {
            anima.SetFloat("run", Mathf.Abs(verticalInput));
        }
        else
        {
            anima.SetFloat("run", 0);
        }
    }

    void FixedUpdate()
    {
        // Maneja el movimiento del personaje
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * moveSpeed * verticalInput;
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);

        // Aplicar gravedad extra si el personaje está cayendo
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * 2f * Time.fixedDeltaTime;
        }
    }

    private void PerformJump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reset velocidad vertical
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        jumpCount++;

        if (jumpCount >= maxJumps)
        {
            canJump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("floor"))
        {
            canJump = true;
            jumpCount = 0;
            anima.SetBool("salto", false);
        }
    }
}