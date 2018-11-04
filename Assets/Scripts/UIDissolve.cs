using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDissolve : MonoBehaviour {
	
	public float DissolveTime;
	public Material dissolveMat;
	public bool isStartAppear = false;
	//public bool isStartDisappear = false;
	public bool isTalking;
	public float timeElapsed = 0;

	private bool isShowing;
	//private bool isAppear = false;
	// Use this for initialization
	void Start () {
		dissolveMat.SetFloat("_Progress", 0.19f);
		isShowing = false;
		timeElapsed = DissolveTime + 1f;
	}

	public void ResetProgress()
	{
		dissolveMat.SetFloat("_Progress", 0.19f);
	}
	public void ResetTimeElapsed()
	{
		timeElapsed = 0;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (isStartAppear && (isTalking == false))
		{
			if (isShowing == false)
			{
				ResetTimeElapsed();
				isShowing = true;
			}

			if (timeElapsed / DissolveTime < 1)
			{
				timeElapsed += Time.deltaTime;
				float progress = Mathf.Lerp(0.19f, 0.6f, timeElapsed / DissolveTime);
				dissolveMat.SetFloat("_Progress", progress);
				dissolveMat.SetTextureOffset("_DissolveTex", new Vector2(timeElapsed / 100, timeElapsed / 100));
			}
		}
		else
		{
			if (isShowing == true)
			{
				ResetTimeElapsed();
				isShowing = false;
			}
			if (timeElapsed / (DissolveTime + 0.5f) < 1)
			{
				timeElapsed += Time.deltaTime;
				float progress = Mathf.Lerp(0.6f, 0.19f, timeElapsed / (DissolveTime + 0.5f));
				dissolveMat.SetFloat("_Progress", progress);
				dissolveMat.SetTextureOffset("_DissolveTex", new Vector2(timeElapsed / 100, timeElapsed / 100));
			}
			else
			{
				gameObject.SetActive(false);
			}

		}

	}
}
