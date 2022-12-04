using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launcher : MonoBehaviour
{
    public GameObject ruby;
    public GameObject cogProjectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Shoot()
    {
        RubyController rubyScript = ruby.gameObject.GetComponent<RubyController>();
        GameObject go = Instantiate(cogProjectile.gameObject, transform.position, Quaternion.identity);
        Projectile projectile = cogProjectile.gameObject.GetComponent<Projectile>();
        projectile.Launch(rubyScript.lookDirection, 300f);
    }
}
