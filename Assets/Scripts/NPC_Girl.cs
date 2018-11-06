using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class NPC_Girl : MonoBehaviour {

	private string[,] dialog = new string[2,10];
	public int[] dialogLong; 
	//public int whichDialog;
	public float waitTime;
	public bool isStartDialog;
	public ParticleSystem dissolveParticles;
	public VTextInterface TargetText;
	public PlayerManagement playerManagement;
	public PlayerInteraction playerInteraction;
	private int rows;
	private TextDissolve textDissolve;
	private int dialogNum;

	// Use this for initialization
	void Awake () {
		TargetText.gameObject.SetActive(false);
		textDissolve = TargetText.GetComponent<TextDissolve>();
		dissolveParticles.Stop();
		//w/o talking to Charon
		dialog[0, 0] = "- you.";
		dialog[0, 1] = "I don’t want to talk to you.";
		dialog[0, 2] = "Because you killed me,\nthat’s why!";
		dialog[0, 3] = "Maybe you didn’t mean to.";
		dialog[0, 4] = "I want to go back. To go above.";
		dialog[0, 5] = "But not with you.";
		dialog[0, 6] = "I don’t trust you.";
		//After talking to Charon
		dialog[1, 0] = "Not again.";
		dialog[1, 1] = "Wait...really?";
		dialog[1, 2] = "You came all the way here\nto say you’re sorry?";
		dialog[1, 3] = "That’s brave.";
		dialog[1, 4] = "I - I knew you didn’t\nmean to hurt me.";
		dialog[1, 5] = "But everything is so\nsad down here.";
		dialog[1, 6] = "Okay.";
		dialog[1, 7] = "I’ll come back.";
		
	}
	
	// Update is called once per frame
	void Update () {

		
	}

	private int GetWhichDialog()
	{
		if (playerManagement.isAskedForHelp)
		{
			return 1;
		}
		return 0;
	}
	public void OneSentenceAppear(int num)
	{
		if (num == 0)
		{
			textDissolve.ResetProgress();
			TargetText.gameObject.SetActive(true);
			dialogNum = num;
		}
		
		TargetText.RenderText = dialog[GetWhichDialog(),num];	
		textDissolve.ResetTimeElapsed();
		textDissolve.isStartAppear = true;
		string tempString = dialog[GetWhichDialog(),dialogNum].Replace("\n","%");
		rows = Regex.Matches(tempString, "%").Count;
		StartCoroutine(WaitAndDisappear(waitTime * (rows+1)));
	}
	
	
	IEnumerator WaitAndDisappear(float dialogWaitTime)
	{
		yield return new WaitForSeconds(dialogWaitTime);
		textDissolve.ResetTimeElapsed();
		textDissolve.isStartDisappear = true;	
		dissolveParticles.Stop();
		var shape = dissolveParticles.shape;
		shape.scale = new Vector3(8.5f , 1.44f* (rows+1), 0);
		dissolveParticles.Play();
		yield return new WaitForSeconds(textDissolve.DissolveTime + 0.5f);
		dialogNum++;
		if (dialogNum < dialogLong[GetWhichDialog()])
		{
			OneSentenceAppear(dialogNum);
		}
		else
		{
			transform.Find("UIText").gameObject.GetComponent<UIDissolve>().isTalking = false;
			if (GetWhichDialog() == 0)
			{
				playerManagement.isTalkedToCorinna = true;
			}
			if (GetWhichDialog() == 1)
			{
				playerInteraction.startFadeOut = true;
			}

		}
		
	}

	
}
