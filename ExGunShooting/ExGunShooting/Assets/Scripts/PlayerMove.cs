using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    public float force = 320f;
    public Rigidbody rb;
    public GameObject playerCamera;
    private float yRotate;
    public int jumpCount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
            
        transform.rotation = Quaternion.Euler(0 , playerCamera.transform.eulerAngles.y, 0);

        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }


        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }


        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
        }


        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }

        if(Input.GetKeyDown(KeyCode.Space) && jumpCount == 1)
        {
            rb.AddForce(Vector3.up * 100f * force * Time.deltaTime);
            jumpCount -= 1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
            if (collision.gameObject.CompareTag("Ground") && jumpCount == 0)
        {
            jumpCount += 1;
        }

    }
}
