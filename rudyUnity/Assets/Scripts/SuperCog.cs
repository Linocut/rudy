using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperCog : MonoBehaviour
{

    public AudioClip collectedClipTwo;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.SuperCogs(1);
            Destroy(gameObject);
            player.PlaySound(collectedClipTwo);
        }
    }
}
