using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public GameObject player;
    private bool throwing = false;
    public Rigidbody rb;
    public float throwSpeed = 200f;
    private float granadeY = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = new Vector3(player.transform.position.x + 0.36f, player.transform.position.y - granadeY, player.transform.position.z + 0.52f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            granadeY = 0.1f;
            transform.position = new Vector3(transform.position.x, player.transform.position.y - granadeY, transform.position.z);
        }


        if(Input.GetMouseButtonUp(0) && throwing == false)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            transform.GetChild(0);
            transform.GetComponentInChildren<GrenadeExplosion>().work = true;
            ThrowAction();
            throwing = true;
        }
    }

    public void ThrowAction()
    {
        gameObject.transform.SetParent(null);
        transform.rotation = Quaternion.Euler(player.transform.eulerAngles.x , player.transform.eulerAngles.y, player.transform.eulerAngles.z);
        rb.AddRelativeForce(Vector3.forward * 250f * throwSpeed * Time.deltaTime);
        rb.AddRelativeForce(Vector3.up * 250f * throwSpeed * Time.deltaTime);
    }
}
