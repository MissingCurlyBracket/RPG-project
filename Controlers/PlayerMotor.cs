using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour {

	Transform target;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}

	//Update is called once per frame
	void Update ()
	{
		if (target != null) 
		{
			agent.SetDestination (target.position);
			FaceTarget ();
		}
	}

	public void MoveToPoint (Vector3 point)
	{
		agent.SetDestination (point);
	}

	public void FollowTarger (Interactable newTarget)
	{
		agent.stoppingDistance = newTarget.radius * .8f;
		agent.updateRotation = false;

		target = newTarget.interactionTransform;
	}

	public void StopFollowingTarget ()
	{
		agent.updateRotation = true;
		agent.stoppingDistance = 0f;

		target = null;
	}

	void FaceTarget () 
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation (new Vector3 (direction.x, 0f, direction.z));
		transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * 5f);
	}
}
