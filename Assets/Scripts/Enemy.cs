using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 3f;

    [Header("Combat")]
    [SerializeField] float attackCD = 3f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float aggroRange = 4f;
    float attackTime = 3f;

    GameObject player;
    Animator animator;
    NavMeshAgent agent;
    float newDestinationCD = 0.5f;
    float newDestinationTime = 0f;

    void Start(){
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update(){
        animator.SetFloat("Speed",agent.velocity.magnitude / agent.speed);
        if(Time.time >= attackTime){
            if(Vector3.Distance(player.transform.position,transform.position) <= attackRange){
                animator.SetTrigger("Attack");
                attackTime = Time.time+attackCD;
            }
        }
        if(Time.time >= newDestinationTime && Vector3.Distance(player.transform.position,transform.position) <= aggroRange){
            newDestinationTime = Time.time+newDestinationCD;
            agent.SetDestination(player.transform.position);
        }
        transform.LookAt(player.transform.position);
    }

    public void TakeDamage(float damageAmount){
        health -= damageAmount;
        animator.SetTrigger("Damage");
        if(health <= 0){
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,aggroRange);
    }
}
