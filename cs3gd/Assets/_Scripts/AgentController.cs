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
	private double timeSinceLastAttack;
	public float attackSpeed;


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
		if (animController.GetCurrentAnimatorStateInfo(1).IsName("Attack")){
			gameObject.BroadcastMessage("attacking");
		}

		if (state == AgentState.Idle)
			Idle ();
		else if (state == AgentState.Patrolling)
			Patrol ();
		else
			Chase ();
	}

	void Chase ()
	{
		navMeshAgent.SetDestination(target.position);
		animController.SetFloat("speed", navMeshAgent.velocity.magnitude);

		float distanceFromTarget = Vector3.Distance(navMeshAgent.transform.position, target.position);

		if (distanceFromTarget < attackDist && Time.timeAsDouble - timeSinceLastAttack >= attackSpeed)
		{
			timeSinceLastAttack = Time.timeAsDouble;
			animController.SetTrigger("attack");
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