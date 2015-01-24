using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class stvarach : MonoBehaviour {
	public GameObject prefab;
	private List<GameObject> atoms;
	public float maxAtoms; // 
	public Vector2 randomSpeed; // X is MIN Y is MAX
	public bool useFixedSpeed;
	[Range(0, 100)] public int speed = 0;

	// Use this for initialization
	void Start () {
		atoms = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject atom; 
		Vector3 start; 
		Vector3 end; 
		start = StartPoint();
		end = EndPoint();

		
		if (atoms.Count < maxAtoms) {
			atom = CreateAtomAtPosition (start);
		
			Debug.Log(atoms.Count);

			atoms.Add(atom);
			slobodniAtom atomic = getFreeAtom(atom);
			atomic.SetDelegate(gameObject);
			atomic.Fling(start, end, getRandomSpeed());
			
		}

	}

	public void Finish(GameObject atom) {
		atoms.Remove(atom);
		Destroy(atom);
	}

	slobodniAtom getFreeAtom (GameObject atom) {
		return atom.transform.GetComponent<slobodniAtom>();
	}

	float getRandomSpeed () {
		return Random.Range(randomSpeed.x, randomSpeed.y);
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
