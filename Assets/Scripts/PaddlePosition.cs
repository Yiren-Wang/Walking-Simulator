using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddlePosition : MonoBehaviour
{
	public Transform leftHand;

	public Transform rightHand;

	public bool isPaddling = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isPaddling)
		{
			/*Vector3 vDir = leftHand.position - rightHand.position;
		
			vDir.Normalize();
			transform.rotation = Quaternion.LookRotation(-vDir);*/
			
		}
		
		float x = (leftHand.position.x + rightHand.position.x) / 2;
		float y = (leftHand.position.y + rightHand.position.y) / 2;
		float z = (leftHand.position.z + rightHand.position.z) / 2;
		transform.position = new Vector3(x,y,z);
		
		
		
		
	}
	
}
