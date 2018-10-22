using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostWalking : MonoBehaviour
{
	private NavMeshAgent ghost;

	public Transform destination;
	// Use this for initialization
	void Start ()
	{
		ghost = gameObject.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		ghost.SetDestination(destination.position);
		/*if (Vector3.Distance(gameObject.transform.position, destination.position)<6f)
		{
			Debug.Log("stopped");
			Destroy(gameObject);
		}*/
	}
}
