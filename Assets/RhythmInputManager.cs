using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmInputManager : MonoBehaviour {

	public RhythmLaneController[] laneControllers;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("axis" + Input.GetAxis("RedButton"));
		if(Input.GetKeyDown(KeyCode.Joystick1Button0))
		{
			Debug.Log("pressed");
			laneControllers[0].ButtonPressed();
		}
		if(Input.GetKeyDown(KeyCode.Joystick1Button1))
		{
			Debug.Log("pressed");
			laneControllers[5].ButtonPressed();
		}
		if(Input.GetKeyDown(KeyCode.Joystick1Button2))
		{
			Debug.Log("pressed");
			laneControllers[2].ButtonPressed();
		}
		if(Input.GetKeyDown(KeyCode.Joystick1Button3))
		{
			Debug.Log("pressed");
			laneControllers[3].ButtonPressed();
		}
		if(Input.GetKeyDown(KeyCode.Joystick1Button4))
		{
			Debug.Log("pressed");
			laneControllers[4].ButtonPressed();
		}
		if(Input.GetKeyDown(KeyCode.Joystick1Button5))
		{
			Debug.Log("pressed");
			laneControllers[1].ButtonPressed();
		}
	}
}
