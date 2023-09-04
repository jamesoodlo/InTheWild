using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public AudioSource sfx;
    public float Speed = 0.1f;
    public float SecondsUntilDestroy = 3f;
    float startTime;
    
    void Start()
    {
        sfx.Play();
        startTime = Time.time;
    }

    void Update()
    {
        this.gameObject.transform.position += Speed * this.gameObject.transform.forward;

        if (Time.time - startTime >= SecondsUntilDestroy)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
