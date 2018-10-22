using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;


public class NPC_OldWoman : MonoBehaviour
{
	public string[] dialog;
	public float waitTime;
	public bool isStartDialog;
	public ParticleSystem dissolveParticles;
	public VTextInterface TargetText;
	
	private TextDissolve textDissolve;
	private int dialogNum;
	// Use this for initialization
	void Start () {
		TargetText.gameObject.SetActive(false);
		textDissolve = GameObject.Find("FPSController/FirstPersonCharacter").gameObject.GetComponent<TextDissolve>();
		dissolveParticles.Stop();
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
	public void OneSentenceAppear(int num)
	{
		if (num == 0)
		{
			textDissolve.ResetProgress();
			TargetText.gameObject.SetActive(true);
			dialogNum = num;
		}
		Debug.Log("StartTime: "+ Time.time +" dialog num: " + dialogNum);
		TargetText.RenderText = dialog[num].Replace("\\n","\n");	
		textDissolve.ResetTimeElapsed();
		textDissolve.isStartAppear = true;
		StartCoroutine(WaitAndDisappear(waitTime));
	}
	
	
	IEnumerator WaitAndDisappear(float dialogWaitTime)
	{
		yield return new WaitForSeconds(dialogWaitTime);
		textDissolve.ResetTimeElapsed();
		textDissolve.isStartDisappear = true;	
		dissolveParticles.Stop();
		string tempString = dialog[dialogNum].Replace("\\n","%");	
		int rows = Regex.Matches(tempString, "%").Count;
		print("rows bum: " + rows);
		var shape = dissolveParticles.shape;
		shape.scale = new Vector3(5.06f , 0.71f* (rows+1), 0);
		dissolveParticles.Play();
		yield return new WaitForSeconds(textDissolve.DissolveTime + 0.5f);
		dialogNum++;
		if (dialogNum < dialog.Length)
		{
			OneSentenceAppear(dialogNum);
		}
		else
		{
			
		}
		
	}

	
}
