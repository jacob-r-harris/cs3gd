using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour 
{
	public enum AgentState 
	{
		Idle = 0,
		Patrolling,
		Chasing
	}

	public AgentState state;
	public Transform[] waypoints;
	private NavMeshAgent navMeshAgent;
	private Animator animController;
	private int speedHashId;
	[SerializeField]
	private Transform target;
	[SerializeField]
	private float stopDist = 2;
	[SerializeField]
	private float attackDist = 7;
	[SerializeField]
	private float noticeDist = 15;


	void Awake()
	{
		speedHashId    = Animator.StringToHash ("walkingSpeed");
		navMeshAgent   = GetComponent<NavMeshAgent>();
		animController = GetComponentInChildren<Animator>();
		navMeshAgent.stoppingDistance = stopDist;
		

		if (waypoints.Length == 0) 
		{
			Debug.LogError("Error: list of waypoints is empty.");
		}
	}

	void Update()
	{
		if (state == AgentState.Idle)
			Idle ();
		else if (state == AgentState.Patrolling)
			Patrol ();
		else
			Chase ();
	}

	void Chase ()
	{
		if (target != null)
		{
			float distanceFromTarget = Vector3.Distance(navMeshAgent.transform.position, target.position);

			if (distanceFromTarget <= noticeDist)
			{
				navMeshAgent.SetDestination(target.position);
				
				animController.SetFloat("speed", navMeshAgent.velocity.magnitude);

				if (distanceFromTarget < attackDist && !animController.GetCurrentAnimatorStateInfo(1).IsName("Attack"))
				{
					animController.SetTrigger("attack");
				}
			} else {
				animController.SetFloat ("speed", 0.0f);
				navMeshAgent.velocity = Vector3.zero;
			}
		}
	}

	void Idle() 
	{
		animController.SetFloat (speedHashId, 0.0f);
	}

	void Patrol()
	{
		animController.SetFloat (speedHashId, 1.0f);
	}


}