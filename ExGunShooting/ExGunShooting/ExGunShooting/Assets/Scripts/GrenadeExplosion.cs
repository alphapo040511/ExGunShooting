using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{
    private float explosionTimer = 5;
    public bool work = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(work == true)
        {
            explosionTimer -= Time.deltaTime;
        }

        if(explosionTimer <=0 )
        {
            Explosion();
        }
    }

    private void Explosion()
    {
        Debug.Log("Boom");
    }
}
