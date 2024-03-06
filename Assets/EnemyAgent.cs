using System;
using System.Collections;
using Unity.Mathematics;
using Random = UnityEngine.Random;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAgent : MonoBehaviour
{
    private FiniteStateMachine currentState;
    private NavMeshAgent agent;
    public GameObject player;
   
    public enum FiniteStateMachine
    {
        Idle,
        Chase,
        Attack
    }

    // Start is called before the first frame update
    void Start()
    {
         currentState = FiniteStateMachine.Idle;
         agent = gameObject.GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case FiniteStateMachine.Idle:
                isIdle();
                break;
            case FiniteStateMachine.Chase:
                isChasing();
                break;
            case FiniteStateMachine.Attack:
                isAttacking();
                break;
        }
    }

    //applies the idle state
    public void isIdle()
    {
        Debug.Log("idle");
        if (agent.remainingDistance == 0)
        {
           
            Vector3 destination = new Vector3(agent.transform.position.x +Random.Range(-5, 5), agent.transform.position.y,
                agent.nextPosition.z + Random.Range(-5, 5));
            
            agent.SetDestination(destination);
            
        }
        
    }

    //applies the chasing state
    public void isChasing()
    {
        
        if (DistanceToPlayer(agent.transform.position.x, player.transform.position.x,agent.transform.position.y, player.transform.position.y ) > 2)
        {
            agent.SetDestination(player.gameObject.transform.position);
            
        }
        Debug.Log("chasing");
        if (DistanceToPlayer(agent.transform.position.x, player.transform.position.x,agent.transform.position.y, player.transform.position.y ) <= 1.5f)
        {
            currentState = FiniteStateMachine.Attack;
        }
        
    }
//applies the attacking state
    public void isAttacking()
    {
        if (DistanceToPlayer(agent.transform.position.x, player.transform.position.x,agent.transform.position.y, player.transform.position.y ) > 1.4f)
        {
            currentState = FiniteStateMachine.Chase;
        }
        Debug.Log("attacking");
    }

    //checks to see if player is within range and switches to chase mode
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            currentState = FiniteStateMachine.Chase;
            agent.SetDestination(player.gameObject.transform.position);
        }
    }
    
    //if player leaves switches to idle mode
    private void OnTriggerExit(Collider other)
    {
        currentState = FiniteStateMachine.Idle;
    }

    //used to calculate the distance to the player
    private float DistanceToPlayer(float x1, float x2, float y1, float y2)
    {
        float distance = (float)Math.Sqrt(math.pow((x2-x1),2) + math.pow((y2-y1),2)) ;
        return distance;
    }
}
