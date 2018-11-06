using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonNextScene : MonoBehaviour
{
	public AudioSource audio;
	private bool startFadeOut = false;
	private bool isPressButton = false;
	public float fadeSpeed = 1.1f;
	public Image blackImage;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (!audio.isPlaying && (isPressButton == true))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(1);
		}
		FadeCamera();
	}
	void FadeCamera()
	{
		if (startFadeOut)
		{
			blackImage.color = Color.Lerp(blackImage.color, Color.black, fadeSpeed * Time.deltaTime);
			if (blackImage.color.a >= 0.99)
			{
				blackImage.color = Color.black;
				startFadeOut = false;
			}

		}
	}
	public void LoadFirstScene()
	{
		isPressButton = true;
		startFadeOut = true;
		audio.Play();
	}
}
