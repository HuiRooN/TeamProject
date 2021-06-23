using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    private enum State { patrol, trace}
    private State state;

	private bool isStop = false;
	private float stopTime = 2f;

	[SerializeField] float viewAngle;
    [SerializeField] float viewDistance;

    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;

    [SerializeField] Transform playerTransform;
	private Vector3 savePosition;

	[SerializeField] Transform eyeTransform;
    [SerializeField] LayerMask targetMask;

    private NavMeshAgent agent;
	[SerializeField] Transform[] EnemyWayPoint;
	private int wayCount = 0;


	private Animator animator;


    private bool patroling;
    private bool wasTracing;
    private float stayTime;




    private void Start()
    {
		patroling = true;
		wasTracing = false;

		walkSpeed = 5.0f;
        runSpeed = 8.0f;

        agent = GetComponent<NavMeshAgent>();
		InvokeRepeating("PatrolMove", 0f, 2f);

		animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		StopControl();

		Sight();
    }

    private void FixedUpdate()
    {
		switch (state)
		{
			case State.patrol:
				animator.SetFloat("Speed", agent.velocity.magnitude);
				if (patroling) break;
				if (wasTracing)
				{
					if (agent.velocity == Vector3.zero)
					{
						animator.SetFloat("Speed", 0f);
						animator.SetBool("FindTarget", false);

						stayTime += Time.deltaTime;

						if(stayTime >= 1.5f)
                        {
							wasTracing = false;
							stayTime = 0f;
							--wayCount;
						}

					}
					break;
				}
				agent.speed = walkSpeed;
				agent.isStopped = false;
				InvokeRepeating("PatrolMove", 0f, 2f);
				patroling = true;
				break;
			case State.trace:
				agent.SetDestination(playerTransform.position);
				stayTime = 0f;
				break;
		}
	}

    private void Sight()
	{
		Collider[] targetColliders = Physics.OverlapSphere(eyeTransform.position, viewDistance, targetMask);

		for (int i = 0; i < targetColliders.Length; ++i)
		{
			Transform targetTransform = targetColliders[i].transform;
			if (targetTransform.name != "Player") continue;

			Vector3 targetDirection = (targetTransform.position - eyeTransform.position);
			targetDirection.y = targetTransform.position.y;
			targetDirection.Normalize();
			Vector3 targetfoot = (targetTransform.position - eyeTransform.position);
			targetfoot.Normalize();

			float targetAngle = Vector3.Angle(targetDirection, transform.forward);

			if (viewAngle * 0.5f <= targetAngle) continue;

			RaycastHit hit;

			if (Physics.Raycast(eyeTransform.position, targetfoot, out hit, viewDistance))
			{
				if (hit.transform.name == "Player")
				{
					if (state == State.patrol) CancelInvoke();


					state = State.trace;
					agent.isStopped = false;
					patroling = false;
					agent.speed = runSpeed;
					wasTracing = true;
					animator.SetBool("FindTarget", true);
					return;
				}
			}
		}

		if (state == State.trace)
		{
			animator.SetBool("FindTarget", true);
			savePosition = playerTransform.position;
			agent.SetDestination(savePosition);
			state = State.patrol;
		}

	}

	private void PatrolMove()
	{

		//agent.isStopped = false;
		if (agent.velocity == Vector3.zero)
		{
			if (EnemyWayPoint.Length == 1)
			{
				agent.SetDestination(EnemyWayPoint[0].position);
				return;
			}

			agent.SetDestination(EnemyWayPoint[wayCount].position);

			wayCount += 1;
			if (wayCount >= EnemyWayPoint.Length)
				wayCount = 0;
		}
	}

	private void StopControl()
	{
		if (isStop == false) return;
		if (stopTime <= 0f)
		{
			InvokeRepeating("PatrolMove", 0f, 2f);
			agent.isStopped = false;
			isStop = false;
			stopTime = 3f;
			return;
		}
		else
		{
			stopTime -= Time.deltaTime;
		}
	}


}
