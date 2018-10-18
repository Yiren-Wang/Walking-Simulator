using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	public float RayDistance;
	public PlayerManagement playerManagement;
	private RaycastHit hitObject;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay(transform.position, transform.forward* RayDistance,Color.blue);
		if (Physics.Raycast(transform.position, transform.forward, out hitObject, RayDistance))
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				if (hitObject.collider.gameObject.tag.Equals("PickObj"))
				{
					Destroy(hitObject.collider.gameObject);
					if (hitObject.collider.gameObject.name.Contains("Mushroom"))
					{
						playerManagement.AddMushroom();
					}
				}
			}
			
		}
	}
}
