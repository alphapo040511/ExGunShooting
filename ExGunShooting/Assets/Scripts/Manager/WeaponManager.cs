using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public int weaponNumber = 1;
    public GameObject player;

    public GameObject grenadePrefab;
    public GameObject grneadeExplosionPrefab;
    public float grenadeCoolTime = 0f;
    public bool isGrenade = false;
    private bool Timer = false;
    public Text ammoUI = null;

    GameObject temp;
    GameObject temp1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool reload = player.GetComponent<CameraShooting>().reloading;
        if (reload == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                weaponNumber = 1;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                weaponNumber = 2;
            }

            if (Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (grenadeCoolTime <= 0)
                {
                    weaponNumber = 3;
                }
            }
        }

        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, player.transform.eulerAngles.y, transform.eulerAngles.z);

        if(weaponNumber == 1)
        {
            player.GetComponent<CameraShooting>().enabled = true;
            ammoUI.enabled = true;
        }
        else
        {
            player.GetComponent<CameraShooting>().enabled = false;
            ammoUI.enabled = false;
        }

        
        if (weaponNumber == 3)
        {
            if (grenadeCoolTime <= 0 && isGrenade == false && Timer == false)
            {
                isGrenade = true;
                GrenadeSpawn();
            }

            if (Input.GetMouseButtonUp(0) && Timer == false)
            {
                Timer = true;
                StartCoroutine(GrenadeCoolTime());
                weaponNumber = 1;
            }
        }
        else if (Timer == false)
        {
            GrenadeDespawn();
        }

    }
    private void GrenadeSpawn()
    {
        temp = Instantiate(grenadePrefab);
        temp.transform.parent = this.transform;
        temp.transform.rotation = Quaternion.Euler(transform.rotation.x -10, transform.rotation.y, transform.rotation.z -10);
        temp1 = Instantiate(grneadeExplosionPrefab);
        temp1.transform.position = transform.position;

    }
    private void GrenadeDespawn()
    {
        Destroy(temp);
        Destroy(temp1);
        isGrenade = false;
    }

    IEnumerator GrenadeCoolTime()
    {
        grenadeCoolTime = 7f;
        while (grenadeCoolTime >= 0)
        {
            grenadeCoolTime -= Time.deltaTime;
            yield return null;
        }
        isGrenade = false;
        Timer = false;
        yield break;
    }
}
