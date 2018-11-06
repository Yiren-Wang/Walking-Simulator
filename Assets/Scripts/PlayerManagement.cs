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
	public bool isFirstTalkWithKQ = false;
	public bool isTalkingToK = false;
	public bool isTalkingToQ = false;
	public bool isGetStick = false;
	public bool isGetCrown = false;
	public bool isGiveStick = false;
	public bool isGiveCrown = false;
	public bool isTalkedToCorinna = false;
	public bool isAskedForHelp = false;
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
