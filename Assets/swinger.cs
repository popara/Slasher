using UnityEngine;
using System.Collections;

public class swinger : MonoBehaviour {
	public GameObject tip;
	public float swang;
	public Vector3 sensitivity;
	// Use this for initialization
	void Start () {
		this.tip.transform.Translate (0, this.swang, 0);
	}
	
	// Update is called once per frame
	void Update () {
		float r = this.swang;

		Vector3 d = new Vector3 (Input.GetAxis ("Mouse X") * Time.smoothDeltaTime * sensitivity.x, 0, 
		                         Input.GetAxis ("Mouse Y") * Time.smoothDeltaTime * sensitivity.z);

		if (d.magnitude > 0) { 
						Vector3 c = new Vector3 (0, 0, 0);
						Vector3 o = this.tip.transform.localPosition;
						Vector3 nv = o + d;
						print ("------");
						print ("C: " + c.x + " : " + c.y + " : " + c.z);
						print ("D: " + d.x + ": " + d.y + " : " + d.z);
						print ("Old: " + o.x + " : " + o.y + " : " + o.z);
						print ("New: " + nv.x + " : " + nv.y + " : " + nv.z);
						if (!inside_of_circle (nv, c, r)) {
								print ("!!??!?!?!?!?!??!?!?!");
								print ("USLI SMO!!!!!!");
								int dominant = domination_determination (d);
								print ("Dom " + (dominant > 0 ? "X" : (dominant < 0 ? "Z" : "equal")));
								Vector3 step_one = this.constrained_by_direction (o, d, r, dominant);
								print ("STEP 1:" + step_one.x + " : " + step_one.y + " : " + step_one.z);
								Vector3 step_two = this.adopted_submissive_axis (step_one, c, d, r, dominant);
								print ("Step 2: " + step_two.x + " : " + step_two.y + " : " + step_two.z);
								
								nv = step_two;
						}
						nv = this.adopted_height (nv, c, d, r);
						print ("Step 3: " + nv.x + " : " + nv.y + " : " + nv.z);

						this.tip.transform.localPosition = nv;
				}
	}



	public float point_on_axis(float axis, float radius, int sign) {
		return sign * Mathf.Sqrt ( Mathf.Pow (radius, 2) - Mathf.Pow (axis, 2) );
	}

	public bool inside_of_sphere(Vector3 p, Vector3 c, float r) {
		return Mathf.Pow (r, 2) == Mathf.Pow (p.x - c.x, 2) + Mathf.Pow (p.y - c.y, 2) + Mathf.Pow (p.z - c.z, 2);
	}

	public bool inside_of_circle(Vector3 v, Vector3 c, float r) {
		return Mathf.Pow (r, 2) >= Mathf.Pow (v.x - c.x, 2) + Mathf.Pow (v.z - c.z, 2);
	}

	private float step_clamped_in_circle (float v, float radius) {
		return Mathf.Clamp (v, -radius, radius);
	}

	private float locus_of_point_given_axis_and_axis(float axis, float height, float radius) {
		return safe_num(Mathf.Sqrt (Mathf.Pow (radius, 2) - Mathf.Pow (axis, 2) - Mathf.Pow (height, 2)));

	}

	private float safe_num (float num) {
		return float.IsNaN(num) ? 0 : num;
	}
	
	public Vector3 constrained_by_direction (Vector3 old, Vector3 delta, float radius, int dominant) {
		if (dominant > 0) {
			// X is dominant
			return new Vector3 (this.step_clamped_in_circle (old.x + delta.x, radius), old.y, old.z);
		} else if (dominant < 0) {
			// Z is dominant
			return new Vector3 (old.x, old.y, this.step_clamped_in_circle (old.z + delta.z, radius));	
		} else {
			return new Vector3 (this.step_clamped_in_circle (old.x + delta.x, radius), old.y, this.step_clamped_in_circle(old.z + delta.z, radius));
		}
	}

	public Vector3 adopted_submissive_axis(Vector3 f, Vector3 center, Vector3 delta, float radius, int dominant) {
		if (dominant > 0) {
			//  X is Dominant
			return new Vector3(f.x, f.y, this.point_on_axis(f.x, radius, sign_of_value(f.z))); 
		}
		else if (dominant < 0) {
			// Z is Dominant
			return new Vector3(this.point_on_axis(f.z, radius, sign_of_value(f.x)), f.y, f.z);
		}
		else {
			return f;
		}
	}
	
	// Returns 1 if X is dominant -1 if Z is dominant 0 if equal
	private int domination_determination(Vector3 v) {
		return sign_of_value(Mathf.Abs (v.x) - Mathf.Abs (v.z));
	}

	public Vector3 adopted_height(Vector3 f, Vector3 center, Vector3 delta, float radius) {
		return new Vector3 (f.x, this.locus_of_point_given_axis_and_axis (f.x, f.z, radius), f.z);
	}

	private int sign_of_value(float value) {
		return value > 0 ? 1 : value < 0 ? -1 : 0;
	}
}

