using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FadeCamera : MonoBehaviour
{
	public bool isCameraFade = false;
	public PlayerInteraction playerInteraction;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isCameraFade)
		{
			RenderSettings.fogDensity += 0.00005f;
			
		}
	}

	void OnTriggerEnter(Collider other)
	{
		print(other.gameObject.name);
		if (other.gameObject.tag.Contains("Player"))
		{
			isCameraFade = true;
			playerInteraction.fadeSpeed = 0.5f;
			playerInteraction.startFadeOut = true;
		}
	}

}
