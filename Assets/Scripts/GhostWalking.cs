using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostWalking : MonoBehaviour
{
	private NavMeshAgent ghost;
	public Transform destination;
	public Animator ghostAnimator;
	// Use this for initialization
	void Start ()
	{
		ghost = gameObject.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		ghost.SetDestination(destination.position);
		if (ghost.velocity.magnitude >0.1f)
		{
			ghostAnimator.SetBool("isWalking", true);
		}
		else
		{
			ghostAnimator.SetBool("isWalking", false);
		}
	}
}
