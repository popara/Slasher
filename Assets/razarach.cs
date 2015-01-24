using UnityEngine;
using System.Collections;

public class razarach : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
        Destroy(other.gameObject);
    }
}
