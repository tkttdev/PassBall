using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private float cameraDis;
	private bool isMove = false;
	private Vector3 tapWorldPos;

	void Start () {
		cameraDis = Vector3.Distance (Camera.main.transform.position, transform.position);
	}

	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			isMove = true;
			tapWorldPos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, cameraDis));
		}
	}

	void FixedUpdate () {
		if (isMove) {
			transform.position = TapPosToPlanePos (tapWorldPos);
			isMove = false;
		}
	}

	private Vector3 TapPosToPlanePos(Vector3 tapWorldPosition , GameObject plane = null){
		Vector3 n, x;
		if (!plane) {
			n = Vector3.up;
			x = Vector3.zero;
		} else {
			n = plane.transform.up;
			x = plane.transform.position;
		}

		var x0 = tapWorldPosition;
		var m = (tapWorldPosition - Camera.main.transform.position).normalized;
		var h = Vector3.Dot(n, x);

		var intersectPoint = x0 + ((h - Vector3.Dot(n, x0)) / (Vector3.Dot(n, m))) * m;

		return intersectPoint;
	}

}
