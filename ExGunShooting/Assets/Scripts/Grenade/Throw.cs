using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public GameObject hand;
    public bool throwing = false;
    public Rigidbody rb;
    public float throwSpeed = 200f;
    public GameObject explosionPrefab;
    public GameObject explosionScript;

    // Start is called before the first frame update
    void Start()
    {
        hand = GameObject.Find("RightHand");
        rb = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = new Vector3(hand.transform.position.x, hand.transform.position.y, hand.transform.position.z);
        GameObject temp = Instantiate(explosionPrefab);
        temp.transform.position = transform.position;
        explosionScript = GameObject.FindGameObjectWithTag("Explosion");
    }

    // Update is called once per frame
    void Update()
    {
        if (throwing == false)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, hand.transform.eulerAngles.y, transform.eulerAngles.z);
        }

        if (Input.GetMouseButtonDown(0) && throwing == false)
        {
            transform.position = new Vector3(transform.position.x, hand.transform.position.y + 0.1f , transform.position.z);
        }


        if(Input.GetMouseButtonUp(0) && throwing == false)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            
            if (explosionScript != null)
            {
                explosionScript.GetComponent<GrenadeExplosion>().TurnOn();
            }
            ThrowAction();
            GetComponentInChildren<GrenadeExplosionSound>().Timer();
            throwing = true;
        }
    }

    public void ThrowAction()
    {
        gameObject.transform.SetParent(null);
        rb.velocity = Vector3.zero;
        rb.AddRelativeForce(Vector3.forward * 400f * throwSpeed * Time.deltaTime);
        rb.AddRelativeForce(Vector3.up * 250f * throwSpeed * Time.deltaTime);
    }
}
