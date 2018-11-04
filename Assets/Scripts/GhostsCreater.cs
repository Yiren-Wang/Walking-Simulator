using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostsCreater : MonoBehaviour
{
	public Transform[] destination;
	public float changeTime;
	private float timeElapsed;
	private GameObject ghost;
	public GameObject[] ghosts;
	// Use this for initialization
	void Awake()
	{
		
	}
	void Start ()
	{
		timeElapsed = 0;
//		ghosts = new GameObject[ghostNum];
//		
//		for (int i = 0; i < ghostNum; i++)
//		{	
//			if (i < 3)
//			{
//				ghost = Instantiate(Resources.Load("Prefabs/GhostKid")) as GameObject;
//			}
//			else if (i < 9)
//			{
//				ghost = Instantiate(Resources.Load("Prefabs/GhostWoman")) as GameObject;
//			}
//			else
//			{
//				ghost = Instantiate(Resources.Load("Prefabs/GhostMan")) as GameObject;
//			}
//			int startPointNum = i%11;
//			ghost.transform.position = startPoint[0].position;
//			
//			int destinationNum = i % 13;
//			ghost.GetComponent<GhostWalking>().destination = destination[0];
//			ghosts[i] = ghost;
//			print(ghosts[0].transform.position);
//		}		
	}
	
	// Update is called once per frame
	void Update()
	{
		timeElapsed += Time.deltaTime;
		if (timeElapsed > changeTime)
		{
			int randomNum = Random.Range(0, ghosts.Length);
			int randomDestination = Random.Range(0, destination.Length - 1);
			ghosts[randomNum].GetComponent<GhostWalking>().destination = destination[randomDestination];
			timeElapsed = 0;
		}
}
}
