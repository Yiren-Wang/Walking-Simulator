using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerInteraction : MonoBehaviour
{
	public Transform player;
	public float RayDistance;
	public PlayerManagement playerManagement;
	private RaycastHit hitObject;
	private GameObject UItext;
	private int layerMask = 1 << 10;
	public Image blackImage;
	public bool startFadeOut;
	public bool startFadeIn;
	public float fadeSpeed = 1.2f;
	private ColorGradingModel.Settings colorGrading;
	private PostProcessingProfile postProcessingProfile;
	public bool isDeathEffect;
	public Transform deathCollider;
	// Use this for initialization
	void Start ()
	{
		startFadeOut = false;
		startFadeIn = false;
		postProcessingProfile = GetComponent<PostProcessingBehaviour>().profile;
		colorGrading = postProcessingProfile.colorGrading.settings;
		colorGrading.basic.saturation = 0.942f;
		colorGrading.basic.contrast = 1.103f;
		postProcessingProfile.colorGrading.settings = colorGrading;
	}

	

	// Update is called once per frame
	void Update () {
		Debug.DrawRay(transform.position, transform.forward* RayDistance,Color.blue);
		FadeCamera();
		if (isDeathEffect)
		{
			DeathCamera();
		}
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
					if (hitObject.collider.gameObject.name.Contains("Shade"))
					{
						hitObject.collider.gameObject.GetComponent<NPC_Shade>().TargetText.gameObject.SetActive(true);
						hitObject.collider.gameObject.GetComponent<NPC_Shade>().OneSentenceAppear(0);
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
			if (hitObject.collider.gameObject.tag.Equals("Gate"))
			{
				
				UItext = hitObject.collider.gameObject.GetComponent<GetUIText>().UIText;
				UItext.SetActive(true);
				UItext.GetComponent<UIDissolve>().isStartAppear = true;


				if (Input.GetKeyDown(KeyCode.E))
				{
					UItext.SetActive(false);
					hitObject.collider.gameObject.GetComponent<BoxCollider>().enabled = false;	
					startFadeOut = true;
					fadeSpeed = 1.2f;
					StartCoroutine(ChangeScene());
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
			
		startFadeOut = false;
		startFadeIn = true;
	}
	IEnumerator ChangeScene()
	{		
		yield return new WaitForSeconds(3f);
		blackImage.color = Color.black;
		startFadeOut = false;
		SceneManager.LoadScene(2);
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
	void DeathCamera()
	{
		blackImage.color = Color.Lerp(blackImage.color, Color.black, 0.7f * Time.deltaTime);
		if (colorGrading.basic.saturation > 0)
		{
			colorGrading.basic.saturation -= 0.01f;
		}

		if (colorGrading.basic.contrast < 2)
		{
			colorGrading.basic.contrast += 0.01f;
		}	
		player.GetComponent<CharacterController>().enabled = false;
		player.transform.position += new Vector3(0,-0.012f,0);
		postProcessingProfile.colorGrading.settings = colorGrading;
		if (blackImage.color.a >= 0.99)
		{
			player.transform.position = deathCollider.Find("RebirthPos").position;
			startFadeIn = true;
			colorGrading.basic.saturation = 0.942f;
			colorGrading.basic.contrast = 1.103f;
			postProcessingProfile.colorGrading.settings = colorGrading;
			player.GetComponent<CharacterController>().enabled = true;
			isDeathEffect = false;
		}



	}
}
