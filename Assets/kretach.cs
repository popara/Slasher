using UnityEngine;
using System.Collections;

public class kretach : MonoBehaviour {
	public Vector3 speed;
	// Update is called once per frame
	void Update () {
		Vector3 d = new Vector3(Input.GetAxis ("Horizontal") * Time.smoothDeltaTime * speed.x, 0, 
		                        Input.GetAxis ("Vertical") * Time.smoothDeltaTime * speed.z);

		this.transform.Translate (d);
	}
}
