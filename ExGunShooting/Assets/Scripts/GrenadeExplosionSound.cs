using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosionSound : MonoBehaviour
{
    public AudioSource boom;
    // Start is called before the first frame update
    void Start()
    {
        boom = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Timer()
    {
        StartCoroutine(BoomSound());
    }
    public IEnumerator BoomSound()
    {
        float checkTime = 5;
        while (checkTime >= 0)
        {
            checkTime -= Time.deltaTime;
            yield return null;
        }
        if(checkTime < 0)
        {
            gameObject.transform.SetParent(null);
            boom.Play();
        }
        Destroy(this.gameObject, boom.clip.length);
        yield break;
    }
}
