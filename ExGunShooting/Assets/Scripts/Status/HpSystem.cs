using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpSystem : MonoBehaviour
{
    public float curHealth;
    public float maxHealth;

    public int hp = 10;
    // Start is called before the first frame update
    void Start()
    {
        
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HitDamage()
    {
        hp -= 1;
    }
}

