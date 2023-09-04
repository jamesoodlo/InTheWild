using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMK2 : MonoBehaviour
{
    EnemyMK2Stats stat;
    Animator anim;

    public bool startBattle;
    public GameObject healthBarMk2;
    [Header("A.I. Settings")]
    public float detectionRadius = 5;
    public float rotateSpeed;
    public float maximumDetectionAngle = 50;
    public float minimumDetectionAngle = -50;
    public float currentRecoveryTime = 0;
    public float maximumAggroRadius = 1.5f;
    public LayerMask detectionLayer;
    public float viewableAngle;
    public Stats target;

    [Header("Sound Effect")]
    public AudioSource hitSound;
    public AudioSource sparkSound;
    public AudioSource dieSound;

    [Header("Gun System")]
    public Mk2Gun Gun1;
    public Mk2Gun Gun2;
    public bool Fire;

    void Start()
    {
        stat = GetComponent<EnemyMK2Stats>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {   
        if(stat.currenthealth <= 0)
        {
            target = null;
            startBattle = false;
            healthBarMk2.SetActive(false);
            Destroy(this); 
        }
        else
        {
            IdleState();
        }

        if(startBattle)
        {
            healthBarMk2.SetActive(true);
        }
        else
        {
            healthBarMk2.SetActive(false);
        }

        FireCheck();

        if(Fire)
        {
            anim.SetBool("isFire", true);
        }
        else
        {
            anim.SetBool("isFire", false);
        }
    }

    private void IdleState()
    {
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            Stats characterStats = colliders[i].transform.GetComponent<Stats>();
            
            if(characterStats != null)
            {
                Vector3 targetDirection = characterStats.transform.position - transform.position;
                viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if(viewableAngle > minimumDetectionAngle && viewableAngle < maximumDetectionAngle)
                {
                    target = characterStats;
                    startBattle = true;
                }
            }
        }

        if(target != null)
        {
            RotateTowardTargetState();
        }
        else
        {
            
        }

    }

    private void RotateTowardTargetState()
    {
        Vector3 targetDirection = target.transform.position - this.transform.position;
        Quaternion rotation = Quaternion.LookRotation(targetDirection);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rotation, rotateSpeed / Time.deltaTime);
        rotation.x = 0;
    }

    private void FireCheck()
    {
        if(Gun1.Fire && Gun2.Fire)
        {
            Fire = true;
        }
        else
        {
            Fire = false;
        }
    }
}
