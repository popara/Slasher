using UnityEngine;
using System.Collections;

public class slobodniAtom : MonoBehaviour {
	public float speed;
	private int upps = 0;
	public string code; 
	private GameObject onEnd;
	private Vector3 start;
	private Vector3 end;
    private float startTime;
    private float journeyLength;

	 void Start() {
        
    }
	
	
	void Update () {
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		if (transform.position != end && !float.IsNaN(fracJourney)) {
	        transform.position = Vector3.Lerp(start, end, fracJourney);
		}
		else {
			if (onEnd) {
				onEnd.transform.GetComponent<stvarach>().Finish(gameObject);				
			}

		}
	}

	public void SetDelegate(GameObject action) {
		onEnd = action;
	}

	void SetSpeed (float aSpeed) {
		speed = aSpeed;
	}

	void SetStart(Vector3 aStart) {
		start = aStart;
	}

	void SetEnd(Vector3 aEnd) {
		end = aEnd;
	}

	public void Fling(Vector3 from, Vector3 to, float aSpeed) {
		SetStart(from);
		SetEnd(to);
		SetSpeed(aSpeed);

		startTime = Time.time;
        journeyLength = Vector3.Distance(from, to);

	}
}
