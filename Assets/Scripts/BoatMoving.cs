using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BoatMoving : MonoBehaviour {

	public Transform target;
	public PathType pathType = PathType.CatmullRom;
	public Transform[] waypoints;
	private Vector3[] points;


	void Start()
	{
		points = new Vector3[waypoints.Length];
		for (int i = 0; i < waypoints.Length; i++)
		{
			points[i] = waypoints[i].position;
		}
		MoveBoat();

		
	}

	public void MoveBoat()
	{
		// Create a path tween using the given pathType, Linear or CatmullRom (curved).
		// Use SetOptions to close the path
		// and SetLookAt to make the target orient to the path itself
		
		Tween rotate = target.DOShakeRotation(5, 1.5f, 2, 120f, false);
		rotate.SetLoops(-1);
		Tween t = target.DOPath(points, 20, pathType)
			.SetOptions(false)
			.SetLookAt(1f);
		// Then set the ease to InOutQuad and use infinite loops
		t.SetEase(Ease.InOutQuad);
	}
}
