using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public GameObject projectilePrefab;
    private Rigidbody2D rigidbody2d;
    /*
   public GameObject projectilePrefab;
   
    public Vector2 direction;
    public float speed = 2;
    private Vector2 velocity;
    public GameObject ruby;

    void update()
    {
        
    }
    void Awake()
    {
        
    }
    public void Launch(Vector2 direction, float force)
    {
        Debug.Log(direction * force);
        Debug.Log(transform);
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.AddForce(direction * force);
    }


    
    
    void FixedUpdate()
    {
        RubyController looking = ruby.gameObject.GetComponent<RubyController>();
        direction = looking.lookDirection;
        velocity = direction * speed;
        Destroy(gameObject, 5);
        Vector2 pos = transform.position;
        pos += velocity * Time.fixedDeltaTime;
        transform.position = pos;
        Debug.Log(pos + " and " + velocity + " and " + direction + " and " + (direction*speed + " and " + looking.lookDirection)) ;

    }
    */


    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }
    
    public void LaunchTwo(Vector2 direction, float force)
    {
        GameObject cogTwo = Instantiate(projectilePrefab, rigidbody2d.position +  Vector2.right * 1f, Quaternion.identity);
        cogTwo.GetComponent<Rigidbody2D>().AddForce(direction * (force ));

        GameObject cogThree = Instantiate(projectilePrefab, rigidbody2d.position +  Vector2.left * 1f, Quaternion.identity);
        cogThree.GetComponent<Rigidbody2D>().AddForce(direction * (force));

        GameObject cogFour = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 1f, Quaternion.identity);
        cogFour.GetComponent<Rigidbody2D>().AddForce(direction * (force));

        GameObject cogFive = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.down * 1f, Quaternion.identity);
        cogFive.GetComponent<Rigidbody2D>().AddForce(direction * (force));

        rigidbody2d.AddForce(direction * force);
    }

    
    void Update()
    {
        if(transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }
    

    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if (e != null)
        {
            e.Fix();
        }
        HardEnemyController he = other.collider.GetComponent<HardEnemyController>();
        if (he != null)
        {
            he.Fix();
        }
        CogEnemy ce = other.collider.GetComponent<CogEnemy>();
        if (ce != null)
        {
            ce.Hit();

            
        }

        Destroy(gameObject);
    }
}