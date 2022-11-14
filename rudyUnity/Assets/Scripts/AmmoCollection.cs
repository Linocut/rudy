using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollection : MonoBehaviour
{
    public AudioClip collectedClipTwo;

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            controller.ChangeCog(4);
            Destroy(gameObject);

            controller.PlaySound(collectedClipTwo);

        }

    }
}
