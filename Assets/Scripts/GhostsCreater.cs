using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostsCreater : MonoBehaviour
{
	public Transform[] startPoint;
	public Transform[] destination;
	public int ghostNum;
	public float changeTime;
	private float timeElapsed;

	private GameObject[] ghosts;
	// Use this for initialization
	void Awake()
	{
		
	}
	void Start ()
	{
		ghosts = new GameObject[ghostNum];
		timeElapsed = 0;
		for (int i = 0; i < ghostNum; i++)
		{
			GameObject ghost = Instantiate(Resources.Load("Prefabs/Ghost")) as GameObject;
			int startPointNum = i%11;
			ghost.transform.position = startPoint[startPointNum].position;
			int destinationNum = 10 - i % 11;
			ghost.GetComponent<GhostWalking>().destination = destination[destinationNum];
			ghosts[i] = ghost;
		}		
	}
	
	// Update is called once per frame
	void Update ()
	{
		timeElapsed += Time.deltaTime;
		if (timeElapsed > changeTime)
		{
			int randomNum = Random.Range(0, ghostNum);
			int randomDestination = Random.Range(0, destination.Length-1);
			ghosts[randomNum].GetComponent<GhostWalking>().destination = destination[randomDestination];
			timeElapsed = 0;
		}
	}
}
