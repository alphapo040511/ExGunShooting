using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject player;
    public GameObject grenadePrefab;
    public float grenadeCoolTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Character"), LayerMask.NameToLayer("Weapon"));
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, player.transform.eulerAngles.y, transform.eulerAngles.z);
        if (Input.GetKeyDown(KeyCode.G) && grenadeCoolTime <= 0)
        {

            GameObject temp = Instantiate(grenadePrefab);
            temp.transform.parent = this.transform;
            StartCoroutine(GrenadeCoolTime());
        }
    }

    IEnumerator GrenadeCoolTime()
    {
        grenadeCoolTime = 8;
        while (grenadeCoolTime >= 0)
        {
            grenadeCoolTime -= Time.deltaTime;
            yield return null;
        }

        yield break;
    }
}
