using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGun : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject Player;
    private bool reboundWork = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int ammoCheck = Player.GetComponent<CameraShooting>().ammo;
        int weaponCheck = rightHand.GetComponent<WeaponManager>().weaponNumber;
        bool autoCheck = Player.GetComponent<CameraShooting>().fullAuto;

        if (weaponCheck == 1)
            gameObject.GetComponentInChildren<Renderer>().enabled = true;
        else
            gameObject.GetComponentInChildren<Renderer>().enabled = false;

        if (Input.GetMouseButton(0) && autoCheck == true)
        {
            if (reboundWork == false && ammoCheck > 0 && weaponCheck == 1)
            {
                reboundWork = true;
                StartCoroutine(ReboundTime());
            }
        }
        else if (Input.GetMouseButtonDown(0) && autoCheck == false)
        {
            if (reboundWork == false && ammoCheck > 0 && weaponCheck == 1)
            {
                reboundWork = true;
                StartCoroutine(ReboundTime());
            }
        }
        else
        { 
        transform.position = new Vector3(rightHand.transform.position.x, rightHand.transform.position.y - 0.3f, rightHand.transform.position.z);
        }
    }

    private IEnumerator ReboundTime()
    {
        float checkTime;
        checkTime = 0.03f;
        while(checkTime >= 0)
        {
            checkTime -= Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y , transform.position.z - 0.003f);
            yield return null;
        }
        checkTime = 0.03f;

        while (checkTime >= 0)
        {
            checkTime -= Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(rightHand.transform.position.x, rightHand.transform.position.y - 0.3f, rightHand.transform.position.z);
        reboundWork = false;
        yield break;
    }    
}
