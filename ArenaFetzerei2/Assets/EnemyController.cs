using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    Transform target;
    NavMeshAgent agent;
    public int AttackDamage = 30;
    public float AttackSpeed = 1f;
    public float AttackCooldown = 0f;

    void Start(){
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update(){

        float distance = Vector3.Distance(target.position, transform.position);
        agent.SetDestination(target.position);

        AttackCooldown -= Time.deltaTime;

        if(distance <= agent.stoppingDistance) {     
            // Richtung Player drehen
            FaceTarget();
            //Attack
            Attack();
        }
    }

    void Attack(){
        if (AttackCooldown <= 0) {
            PlayerManager.instance.PlayerHealth -= AttackDamage;
            Debug.Log("Attack");
            AttackCooldown = 1 / AttackSpeed;
        }
    }

    // Wenn Gegner nah genug an Player rotiert er trotzdem weiterhin zum Player
    void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

}
