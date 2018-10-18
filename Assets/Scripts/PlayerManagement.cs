using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
	private float curMushroom;
	public float MushroomNumGoal;
	
	// Use this for initialization
	void Start ()
	{
		curMushroom = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddMushroom()
	{
		curMushroom++;
		Debug.Log("Number of mushrooms:" + curMushroom);
		if (curMushroom >= MushroomNumGoal)
		{
			Debug.Log("You have enough mushrooms!");
		}
	}
}
