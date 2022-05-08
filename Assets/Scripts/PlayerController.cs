using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
<<<<<<< Updated upstream
=======
using TMPro;
using UnityEngine.Events;
>>>>>>> Stashed changes

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] int walkSpeed = 4;
    [SerializeField] int runSpeed = 5;
    [SerializeField] int jumpForce = 10;
    [SerializeField] GameObject powerFX;
    [SerializeField] Transform manabar;
    [SerializeField] Transform healthbar;
<<<<<<< Updated upstream
=======
    public TextMeshProUGUI coinsText;
    [SerializeField] UnityEvent IsLanding;

    // [SerializeField] Transform hitBox;
    // [SerializeField] LayerMask hit_LayerMask;
>>>>>>> Stashed changes

    public int mana = 100;
    public int health = 100;
    bool canSpecial = true;
    bool dead = false;
    int speed;
    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!dead)
        {
            // adjust speed between walk and run whether shift is being held
            speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
            
            // capture horizontal movement and apply it to transform's position
            float input = Input.GetAxisRaw("Horizontal");
            transform.position += new Vector3(input, 0, 0) * Time.deltaTime * speed;

            // allow player to jump if grounded
            if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.001f)
            {
                anim.SetBool("Jumping", true);
                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode2D.Impulse);
            }

            // flip sprite based on whether they are moving left or right
            if (input > 0)
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            if (input < 0)
                gameObject.transform.localScale = new Vector3(-1, 1, 1);

            anim.SetBool("Input", input != 0);
            anim.SetBool("Attack", Input.GetMouseButton(0) || (Input.GetMouseButton(1) && canSpecial && input == 0));

            // power attack TODO: only allow to attack based on having enough mana
            if (Input.GetMouseButtonDown(1) && mana > 0 && input == 0)
            {
                // handle mana and manabar
                mana -= 25;

                // spawn FX
                Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y - 0.5f, 0);
                GameObject fx = Instantiate(powerFX, spawnPos, Quaternion.identity) as GameObject;
                
                Destroy(fx, 0.3f);
            }

            // for testing damage
            if (Input.GetKeyDown(KeyCode.F))
                TakeDamage(25);

        }

        if (health > 100)
            health = 100;

        if (mana <= 0)
            canSpecial = false;
        else
            canSpecial = true;

        if (mana > 100)
            mana = 100;

        UpdateUI();
    }

    public void Landing ()
    {
        anim.SetBool("IsJumping", false);
    }

    void UpdateUI()
    {
        float manabarRatio = (float)mana / (float)100;
        manabar.localScale = Vector3.Lerp(manabar.localScale, new Vector3(manabarRatio, 1, 1), Time.deltaTime * 8f);

        float healthbarRatio = (float)health / (float)100;
        healthbar.localScale = Vector3.Lerp(healthbar.localScale, new Vector3(healthbarRatio, 1, 1), Time.deltaTime * 8f);
    }

    public void AddMana(int manaToAdd)
    {
        mana += manaToAdd;
    }

    public void AddHealth(int healthToAdd)
    {
        health += healthToAdd;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    public void Die()
    {
        dead = true;
        anim.SetTrigger("Die");
    }
}
