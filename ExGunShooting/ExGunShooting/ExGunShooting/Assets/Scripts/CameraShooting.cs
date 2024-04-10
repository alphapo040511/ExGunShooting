using Unity.PlasticSCM.Editor.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraShooting : MonoBehaviour
{
    private float xRotate, yRotate, xRotateMove, yRotateMove;
    public float rotateSpeed = 500.0f;
    public GameObject player;

    public float gunForce = 20.0f;
    public float coolDown;
    public float coolTime;
    private int ammo;
    public Text ammoUI = null;
    public Text reloadUI = null;
    GameObject obj;
    private float fireCooltime = 0;
    private bool fire = false;

    private bool reloading = false;
    public bool fullAuto = true;
    public int reBound = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        coolDown = 0.0f;
        coolTime = 0.08f;
        ammo = 30;
        AmmoUI();

        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;
        yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;


        yRotate = transform.eulerAngles.y + yRotateMove;
        xRotate = transform.eulerAngles.x + xRotateMove;

        if (fire == false)
        {
            transform.rotation = Quaternion.Euler(xRotate, yRotate, 0);
        }

        if (Input.GetKeyDown(KeyCode.V) && reloading == false)
        {
            if (fullAuto == true)
            {
                fullAuto = false;
            }
            else
            {
                fullAuto = true;
            }
        }

        
        coolDown += Time.deltaTime;


        if (coolDown >= coolTime && ammo > 0 && reloading == false)
        {
            if (Input.GetMouseButton(0) && fullAuto == true)
            {
                FullAutoShoot();
            }
            if(Input.GetMouseButtonDown(0) && fullAuto == false)
            {
                SingleShoot();
            }
        }
        float fireCoolTime = 0f;

        while(fireCoolTime <= 0.03f)
        {
            transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
            fireCoolTime += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            reBound = 0;
            fire = false;
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

    private void HitCheck()
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
    }

    private void FullAutoShoot()
    {
        HitCheck();

        coolDown = 0.0f;
        ammo -= 1;
        AmmoUI();

        for (fireCooltime = 0; fireCooltime <= 1f;)
        {
            float RandonNumber = Random.Range(0.02f + reBound * reBound / 30000, 0.06f + reBound * reBound / 30000);
            xRotate = xRotate - RandonNumber * gunForce * Time.deltaTime;
            RandonNumber = Random.Range(-0.1f + reBound * reBound / 60000, 0.1f + reBound * reBound / 60000);
            yRotate = yRotate - RandonNumber * gunForce * Time.deltaTime;
            fireCooltime += Time.deltaTime;
        }

        fire = true;

        reBound += 1;
        
    }



    private void SingleShoot()
    {
        HitCheck();

        coolDown = 0.0f;
        ammo -= 1;
        AmmoUI();

            for (fireCooltime = 0; fireCooltime <= 5f;)
        {
            float RandonNumber = Random.Range(0.02f, 0.06f);
            xRotate = xRotate - RandonNumber * gunForce * Time.deltaTime;
            RandonNumber = Random.Range(-0.2f, 0.2f);
            yRotate = yRotate - RandonNumber * gunForce * Time.deltaTime;
            fireCooltime += Time.deltaTime;
            Debug.Log(fireCooltime);
        }

            fire = true;
        }
    
}