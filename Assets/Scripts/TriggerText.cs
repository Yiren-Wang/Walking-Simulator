using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TriggerText : MonoBehaviour
{
	//private PlayerManagement playerManagement;
	private TextDissolve textDissolve;
	private bool isTextApperaed = false;
	
	public GameObject targetText;
	public ParticleSystem dissolveParticles;
	public float waitTime = 8;
	// Use this for initialization
	void Start ()
	{
		targetText.SetActive(false);
		//playerManagement = GameObject.Find("FPSController/FirstPersonCharacter").gameObject.GetComponent<PlayerManagement>();
		textDissolve = GameObject.Find("FPSController/FirstPersonCharacter").gameObject.GetComponent<TextDissolve>();
		dissolveParticles.Stop();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!isTextApperaed && other.gameObject.tag.Equals("Player"))
		{
			textDissolve.ResetProgress();
			textDissolve.ResetTimeElapsed();
			targetText.SetActive(true);
			isTextApperaed = true;
			textDissolve.isStartAppear = true;
			StartCoroutine(WaitAndDisappear());
		}
	}
	
	IEnumerator WaitAndDisappear()
	{
		yield return new WaitForSeconds(waitTime);
		textDissolve.ResetTimeElapsed();
		textDissolve.isStartDisappear = true;
		dissolveParticles.Play();
		yield return new WaitForSeconds(textDissolve.DissolveTime + 0.5f);
		targetText.SetActive(false);
	}
}
