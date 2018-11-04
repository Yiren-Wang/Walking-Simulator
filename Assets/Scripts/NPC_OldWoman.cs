using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;


public class NPC_OldWoman : MonoBehaviour
{
	private string[,] dialog = new string[3,10];
	public int[] dialogLong; 
	//public int whichDialog;
	public float waitTime;
	public bool isStartDialog;
	public ParticleSystem dissolveParticles;
	public VTextInterface TargetText;
	public PlayerManagement playerManagement;
	private int rows;
	private TextDissolve textDissolve;
	private int dialogNum;

	public GameObject putPosition;
	// Use this for initialization
	void Awake () {
		TargetText.gameObject.SetActive(false);
		textDissolve = TargetText.GetComponent<TextDissolve>();
		dissolveParticles.Stop();
		//w/o talking to Charon
		dialog[0, 0] = "Child, what are you\ndoing in this place?";
		dialog[0, 1] = "You’re not ready to\ncross over.";
		dialog[0, 2] = "If you must cross,\nask the ferryman.";
		
		//After talking to Charon
		dialog[1, 0] = "An extra coin?";
		dialog[1, 1] = "Yes. I have two coins\nfor the ferryman.";
		dialog[1, 2] = "My husband buried me\nwith everything he thought\nI might need.";
		dialog[1, 3] = "I could feel the priestess’s\nhands as she placed the\ncoins on my eyes.";
		dialog[1, 4] = "But faintly. As if she’d\nalready wrapped me in\nthe burial shroud.";
		dialog[1, 5] = "I will give you a coin\nif you bring me the stars\n in the sky.";
		dialog[1, 6] = "I want to see the\nnight sky once more.";
		dialog[1, 7] = "You say it’s impossible?";
		dialog[1, 8] = "You’re here, aren’t you?\nJust look around you.";
		
		//After showing the mushroom dust
		dialog[2, 0] = "As a girl, I lay in the\nfields at night and searched\n for constellations.";
		dialog[2, 1] = "The dragon, the herdsman,\nthe northern crown...never\nwas I happier than on those\nnights.";
		dialog[2, 2] = "Thank you, child.";
		dialog[2, 3] = "I will go now.";

	}
	
	// Update is called once per frame
	void Update () {
		/*if (isStartDialog)
		{
			textDissolve.ResetProgress();
			TargetText.gameObject.SetActive(true);
			dialogNum = 0;
			OneSentenceAppear(dialogNum);			
		}*/
		
	}

	private int GetWhichDialog()
	{
		if (playerManagement.isShowedMushroom)
		{
			return 2;
		}
		if (playerManagement.isTalkToCharon)
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
			if (GetWhichDialog() == 1)
			{
				putPosition.GetComponent<BoxCollider>().enabled = true;
			}
			if (GetWhichDialog() == 2)
			{
				playerManagement.isGetCoin = true;
			}
		}
		
	}

	
}
