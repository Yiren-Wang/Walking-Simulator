using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
	private float curMushroom;
	public float MushroomNumGoal;
	public bool isTalkToCharon = false;
	public bool isShowedMushroom = false;
	public bool isEnoughMushroom = false;
	public bool isGetCoin = false;
	public bool isMoveBoat = false;
	//public GameObject curText;
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
		if (curMushroom >= MushroomNumGoal)
		{
			Debug.Log("You have enough mushrooms!");
			isEnoughMushroom = true;
		}
	}
}
