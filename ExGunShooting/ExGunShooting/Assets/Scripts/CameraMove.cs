using Unity.PlasticSCM.Editor.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    private float xRotate, yRotate, xRotateMove, yRotateMove;
    public float rotateSpeed = 500.0f;
    public float gunForce = 20.0f;
    public float coolDown;
    public float coolTime;
    private int ammo;
    public Text ammoUI = null;
    public Text reloadUI = null;
    GameObject obj;
    public GameObject player;
    private bool reloading = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        coolDown = 0.0f;
        coolTime = 0.08f;
        ammo = 30;
        AmmoUI();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;

        coolDown += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;
        yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;

        //xRotate = Mathf.Clamp(xRotate, -80, 80); // 위, 아래 고정

        yRotate = transform.eulerAngles.y + yRotateMove;
        xRotate = transform.eulerAngles.x + xRotateMove;
        //xRotate = xRotate + xRotateMove;

        //xRotate = Mathf.Clamp(xRotate, -85, 85);

        transform.rotation = Quaternion.Euler(xRotate, yRotate, 0);


        if (Input.GetMouseButton(0) && coolDown >= coolTime && ammo > 0 && reloading == false) // 클릭한 경우
        {
            Ray shoot = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(shoot, out hit))
            {
                string EnemyName = hit.collider.name;
                obj = GameObject.Find(EnemyName);


                if (hit.collider.tag == "Enemy")
                {
                    if (obj != null)
                    {
                        obj.GetComponent<EnemyNormal>().HitDamage();
                    }

                }
            }

            coolDown = 0.0f;
            ammo -= 1;
            AmmoUI();

            float RandonNumber = Random.Range(10f, 15f);
            xRotate = xRotate - RandonNumber * gunForce * Time.deltaTime;
            RandonNumber = Random.Range(-10f, 10f);
            yRotate = yRotate - RandonNumber * gunForce * Time.deltaTime;


            transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
        }

        if (Input.GetKeyDown(KeyCode.R) && ammo != 30)
        {
            Invoke("Reload", 1.5f);
            reloading = true;
            reloadUI.text = "Reload".ToString();
        }


    }
    private void Reload()
    {
        reloadUI.text = "".ToString();
        ammo = 30;
        AmmoUI();
        reloading = false;
    }
    private void AmmoUI()
    {
        string ammoText = ammo + "/30";
        //string ammoText = $"{ammo}/30";
        ammoUI.text = ammoText;
    }
}