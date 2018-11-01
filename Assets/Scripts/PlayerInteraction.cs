using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	public float RayDistance;
	public PlayerManagement playerManagement;
	private RaycastHit hitObject;

	private GameObject UItext;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay(transform.position, transform.forward* RayDistance,Color.blue);
		if (Physics.Raycast(transform.position, transform.forward, out hitObject, RayDistance))
		{
			if (hitObject.collider.gameObject.tag.Equals("PickObj"))
			{

				UItext = hitObject.collider.gameObject.transform.Find("UIText").gameObject;
				UItext.SetActive(true);
				UItext.GetComponent<TextDissolve>().isStartAppear = true;
				if (Input.GetKeyDown(KeyCode.E))
				{
					Destroy(hitObject.collider.gameObject);
					if (hitObject.collider.gameObject.name.Contains("Mushroom"))
					{
						playerManagement.AddMushroom();
					}
				}
			}
			else
			{
				if (UItext != null)
				{
					UItext.GetComponent<TextDissolve>().isStartDisappear = true;
				}
				
			}
			
				
				if (hitObject.collider.gameObject.tag.Equals("NPC"))
				{
					if (Input.GetKeyDown(KeyCode.E))
					{
						hitObject.collider.gameObject.GetComponent<DisplayUI>().IsTalking = true;
						Debug.Log("You are talking with NPC");

						if (hitObject.collider.gameObject.name.Contains("OldWoman"))
						{
							hitObject.collider.gameObject.GetComponent<NPC_OldWoman>().OneSentenceAppear(0);
						}
					}
				}
			
			
		}
	}
}
