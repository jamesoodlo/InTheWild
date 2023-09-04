using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Animator anim;
    NavMeshAgent navAi;
    EnemyStats stats;

    private bool getScore;

    public Enemy[] enemy;
    public GameObject findPlayer;
    private GameObject findGhost;
    private GhostAI ghost;

    [Header("Sound Effect")]
    public AudioSource hitSound;
    public AudioSource atkSound;
    public AudioSource footStepSound;
    public AudioSource sparkSound;
    public AudioSource dieSound;

    [Header("Collider")]
    public Collider BodyCollider;
    public Collider lDmgCollider;
    public Collider rDmgCollider;

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
    public GhostAI targetGhost;

    void Start()
    {
        anim = GetComponent<Animator>();
        navAi = GetComponent<NavMeshAgent>();
        stats = GetComponent<EnemyStats>();
        findGhost = GameObject.Find("Ghost");
        ghost = findGhost.GetComponent<GhostAI>();
        lDmgCollider.enabled = false;
        rDmgCollider.enabled = false;
    }

    void Update()
    {  
        findPlayer = GameObject.Find("Himba");
        enemy = FindObjectsOfType<Enemy>();
        //allEnemyAlert();
        if(stats.currenthealth <= 0)
        {
            target = null;
            BodyCollider.enabled = false;
            navAi.isStopped = true;
            anim.SetBool("Death", true);
            StartCoroutine(DestroyBody());
        }
        else
        {
            IdleState();
            DetectGhost();
        }

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Death")) 
        {
            if(getScore == false)
            {
                stats.scoreData.enemiesKill += stats.scoreData.mk1Score;
                getScore = true;
            } 
        }
    }

    private void allEnemyAlert()
    {
        for (int i = 0; i < enemy.Length; i++)
        {
            if (enemy[i].target != null)
            {
                target = findPlayer.GetComponent<Stats>();
            }
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
                }
            }
        }

        if(target != null)
        {
            PursueTargetState();
        }
        else
        {
            anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        }
    }

    private void DetectGhost()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            GhostAI ghost = colliders[i].transform.GetComponent<GhostAI>();
            
            if(ghost != null)
            {
                Vector3 targetDirection = ghost.transform.position - transform.position;
                viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if(viewableAngle > minimumDetectionAngle && viewableAngle < maximumDetectionAngle)
                {
                    targetGhost = ghost;
                }
            }
        }
    }

    private void PursueTargetState()
    {
        Vector3 targetDirection = target.transform.position - this.transform.position;
        float distanceFromTarget = Vector3.Distance(this.target.transform.position, this.transform.position);
        float viewableAngle = Vector3.SignedAngle(targetDirection, this.transform.forward, Vector3.up); 

        Quaternion rotation = Quaternion.LookRotation(targetDirection);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rotation, rotateSpeed / Time.deltaTime);

        if(target == null)
        {
            anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        }

        if(distanceFromTarget > maximumAggroRadius)
        {
            navAi.updatePosition = true;
            navAi.isStopped = false;
            navAi.SetDestination(target.transform.position);
            anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
            anim.SetInteger("Hit", 0);
        }
        else if(distanceFromTarget <= maximumAggroRadius)
        {
            AttackState();
        }
        else
        {
            PursueTargetState();
        }
    }
    
    private void AttackState()
    {
        navAi.isStopped = true;
        anim.SetInteger("Hit", 1);
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Unarmed-Attack-L1"))
        {
            anim.SetInteger("Hit", 2);
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Unarmed-Attack-R1"))
            {
                anim.SetInteger("Hit", 1);
            }
        }
    }

    public void GetHurt()
    {
        anim.Play("GetHit");
    }

    private void EnabledDmg()
    {
        lDmgCollider.enabled = true;
        rDmgCollider.enabled = true;
    }

    private void DisabledDmg()
    {
        lDmgCollider.enabled = false;
        rDmgCollider.enabled = false;
    }

    private void punchSfx()
    {
        atkSound.Play();
    }

    private void footStepSfx()
    {
        footStepSound.Play();
    }

    private IEnumerator DestroyBody()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
        
    }
}
