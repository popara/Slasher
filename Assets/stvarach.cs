﻿using UnityEngine;
using System.Collections;

public class stvarach : MonoBehaviour {
	public GameObject prefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GameObject atom; 
		Vector3 start; 
		start = StartPoint();
		atom = CreateAtomAtPosition (start);
		FlingAtom(atom, start, EndPoint());

	}

	GameObject CreateAtomAtPosition (Vector3 position) {
		return (GameObject) Instantiate (prefab, position, Quaternion.identity);
	}

	Vector3 StartPoint() {
		return PointOnBack();
	}

	Vector3 EndPoint () {
		return PointOnFront();
	}


	Vector3 PointOnBack () {
		return PointInSide(new Vector3(-box().x, box().y, box().z));
	}
		
	Vector3 PointOnFront () {
		return PointInSide(new Vector3(box().x, box().y, box().z));
	}

	Vector3 PointInSide (Vector3 plane) {
		return new Vector3(plane.x, Random.Range(-plane.y, plane.y), Random.Range(-plane.z, plane.z));
	}

	BoxCollider collider () {
		return transform.GetComponent<BoxCollider>();
	}

	Vector3 box () {
		return collider().size;
	}

	void FlingAtom(GameObject atom, Vector3 from, Vector3 to) {
		Debug.Log(from.ToString());
		Debug.Log(to.ToString());
		Debug.Log("fliing");
		Debug.Log(to - from);
		Debug.Log(">>>>");

		atom.rigidbody.velocity = to - from;
	}

}
