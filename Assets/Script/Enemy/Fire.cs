using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public AudioSource fireSfx;
    public bool onDestroy = true;

    void Start()
    {
        fireSfx.Play();
        if(onDestroy) StartCoroutine(DestroyFire());
    }

    
    void Update()
    {
        
    }

    private IEnumerator DestroyFire()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
