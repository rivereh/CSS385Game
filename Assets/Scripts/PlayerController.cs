using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] int walkSpeed = 4;
    [SerializeField] int runSpeed = 5;
    [SerializeField] int jumpForce = 10;
    [SerializeField] GameObject powerFX;
    [SerializeField] Transform manabar;

    public int mana = 100;

    int speed;
    bool canSpecial;
    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canSpecial = true;
    }

    void Update()
    {
        // adjust speed between walk and run whether shift is being held
        speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        
        // capture horizontal movement and apply it to transform's position
        float input = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(input, 0, 0) * Time.deltaTime * speed;

        // allow player to jump if grounded
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode2D.Impulse);
        }

        // flip sprite based on whether they are moving left or right
        if (input > 0)
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        if (input < 0)
            gameObject.transform.localScale = new Vector3(-1, 1, 1);

        anim.SetBool("Input", input != 0);
        anim.SetBool("Attack", Input.GetMouseButton(0) || (Input.GetMouseButton(1) && canSpecial));

        // power attack TODO: only allow to attack based on having enough mana
        if (Input.GetMouseButtonDown(1) && mana > 0)
        {
            // handle mana and manabar
            mana -= 25;
            
            
            // spawn FX
            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y - 0.5f, 0);
            GameObject fx = Instantiate(powerFX, spawnPos, Quaternion.identity) as GameObject;
            
            Destroy(fx, 0.3f);
        }

        if (mana <= 0)
            canSpecial = false;
        if (mana > 100)
            mana = 100;

        UpdateManabar();
    }

    void UpdateManabar()
    {
        float manabarRatio = (float)mana / (float)100;
        manabar.localScale = Vector3.Lerp(manabar.localScale, new Vector3(manabarRatio, 1, 1), Time.deltaTime * 8f);
    }

    public void AddMana(int manaToAdd)
    {
        mana += manaToAdd;
    }
}
