using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyMK3 : MonoBehaviour
{
    Animator anim;
    UnityEngine.AI.NavMeshAgent navAi;
    EnemyMK3Stats stats;
    public GameObject findPlayer;
    private GameObject findGhost;
    private GhostAI ghost;
    private bool getScore;

    public GameObject hpBarMk3;
    public GameObject stmBarMk3;

    [Header("Sound Effect")]
    public AudioSource hitSound;
    public AudioSource atkSound;
    public AudioSource footStepSound;
    public AudioSource sparkSound;
    public AudioSource dieSound;

    [Header("Combat System")]
    public bool startBattle;
    public bool isStun;
    public bool Phase2;
    public bool outStm;
    public GameObject lHandWeapon;
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
        navAi = GetComponent<UnityEngine.AI.NavMeshAgent>();
        stats = GetComponent<EnemyMK3Stats>();
        findGhost = GameObject.Find("Ghost");
        ghost = findGhost.GetComponent<GhostAI>();
        lHandWeapon.SetActive(false);
        lDmgCollider.enabled = false;
        rDmgCollider.enabled = false;
    }

    void Update()
    {  
        if(stats.currenthealth <= 401)
        {
            Phase2 = true;
            navAi.speed = 2.5f;
            lHandWeapon.SetActive(true);
            anim.SetBool("Phase2", true);
            stats.electricFx.SetActive(true);
            stats.sparkFx.SetActive(true);
            sparkSound.Play();
        }

        SetOutStamina();

        if(startBattle)
        {
            hpBarMk3.SetActive(true);
            stmBarMk3.SetActive(true);
            
        }
        else
        {
            hpBarMk3.SetActive(false);
            stmBarMk3.SetActive(false);
        }

        if(stats.currenthealth <= 0)
        {
            hpBarMk3.SetActive(false);
            stmBarMk3.SetActive(false);
            BodyCollider.enabled = false;
            startBattle = false;
            navAi.isStopped = true;
            Destroy(this); 
            anim.Play("EnemyMk3-Death");
        }
        else
        {
            if(!outStm)
            {
                IdleState();
            }
            else
            {
                anim.Play("EnemyMk3-Crouch");
                navAi.SetDestination(transform.position);
                anim.SetInteger("Hit", 0);
                anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            }
        }
    }

    private void SetOutStamina()
    {
        if(stats.currentStamina <= 0)
        {
            outStm = true;
        }
        else if(stats.currentStamina >= stats.maxStamina)
        {
            outStm = false;
        }

        if(outStm)
        {
            stats.RegenrateStamina();
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
            PursueTargetState();
            stats.DrainStamina();
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
            navAi.SetDestination(target.transform.position);
        }
    }
    
    private void AttackState()
    {
        navAi.SetDestination(transform.position);
        navAi.isStopped = true;
        if(!Phase2)
        {
            anim.SetInteger("Hit", 1);
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyMk3-Attack-R1"))
            {
                anim.SetInteger("Hit", 2);
            }

            if(anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyMk3-Attack-R2"))
            {
                anim.SetInteger("Hit", 3);   
            }

            if(anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyMk3-Attack-R3"))
            {
                anim.SetInteger("Hit", 1); 
            }
        }
        else
        {
            anim.SetInteger("Hit", 1);
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyMk3-Attack-R1P2"))
            {
                anim.SetInteger("Hit", 2);
            }

            if(anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyMk3-Attack-L1"))
            {
                anim.SetInteger("Hit", 3);   
            }

            if(anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyMk3-Attack-R2P2"))
            {
                anim.SetInteger("Hit", 4);   
            }

            if(anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyMk3-Attack-L2"))
            {
                anim.SetInteger("Hit", 5);   
            }

            if(anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyMk3-Attack-R3P2"))
            {
                anim.SetInteger("Hit", 6);   
            }

            if(anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyMk3-Attack-L3"))
            {
                anim.SetInteger("Hit", 1);    
            }
        } 
    }

    private void atkSfx()
    {
        atkSound.Play();
    }

    private void footStepSfx()
    {
        footStepSound.Play();
    }

    private void EnabledDmgLHand()
    {
        lDmgCollider.enabled = true;
    }

    private void EnabledDmgRHand()
    {
        rDmgCollider.enabled = true;
    }

    private void DisabledDmgLHand()
    {
        lDmgCollider.enabled = false;
    }

    private void DisabledDmgRHand()
    {
        rDmgCollider.enabled = false;
    }

    private IEnumerator DestroyBody()
    {
        yield return new WaitForSeconds(1.5f);
        startBattle = false;
        Destroy(this.gameObject);
        
    }
}
