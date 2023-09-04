using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    NavMeshAgent navAi;
    Animator anim;

    [Header("Ghost Settings")]
    public AudioSource footStepSfx;
    public AudioSource atkSfx;
    public bool isFree = true;
    public Collider DmgCollider;

    [Header("A.I. Settings")] 
    public float detectionRadius = 6;  
    public float rotateSpeed;
    public float maximumDetectionAngle = 50;
    public float minimumDetectionAngle = -50;
    public float viewableAngle;
    public float currentRecoveryTime = 0;
    public float maximumAggroRadius = 2f;
    public float outAggroRadius = 5.5f;
    public LayerMask detectionLayer;
    public EnemyStats targetEnemy;
    private GameObject findPlayer;
    public Stats targetPlayer;

    void Start()
    {
        navAi = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        DmgCollider.enabled = false;
        isFree = true;
    }

    void Update()
    {  
        FollowState();
        IdleState();
        if(targetEnemy == null || targetEnemy.currenthealth <= 0)
        {
            isFree = true;
        }
    }


    private void FollowState()
    {
        if(isFree)
        {
            findPlayer = GameObject.Find("Himba");
            targetPlayer = findPlayer.GetComponent<Stats>();
            targetPlayer.hasGhost = true;
        }

        if(targetPlayer != null )
        {
            PursueTargetPlayerState(); 
        }
        else if(targetPlayer == null && targetEnemy != null)
        {
            PursueTargetEnemyState();
        }

        if(targetEnemy != null)
        {
            targetPlayer = null;
        }
    }

    private void IdleState()
    {
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            EnemyStats enemyStats = colliders[i].transform.GetComponent<EnemyStats>();
           
            if(enemyStats != null)
            {
                Vector3 targetDirection = enemyStats.transform.position - transform.position;
                viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if(viewableAngle > minimumDetectionAngle && viewableAngle < maximumDetectionAngle)
                {
                    targetEnemy = enemyStats;
                    targetPlayer = null;
                    isFree = false;
                }
            }
        }
    }

    private void PursueTargetEnemyState()
    { 
        Vector3 targetDirection = targetEnemy.transform.position - this.transform.position;
        float distanceFromTarget = Vector3.Distance(this.targetEnemy.transform.position, this.transform.position);
        float viewableAngle = Vector3.SignedAngle(targetDirection, this.transform.forward, Vector3.up);

        Quaternion rotation = Quaternion.LookRotation(targetDirection);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rotation, rotateSpeed / Time.deltaTime);

        if(targetEnemy == null)
        {
            isFree = true;
        }

        
        if(distanceFromTarget > maximumAggroRadius )
        {
            navAi.updatePosition = true;
            navAi.isStopped = false;
            navAi.SetDestination(targetEnemy.transform.position);
            anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
            anim.SetInteger("Hit", 0);
        }
        else if(distanceFromTarget <= maximumAggroRadius)
        {
            AttackState();
        }
        
    }

    private void PursueTargetPlayerState()
    {
        Vector3 targetDirection = targetPlayer.transform.position - this.transform.position;
        float distanceFromTarget = Vector3.Distance(this.targetPlayer.transform.position, this.transform.position);
        float viewableAngle = Vector3.SignedAngle(targetDirection, this.transform.forward, Vector3.up); 

        Quaternion rotation = Quaternion.LookRotation(targetDirection);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rotation, rotateSpeed / Time.deltaTime);

        if(targetPlayer == null)
        {
            anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        }

        if(distanceFromTarget > maximumAggroRadius)
        {
            navAi.updatePosition = true;
            navAi.isStopped = false;
            navAi.SetDestination(targetPlayer.transform.position);
            anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
            anim.SetInteger("Hit", 0);
        }
        else if(distanceFromTarget <= maximumAggroRadius)
        {
            navAi.isStopped = true;
            anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        }
    }

    public void footStep()
    {
        footStepSfx.Play();
    }

    public void atkSound()
    {
        atkSfx.Play();
    }
    
    private void AttackState()
    {
        navAi.isStopped = true;
        anim.SetInteger("Hit", 1);
    }

    private void EnabledDmg()
    {
        DmgCollider.enabled = true;
    }

    private void DisabledDmg()
    {
        DmgCollider.enabled = false;
    }
}
