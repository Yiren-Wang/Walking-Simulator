using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateForever : MonoBehaviour
{
	public float rotateSpeed = 2;
	
	// Use this for initialization
	void Start ()
	{
		
	}

	void Update()
	{
		transform.Rotate(new Vector3(0, rotateSpeed/50, 0));
	}
}
