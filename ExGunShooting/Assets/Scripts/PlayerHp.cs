using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    public int Hp = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp == 0)
        {
            Debug.Log("Die");
        }
    }

    public void HitDamage()
    {
        Hp -= 5;
        Debug.Log(Hp);
    }
}
