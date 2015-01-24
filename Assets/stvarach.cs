using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class stvarach : MonoBehaviour {
	public GameObject prefab;
	public List<GameObject> atoms;

	// Use this for initialization
	void Start () {
		atoms = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject atom; 
		Vector3 start; 
		start = StartPoint();
		atoms.Add(CreateAtomAtPosition (start));
	
		Debug.Log(atoms.Count);

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


}
