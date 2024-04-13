using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float xRotate, yRotate, xRotateMove, yRotateMove;
    public float rotateSpeed = 500.0f;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //transform.rotation = Quaternion.Euler(0, 0, 0);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);


        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;
        yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;


        yRotate = transform.eulerAngles.y + yRotateMove;
        xRotate = transform.eulerAngles.x + xRotateMove;

        transform.rotation = Quaternion.Euler(xRotate, yRotate, 0);
    }
}
