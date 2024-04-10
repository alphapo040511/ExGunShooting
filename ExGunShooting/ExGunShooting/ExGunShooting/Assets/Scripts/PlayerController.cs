using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public float jForce = 7f;
    public float gravity = 20f;
    public GameObject playerCamera;

    CharacterController controller;
    Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        moveDir = Vector3.zero;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, playerCamera.transform.eulerAngles.y, 0);

        if (controller.isGrounded)
        {
            if(Input.GetButton("Dash") && Input.GetAxisRaw("Vertical") > 0)
            {
                speed = 6f;
            }
            else if (Input.GetButton("Walk"))
            {
                speed = 1.5f;
            }
            else
            {
                speed = 3f;            
            }

            moveDir = new Vector3(Input.GetAxisRaw("Horizontal") * speed, 0, Input.GetAxisRaw("Vertical") * speed);

            moveDir = transform.TransformDirection(moveDir);

            if(Input.GetButtonDown("Jump")) 
            {
                moveDir.y = jForce;
            }
        }

        moveDir.y -= gravity * Time.deltaTime;

        controller.Move(moveDir * Time.deltaTime);
    }
}
