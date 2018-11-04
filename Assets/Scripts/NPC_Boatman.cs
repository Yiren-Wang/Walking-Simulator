using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DG.Tweening;
using UnityEngine;

public class NPC_Boatman : MonoBehaviour {

	private string[,] dialog = new string[2,10];
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
	public Animator animator;
	public Transform boatmanPosition1;
	public Transform boatmanPosition2;
	public Transform boat;
	// Use this for initialization
	void Awake () {
		TargetText.gameObject.SetActive(false);
		textDissolve = TargetText.GetComponent<TextDissolve>();
		dissolveParticles.Stop();
		//no coin
		dialog[0, 0] = "I row the dead across,\nand only if they can pay.";
		dialog[0, 1] = "You are not dead, and\nyou have no coin.";
		dialog[0, 2] = "You really wish to cross?";
		dialog[0, 3] = "I cannot turn down a\nshade’s coin.";
		dialog[0, 4] = "But be careful. Coins\nare all that these souls \nhave left.";
		
		//After getting a coin
		dialog[1, 0] = "Get in, then.";
		dialog[1, 1] = "And careful not to\nrock the boat.";
		dialog[1, 2] = "These waters are dark\nand deep, and even I don’t\nknow what lies beneath.";
	}

	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(transform.position, boatmanPosition1.position) < 0.1)
		{
			ChangeAnimatorState();
		}
		
	}

	public void BoatmanMove()
	{
		transform.parent = boat;
		Sequence newSequence = DOTween.Sequence();
		newSequence.Append(transform.DOMove(boatmanPosition1.position, 2).SetEase(Ease.InOutQuad));
		newSequence.Insert(0.4f, transform.DORotate(boatmanPosition1.eulerAngles, 3).SetEase(Ease.InOutQuad));
		newSequence.Insert(2, transform.DOMove(boatmanPosition2.position, 3).SetEase(Ease.InOutQuad));
		newSequence.Play();
	}

	void BoatMove()
	{
		boat.gameObject.GetComponent<BoatMoving>().MoveBoat();
	}
	void ChangeAnimatorState()
	{
		animator.SetBool("isPaddling",true);
	}
	private int GetWhichDialog()
	{
		if (playerManagement.isGetCoin)
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
			playerManagement.isTalkToCharon = true;
			transform.Find("UIText").gameObject.GetComponent<UIDissolve>().isTalking = false;
			if (GetWhichDialog() == 1)
			{
				playerManagement.isMoveBoat = true;
			}
		}
		
	}
}
