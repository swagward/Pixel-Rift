using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private AudioSource hitSFX;
    [SerializeField] private int enemyHealth;
    public bool isAlive;
    public Animator anim;
    
    [Header("Attacking")] 
    [SerializeField] private float knockbackForce;
    [SerializeField] private int damage;

    [Header("Info")] 
    //[SerializeField] private Animator anim;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private PlayerMovement playerScript;
    [SerializeField] private Rigidbody playerRB;
    
    [Header("Area Detection")]
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask player;

    [Header("Movement")]
    [SerializeField] private Vector3 walkTo;
    [SerializeField] private float walkRange;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float sightRange, attackRange;
    public bool _walkPointSet;
    public bool _hasAttacked;
    public bool _inSightRange, _inAttackRange;
    
    private void Awake()
    {
        isAlive = true;

        hitSFX = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        playerObj = GameObject.Find("PlayerRoot");
        playerRB = GameObject.Find("PlayerRoot").GetComponent<Rigidbody>();
        playerScript = GameObject.Find("PlayerRoot").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (PlayerMovement.IsDead || !isAlive) return;
        
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
        //Check player for sight/attack range
        _inSightRange = Physics.CheckSphere(transform.position, sightRange, player);
        _inAttackRange = Physics.CheckSphere(transform.position, attackRange, player);
        
        if(!_inSightRange && !_inAttackRange)
            Idle();
        if(_inSightRange && !_inAttackRange)
            ChasePlayer();
        if(_inSightRange && _inAttackRange)
            AttackPlayer();
    }

    private void Idle()
    {
        anim.SetBool("Idle", true);
        if(!_walkPointSet)
            FindWalkPoint();
        else
           agent.SetDestination(walkTo);

        var dstToWalkPoint = transform.position - walkTo;
        if (dstToWalkPoint.magnitude < 1f)
            _walkPointSet = false;
    }
    
    private void FindWalkPoint()
    {
        var walkPointX = Random.Range(-walkRange, walkRange);
        var walkPointZ = Random.Range(-walkRange, walkRange);

        walkTo = new Vector3(transform.position.x + walkPointX, transform.position.y,
            transform.position.z + walkPointZ);

        if (Physics.Raycast(walkTo, -transform.up, ground))
            _walkPointSet = true;
    }

    private void ChasePlayer()
    {
        anim.SetBool("Idle", false);
        agent.SetDestination(playerObj.transform.position);
        Debug.Log("Chasing");
    }

    private void AttackPlayer()
    {
        //Stop enemy moving when attacking
        agent.SetDestination(transform.position);
        transform.LookAt(playerObj.transform);

        if (!_hasAttacked)
        {
            anim.SetTrigger("Hit");
            Hit();
            _hasAttacked = true;
        }
    }

    private void Hit()
    {
        if (_inAttackRange)
        {
            playerRB.AddForce(transform.forward * knockbackForce, ForceMode.Impulse);
            playerRB.AddForce(playerObj.transform.up * (knockbackForce / 5), ForceMode.Impulse);
            playerScript.health.TakeDamage(damage);
        }

        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Balls");
        if (!other.gameObject.CompareTag("Projectile")) return;
        
        var projectile = other.GetComponent<BaseProjectile>();
        enemyHealth -= projectile.damage;
        
        if(enemyHealth <= 0)
            Die();
    }

    private void Die()
    {
        StartCoroutine(deathProcess());  
    }

    IEnumerator deathProcess()
    {
        Destroy(agent);
        anim.SetTrigger("Die");
        hitSFX.Play();
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
        isAlive = false;
    }


    private void ResetAttack()
    {
        _hasAttacked = false;
    }
}
