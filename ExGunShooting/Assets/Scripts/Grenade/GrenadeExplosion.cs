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
        grenade = GameObject.FindGameObjectWithTag("Grenade");
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

        if (explosionTimer <= 0 && work == true)
        {
            gameObject.GetComponent<SphereCollider>().enabled = true;
            work = false;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        RaycastHit hit;
        if (collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("Player"))
        {
            transform.LookAt(new Vector3(collider.transform.position.x, collider.transform.position.y, collider.transform.position.z));

            if (Physics.Raycast(transform.position, transform.forward, out hit, Vector3.Distance(collider.transform.position , transform.position), 1 << LayerMask.NameToLayer("Field")))
            {
                Debug.DrawRay(transform.position, transform.forward * Vector3.Distance(collider.transform.position, transform.position), Color.red);
                if (hit.transform.tag == "Field")
                {
                    damage = false;
                    Debug.Log("방해물 발견");
                }
            }
            else
            {
                damage = true;
                Debug.Log("방해물 없음");
            }

            if (damage == true && collider.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("피격");
                collider.GetComponent<EnemyNormal>().Hp -= 5;
            }

            if (damage == true && collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("피격");
                collider.GetComponent<HpSystem>().hp -= 5;
            }
        }
        GrenadeDestroy();
    }

    public void TurnOn()
    {
        work = true;
    }

    private void GrenadeDestroy()
    {
        Destroy(grenade);
        Destroy(gameObject);
    }
}
