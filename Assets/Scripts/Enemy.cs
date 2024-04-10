using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 3f;

    [Header("Combat")]
    [SerializeField] float attackCD = 3f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float aggroRange = 4f;
    [SerializeField] float visionAngle = 90f;
    [SerializeField] float playerAngle;
    [SerializeField] GameObject hitVFX;

    GameObject damageDealer;

    float attackTime = 3f;

    GameObject player;
    Animator animator;
    NavMeshAgent agent;
    float newDestinationCD = 4f;
    float newDestinationTime = 0f;

    public Vector3 RandomNavmeshLocation(float radius) {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
            finalPosition = hit.position;            
        }
        return finalPosition;
    }

    void Start(){
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        damageDealer = transform.GetChild(2).gameObject;
        damageDealer.SetActive(false);
    }

    void Update(){
        animator.SetFloat("Speed",agent.velocity.magnitude / agent.speed);
        float dist = Vector3.Distance(player.transform.position,transform.position);
        if(dist <= aggroRange && playerIsOnSight()){
            agent.SetDestination(player.transform.position);
            transform.LookAt(player.transform.position);
            if(dist <= attackRange && Time.time >= attackTime){
                animator.SetTrigger("Attack");
                attackTime = Time.time+attackCD;
            }
        }else if(Time.time >= newDestinationTime){
            newDestinationTime = Time.time+newDestinationCD;
            agent.SetDestination(RandomNavmeshLocation(10f));
        }
    }

    bool playerIsOnSight()
    {
        playerAngle = Vector3.Angle(player.transform.position - transform.position,transform.forward);
        if(playerAngle > visionAngle/2) return false;
        RaycastHit hit;
        if (Physics.Raycast(transform.position+transform.up*1.5f,(player.transform.position - transform.position),out hit,Mathf.Infinity))
        {
            Debug.DrawRay(transform.position+transform.up*1.5f, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if(hit.transform.gameObject == player) return true;
        }
        return false;
    }

    public void TakeDamage(float damageAmount){
        GameObject hit = Instantiate(hitVFX,transform.position+transform.up*1.8f,Quaternion.identity);
        Destroy(hit,3f);
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

    public void StartDamage(){
        damageDealer.SetActive(true);
    }
    public void EndDamage(){
        damageDealer.SetActive(false);
    }

}
