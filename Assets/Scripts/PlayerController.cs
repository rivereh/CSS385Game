using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    int speed;
    int walkSpeed = 4;
    int runSpeed = 5;
    int jumpForce = 10;

    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        float input = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(input, 0, 0) * Time.deltaTime * speed;

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode2D.Impulse);
        }

        if (input > 0)
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        if (input < 0)
            gameObject.transform.localScale = new Vector3(-1, 1, 1);

        anim.SetBool("Input", input != 0);
    }
}
