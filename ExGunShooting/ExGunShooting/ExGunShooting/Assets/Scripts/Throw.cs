using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public GameObject player;
    private bool throwing = false;
    public Rigidbody rb;
    public float throwSpeed = 200f;
    private float granadeY = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = new Vector3(player.transform.position.x + 0.34f, player.transform.position.y + granadeY, player.transform.position.z + 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            granadeY = 0.4f;
            transform.position = new Vector3(player.transform.position.x + 0.34f, player.transform.position.y + granadeY, player.transform.position.z + 0.5f);
        }


        if(Input.GetMouseButtonUp(0) && throwing == false)
        {
            throwing = true;
            GetComponent<Rigidbody>().isKinematic = false;
            transform.GetChild(0);
            transform.GetComponentInChildren<GrenadeExplosion>().work = true;
            ThrowAction();
        }
    }

    public void ThrowAction()
    {
        gameObject.transform.SetParent(null);
        transform.rotation = Quaternion.Euler(0, player.transform.eulerAngles.y, 0);
        rb.AddForce(Vector3.forward * 250f * throwSpeed * Time.deltaTime);
        rb.AddForce(Vector3.up *250f * throwSpeed * Time.deltaTime);
    }
}
