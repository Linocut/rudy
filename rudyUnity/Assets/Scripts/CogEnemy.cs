using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogEnemy : MonoBehaviour
{
    private float distance;
    public float speed;
    public GameObject player;
    public GameObject text;
    private int coggieHits;
    public ParticleSystem smokeEffect;
    public ParticleSystem damageEffect;
    public GameObject cogPickup;

    Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        if (coggieHits >= 3)
        {
            Fix();
        }

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
    public void Fix()
    {
        TextController textcounter = text.gameObject.GetComponent<TextController>();
        textcounter.CoggieChange();
        gameObject.SetActive(false);
        Instantiate(cogPickup, rigidbody2d.position, Quaternion.identity);

        //rigidbody2D.simulated = false;
        //optional if you added the fixed animation


    }
    public void Hit()
    {
        coggieHits = coggieHits + 1;
        smokeEffect.Play();
        Debug.Log(coggieHits);
        if (coggieHits == 2)
        {
            damageEffect.Play();
        }
    }
}
