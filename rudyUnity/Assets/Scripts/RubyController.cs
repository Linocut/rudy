using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;
    
    public int maxHealth = 5;
    
    public GameObject projectilePrefab;
    
    public AudioClip throwSound;
    public AudioClip hitSound;
    
    public int health { get { return currentHealth; }}
    int currentHealth;
    public int currentCog;
    
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    public ParticleSystem healthEffect;
    public ParticleSystem damageEffect;

    public GameObject text;
    public int currentSuperCogs;

    Animator animator;
    public Vector2 lookDirection;
    Vector2 cogTwoDirection = new Vector2(1, 0);

    AudioSource audioSource;

    //public GameObject directionalLine;
    /*public GameObject directionalLineTwo;
    public GameObject directionalLineThree;*/

   // launcher[] guns;
    //bool shoot; 
    
    // Start is called before the first frame update
    void Start()
    {

       // guns = transform.GetComponentsInChildren<launcher>();

        currentSuperCogs = 0;
        currentCog = 4;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        Vector2 move = new Vector2(horizontal, vertical);
        
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
            cogTwoDirection.Set(move.x -30 , move.y - 30);
            cogTwoDirection.Normalize();

        }
        
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        /*

        Vector3 tired = new Vector3(lookDirection.x, lookDirection.y, 0);
        float rotation_z = Mathf.Atan2(tired.y, tired.x) * Mathf.Rad2Deg;
        directionalLine.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);

     
        
        Vector3 tiredTwo = new Vector3(lookDirection.x, lookDirection.y, 0);
        float rotation_zTwo = Mathf.Atan2(tiredTwo.y, tiredTwo.x) * Mathf.Rad2Deg;
        directionalLineTwo.transform.rotation = Quaternion.Euler(0f, 0f, rotation_zTwo + 30f);

        
        Vector3 tiredThree = new Vector3(lookDirection.x, lookDirection.y, 0);
        float rotation_zThree = Mathf.Atan2(tiredThree.y, tiredThree.x) * Mathf.Rad2Deg;
        directionalLineThree.transform.rotation = Quaternion.Euler(0f, 0f, rotation_zThree - 30f);
        */

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        
        if(Input.GetKeyDown(KeyCode.C))
        {
            if (currentCog > 0)
            {
                Launch();
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (currentSuperCogs > 0)
            {
                LaunchTwo();
            }
        }
        /*
        shoot = Input.GetKeyDown(KeyCode.Z);
        if (shoot)
        {
            shoot = false;
            foreach (launcher gun in guns)
            {
                gun.Shoot();
            }
  
        }
        */
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
                TextController textScript = text.gameObject.GetComponent<TextController>();
                if (character != null && textScript.win == true)
                {
                    character.newScene();
                }

            }
        }
    }
    
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
            damageEffect.Play();

            PlaySound(hitSound);
        }
        if (amount > 0)
        {

            healthEffect.Play();

        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
        if (currentHealth == 0)
        {
            TextController textScript = text.gameObject.GetComponent<TextController>();
            textScript.lose();
        }
    }
    public void SuperCogs(int add)
    {
        currentSuperCogs = currentSuperCogs + add; 
    }
    public void ChangeCog(int amount)
    {
        currentCog = currentCog + amount;
        
    }
    
    void Launch()
    {
        currentCog = currentCog - 1;
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
   
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);
        

        animator.SetTrigger("Launch");
        
        PlaySound(throwSound);
    } 
    void LaunchTwo()
    {
        currentSuperCogs = currentSuperCogs - 1;
        GameObject projectileObjectTwo = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectileTwo = projectileObjectTwo.GetComponent<Projectile>();
        projectileTwo.LaunchTwo(lookDirection, 300);
        animator.SetTrigger("Launch");

        PlaySound(throwSound);

    }
    
    
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}