using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.DrawRay(Vector3.zero, Vector3.right, Color.green, 100000);
		Vector3 start = Vector3.right;
		Vector3 rotated = Quaternion.Euler(0, 0, 240) * start;
		Debug.DrawRay(Vector3.zero, rotated, Color.red, 100000);
		Debug.Log(Vector3.right.magnitude);
		Debug.Log(rotated);
		Debug.Log(rotated.magnitude);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
