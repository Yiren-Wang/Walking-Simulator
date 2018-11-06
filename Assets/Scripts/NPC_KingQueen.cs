using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class NPC_KingQueen : MonoBehaviour {

	private string[,] dialog = new string[7,10];
	public int[] dialogLong; 
	//public int whichDialog;
	public float waitTime;
	public bool isStartDialog;
	public ParticleSystem[] dissolveParticles;
	public VTextInterface[] TargetText;
	public PlayerManagement playerManagement;
	private int rows;
	private TextDissolve[] textDissolve = new TextDissolve[3];
	private int dialogNum;


	// Use this for initialization
	void Awake () {
		TargetText[0].gameObject.SetActive(false);
		TargetText[1].gameObject.SetActive(false);
		TargetText[2].gameObject.SetActive(false);
		textDissolve[0] = TargetText[0].GetComponent<TextDissolve>();
		textDissolve[1] = TargetText[1].GetComponent<TextDissolve>();
		textDissolve[2] = TargetText[2].GetComponent<TextDissolve>();
		dissolveParticles[0].Stop();
		dissolveParticles[1].Stop();
		dissolveParticles[2].Stop();
		//Inside the Palace first talk
		dialog[0, 0] = "We’ve been watching\nyou approach.";
		dialog[0, 1] = "You’ve come a long way.";
		dialog[0, 2] = "A long way for one\nyou killed.";
		dialog[0, 3] = "But we will not be generous today.";
		dialog[0, 4] = "The girl will stay in\nthe world of the shades.";
		dialog[0, 5] = "We are sorry. ";

		
		//Hades after first talk 
		dialog[1, 0] = "The queen is unhappy.\nToo much time in the\ndarkness, perhaps?";
		dialog[1, 1] = "I had my shades make\nher a new crown, but\nwould she want it...?";
		
		//Persephone after first talk 
		dialog[2, 0] = "I want to help you.\nI come from the land of\ndaylight and spring breezes.\nLike you.";
		dialog[2, 1] = "Do you think my husband\nstill loves me?";
		dialog[2, 2] = "I wonder.";
		dialog[2, 3] = "I made him a gift.\nTo remind him of who\nI am.";
		
		//When the player brings the staff to Hades		
		dialog[3, 0] = "Where did you find this?\nDid you steal it?";
		dialog[3, 1] = "You mean my wife...wanted\nto give it to me?";
		dialog[3, 2] = "I don’t know what to feel.";
		
		//When the player brings the crown to Persephone		
		dialog[4, 0] = "This is beautiful.\nWhere did you find it?";
		dialog[4, 1] = "So this is a gift\nfrom Hades? I...";
		dialog[4, 2] = "This changes things.";
		
		//After giving both gifts		
		dialog[5, 0] = "You’ve been good to us,\nlittle not-shade.";//Persephone
		dialog[5, 1] = "There was someone you\nwanted to see? Go ahead.";
		dialog[5, 2] = "We wish you luck.";//Persephone
		
		//Asking Persephone for guidance
		dialog[6, 0] = "Ah, so she’s angry?";
		dialog[6, 1] = "Some suggestions,\nlittle one.";
		dialog[6, 2] = "Tell her you’re sorry.";
		dialog[6, 3] = "That’s why you’re really\nhere, isn’t it?";

	}
	
	// Update is called once per frame
	void Update () {

		
	}

	private int GetWhichDialog()
	{
		if (playerManagement.isTalkedToCorinna)
		{
			return 6;
		}
		if (playerManagement.isGiveCrown && playerManagement.isGiveStick)
		{
			return 5;
		}
		if (playerManagement.isGetStick && playerManagement.isTalkingToQ)
		{
			return 4;
		}
		if (playerManagement.isGetCrown && playerManagement.isTalkingToK)
		{
			return 3;
		}
		if (playerManagement.isFirstTalkWithKQ && playerManagement.isTalkingToQ)
		{
			return 2;
		}
		if (playerManagement.isFirstTalkWithKQ && playerManagement.isTalkingToK)
		{
			return 1;
		}
		return 0;
	}
	private int GetWhichText()
	{
		if (GetWhichDialog() == 1 || GetWhichDialog() == 3 )
		{
			return 0;
		}
		if (GetWhichDialog() == 2 || GetWhichDialog() == 4 || GetWhichDialog() == 6)
		{
			return 1;
		}
		return 2;
	}
	public void OneSentenceAppear(int num)
	{
		if (num == 0)
		{
			textDissolve[GetWhichText()].ResetProgress();
			TargetText[GetWhichText()].gameObject.SetActive(true);
			dialogNum = num;
		}
		
		TargetText[GetWhichText()].RenderText = dialog[GetWhichDialog(),num];	
		textDissolve[GetWhichText()].ResetTimeElapsed();
		textDissolve[GetWhichText()].isStartAppear = true;
		string tempString = dialog[GetWhichDialog(),dialogNum].Replace("\n","%");
		rows = Regex.Matches(tempString, "%").Count;
		StartCoroutine(WaitAndDisappear(waitTime * (rows+1)));
	}

	public void SetTextActive()
	{
		TargetText[GetWhichText()].gameObject.SetActive(true);
	}
	
	
	IEnumerator WaitAndDisappear(float dialogWaitTime)
	{
		yield return new WaitForSeconds(dialogWaitTime);
		textDissolve[GetWhichText()].ResetTimeElapsed();
		textDissolve[GetWhichText()].isStartDisappear = true;	
		dissolveParticles[GetWhichText()].Stop();
		var shape = dissolveParticles[GetWhichText()].shape;
		shape.scale = new Vector3(10.32f , 1.38f* (rows+1), 0);
		dissolveParticles[GetWhichText()].Play();
		yield return new WaitForSeconds(textDissolve[GetWhichText()].DissolveTime + 0.5f);
		dialogNum++;
		if (dialogNum < dialogLong[GetWhichDialog()])
		{
			OneSentenceAppear(dialogNum);
		}
		else
		{
			transform.Find("UIText").gameObject.GetComponent<UIDissolve>().isTalking = false;
			TargetText[GetWhichText()].gameObject.SetActive(false);
			if (GetWhichDialog() == 0)
			{
				playerManagement.isFirstTalkWithKQ = true;
			}
			if (GetWhichDialog() == 3)
			{
				playerManagement.isGiveStick= true;
			}
			if (GetWhichDialog() == 4)
			{
				playerManagement.isGiveCrown= true;
			}
			if (GetWhichDialog() == 6)
			{
				playerManagement.isAskedForHelp = true;
			}
		}
		
	}

	
}
