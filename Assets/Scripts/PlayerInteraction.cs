using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerInteraction : MonoBehaviour
{
	public float RayDistance;
	public PlayerManagement playerManagement;
	private RaycastHit hitObject;
	private GameObject UItext;
	private int layerMask = 1 << 10;
	public Image blackImage;
	public bool startFadeOut;
	public bool startFadeIn;
	public float fadeSpeed = 1.2f;
	// Use this for initialization
	void Start ()
	{
		startFadeOut = false;
		startFadeIn = false;
	}

	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay(transform.position, transform.forward* RayDistance,Color.blue);
		FadeCamera();
		if (Physics.Raycast(transform.position, transform.forward, out hitObject, RayDistance,layerMask))
		{
			if (hitObject.collider.gameObject.tag.Equals("PickObj"))
			{

				UItext = hitObject.collider.gameObject.GetComponent<GetUIText>().UIText;
				UItext.SetActive(true);
				UItext.GetComponent<UIDissolve>().isStartAppear = true;
				if (Input.GetKeyDown(KeyCode.E))
				{
					Destroy(hitObject.collider.gameObject);
					if (hitObject.collider.gameObject.name.Contains("Mushroom"))
					{
						playerManagement.AddMushroom();
					}
				}
			}
			if (hitObject.collider.gameObject.tag.Equals("NPC"))
			{
				UItext = hitObject.collider.gameObject.GetComponent<GetUIText>().UIText;
				UItext.SetActive(true);
				UItext.GetComponent<UIDissolve>().isStartAppear = true;
				if (Input.GetKeyDown(KeyCode.E))
				{
					//hitObject.collider.gameObject.GetComponent<DisplayUI>().IsTalking = true;
					UItext.GetComponent<UIDissolve>().isTalking = true;

					if (hitObject.collider.gameObject.name.Contains("OldWoman"))
					{
						hitObject.collider.gameObject.GetComponent<NPC_OldWoman>().TargetText.gameObject.SetActive(true);
						hitObject.collider.gameObject.GetComponent<NPC_OldWoman>().OneSentenceAppear(0);
					}
					if (hitObject.collider.gameObject.name.Contains("Charon"))
					{
						hitObject.collider.gameObject.GetComponent<NPC_Boatman>().TargetText.gameObject.SetActive(true);
						hitObject.collider.gameObject.GetComponent<NPC_Boatman>().OneSentenceAppear(0);
					}
				}
			}
			if (hitObject.collider.gameObject.tag.Equals("Put"))
			{
				if (playerManagement.isEnoughMushroom)
				{
					if (!playerManagement.isShowedMushroom)
					{
						UItext = hitObject.collider.gameObject.GetComponent<GetUIText>().UIText;
						UItext.SetActive(true);
						UItext.GetComponent<UIDissolve>().isStartAppear = true;
					}

					if (Input.GetKeyDown(KeyCode.E))
					{						
						UItext.SetActive(false);
						playerManagement.isShowedMushroom = true;
						startFadeOut = true;
						StartCoroutine(AfterFade());

					}
				}
				
			}
			
		}
		else
		{
			if (UItext != null)
			{
				UItext.GetComponent<UIDissolve>().isStartAppear = false;
			}
		}
	}
	
	IEnumerator AfterFade()
	{		
		yield return new WaitForSeconds(3f);
		blackImage.color = Color.black;
		hitObject.collider.gameObject.GetComponent<ShowMushrooms>().showTheMushrooms();
		hitObject.collider.gameObject.GetComponent<BoxCollider>().enabled = false;		
		startFadeOut = false;
		startFadeIn = true;
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
		if(startFadeIn)
		{
			blackImage.color = Color.Lerp(blackImage.color, Color.clear, fadeSpeed * Time.deltaTime);
			
			if (blackImage.color.a <=0.01)
			{
				blackImage.color = Color.clear;
				startFadeIn = false;
			}
		}
	}
}
