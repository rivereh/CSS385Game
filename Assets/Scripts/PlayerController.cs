using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] int walkSpeed = 4;
    [SerializeField] int runSpeed = 5;
    [SerializeField] int jumpForce = 10;
    [SerializeField] GameObject powerFX;
    [SerializeField] Transform manabar;
    [SerializeField] Transform healthbar;
    public TextMeshProUGUI coinsText;

    AudioSource audioSource;
    public AudioClip jumpSound;


    // [SerializeField] Transform hitBox;
    // [SerializeField] LayerMask hit_LayerMask;

    public int mana = 100;
    public int health = 100;
    // bool canSpecial = true;
    [HideInInspector] public bool dead = false;
    int speed;
    Rigidbody2D rb;
    Animator anim;

    PlayerStats stats;
    public GameObject deathText;

    public SpriteRenderer spriteRenderer;
    Material originalMaterial;
    public Material flashMaterial;
    Coroutine flashSprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        stats = GetComponent<PlayerStats>();
        UpdateAnimatorController();
        audioSource = GetComponent<AudioSource>();
        originalMaterial = spriteRenderer.material;
    }

    void Update()
    {

        if (!dead && !MenuManager.instance.pausedGame)
        {
            // adjust speed between walk and run whether shift is being held
            speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
            
            // capture horizontal movement and apply it to transform's position
            float input = Input.GetAxisRaw("Horizontal");
            transform.position += new Vector3(input, 0, 0) * Time.deltaTime * speed;

            // // allow player to jump if grounded
            if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.001f)
            {
                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode2D.Impulse);
                audioSource.clip = jumpSound;
                audioSource.Play();
            }

            // flip sprite based on whether they are moving left or right
            if (input > 0)
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            if (input < 0)
                gameObject.transform.localScale = new Vector3(-1, 1, 1);

            anim.SetBool("Input", input != 0);
           
            // anim.SetBool("Attack", Input.GetMouseButtonDown(0) || (Input.GetMouseButtonDown(1) && canSpecial && input == 0));

            // if (Input.GetMouseButtonDown(0))
            // {
            //     anim.SetTrigger("Attackk");
            //     // apply damage
            //     Collider[] hitColliders = Physics.OverlapBox(hitBox.position, hitBox.localScale / 4, Quaternion.identity, hit_LayerMask);
            //     int i = 0;
            //     //Check when there is a new collider coming into contact with the box
            //     while (i < hitColliders.Length)
            //     {
            //         if(hitColliders[i].gameObject.GetComponentInParent<Skeleton>())
            //             hitColliders[i].gameObject.GetComponentInParent<Skeleton>().TakeDamage(10);

            //         i++;
            //     }
            // }

            // power attack TODO: only allow to attack based on having enough mana
            // if (Input.GetMouseButtonDown(1) && mana > 0 && input == 0)
            // {
            //     // handle mana and manabar
            //     mana -= 25;

            //     // apply damage
            //     Collider[] hitColliders = Physics.OverlapBox(hitBox.position, hitBox.localScale / 4, Quaternion.identity, hit_LayerMask);
            //     int i = 0;
            //     //Check when there is a new collider coming into contact with the box
            //     while (i < hitColliders.Length)
            //     {
            //         if(hitColliders[i].gameObject.GetComponentInParent<Skeleton>())
            //             hitColliders[i].gameObject.GetComponentInParent<Skeleton>().TakeDamage(25);

            //         i++;
            //     }


            //     // spawn FX
            //     Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y - 0.5f, 0);
            //     GameObject fx = Instantiate(powerFX, spawnPos, Quaternion.identity) as GameObject;
                
            //     Destroy(fx, 0.3f);
            // }
        }

        if (Input.GetKeyDown(KeyCode.S) && !dead)
            gameObject.layer = 8;
        if (Input.GetKeyUp(KeyCode.S) && !dead)
            gameObject.layer = 9;

        if (health > 100)
            health = 100;

        // if (mana <= 0)
        //     canSpecial = false;
        // else
        //     canSpecial = true;

        if (mana > 100)
            mana = 100;

        UpdateUI();

        if (dead)
            gameObject.layer = 11;
    }

    void UpdateUI()
    {
        float manabarRatio = (float)mana / (float)100;
        manabar.localScale = Vector3.Lerp(manabar.localScale, new Vector3(manabarRatio, 1, 1), Time.deltaTime * 8f);

        float healthbarRatio = (float)health / (float)100;
        healthbar.localScale = Vector3.Lerp(healthbar.localScale, new Vector3(healthbarRatio, 1, 1), Time.deltaTime * 8f);

        coinsText.text = "Coins: " + stats.coins;
    }

    void UpdateAnimatorController()
    {
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Player/Player_" + stats.weapon + "/Player_" + stats.weapon + "_Defence" + stats.defence);
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
        if (dead)
            return;
        float damageToDeal = damage - (3 * PlayerStats.instance.defence);
        if (damageToDeal > 0)
        {
            health -= (damage - (3 * PlayerStats.instance.defence));
        }
        Flash();
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public void Die()
    {
        dead = true;
        anim.SetTrigger("Die");
        GetComponent<PlayerCombat>().enabled = false;
        deathText.SetActive(true);
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    void Flash()
    {
        if (flashSprite != null)
        {
            StopCoroutine(flashSprite);
        }
        flashSprite = StartCoroutine(FlashSprite());
    }

    IEnumerator FlashSprite()
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material = originalMaterial;
        flashSprite = null;
    }
}
