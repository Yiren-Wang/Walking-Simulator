using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class NPC_Shade : MonoBehaviour {
	private string[,] dialog = new string[1,5];
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
	// Use this for initialization
	void Awake () {
		TargetText.gameObject.SetActive(false);
		textDissolve = TargetText.GetComponent<TextDissolve>();
		dissolveParticles.Stop();
		dialog[0, 0] = "The Styx was dark and\ndeep, and the Lethe is\nshallow and clear.";
		dialog[0, 1] = "But careful. Touch the\nwater and you forget\nall you know.";			
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OneSentenceAppear(int num)
	{
		if (num == 0)
		{
			textDissolve.ResetProgress();
			TargetText.gameObject.SetActive(true);
			dialogNum = num;
		}
		
		TargetText.RenderText = dialog[0,num];	
		textDissolve.ResetTimeElapsed();
		textDissolve.isStartAppear = true;
		string tempString = dialog[0,dialogNum].Replace("\n","%");
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
		shape.scale = new Vector3(5.14f , 0.65f* (rows+1), 0);
		dissolveParticles.Play();
		yield return new WaitForSeconds(textDissolve.DissolveTime + 0.5f);
		dialogNum++;
		if (dialogNum < dialogLong[0])
		{
			OneSentenceAppear(dialogNum);
		}
		else
		{
			transform.Find("UIText").gameObject.GetComponent<UIDissolve>().isTalking = false;
		}
		
	}

	
}

