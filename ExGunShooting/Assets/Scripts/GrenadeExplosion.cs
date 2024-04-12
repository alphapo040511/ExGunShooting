using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{
    private float explosionTimer = 5;
    public bool work = false;
    public GameObject grenade;
    private bool damage = true;

    // Start is called before the first frame update
    void Start()
    {
        work = false;
        grenade = GameObject.FindWithTag("Grenade");
        gameObject.GetComponent<SphereCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(grenade.transform.position.x, grenade.transform.position.y, grenade.transform.position.z);

        if (work == true && explosionTimer >= 0)
        {
            explosionTimer -= Time.deltaTime;
        }

        if (explosionTimer <= 0)
        {
            gameObject.GetComponent<SphereCollider>().enabled = true;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("HitCheck"))
        {
            transform.LookAt(new Vector3(collider.transform.position.x, collider.transform.position.y, collider.transform.position.z));

            Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);

            if (Physics.Raycast(transform.position, Vector3.forward, 50f, 1 << LayerMask.NameToLayer("Field")))
            {
                damage = true;
                Debug.Log("발견");

            
                if (damage == true)
                {
                    Debug.Log("피격");
                    //collider.GetComponent<PlayerHp>().HitDamage();
                }
            }
                
        }
    }

    public void TurnOn()
    {
        work = true;
    }
}
