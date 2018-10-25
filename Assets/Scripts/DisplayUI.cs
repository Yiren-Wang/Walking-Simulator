using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DisplayUI : MonoBehaviour
{

	public string MyString;
	public Text MyText;
	public float FadeTime;
	public bool DisplayInfo;
	public bool IsTalking;
	
	// Use this for initialization
	void Start ()
	{

		MyText = GameObject.Find("Text").GetComponent<Text>();
		MyText.color = Color.clear;
	}
	
	// Update is called once per frame
	void Update(){
		FadeText();
	}

	void OnMouseOver()
		{
			DisplayInfo = true;
		}

		void OnMouseExit()
		{
			DisplayInfo = false;
		}

		void FadeText()
		{
			if (DisplayInfo && (IsTalking == false))
			{
				MyText.text = MyString;
				MyText.color = Color.Lerp(MyText.color, Color.white, FadeTime * Time.deltaTime);
			}
			else
			{
				MyText.color = Color.Lerp(MyText.color, Color.clear, FadeTime * Time.deltaTime);
			}
		}
	}
