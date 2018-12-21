using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;


public class TestEventSubscriber : MonoBehaviour {

	public Material m1;
	public Material m2;

	bool switchBool;
	Renderer renderer;


	public string Track;

	// Use this for initialization
	void Start () {
		Koreographer.Instance.RegisterForEvents(Track, FireEventDebugLog);
		renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FireEventDebugLog(KoreographyEvent koreoEvent)
	{
		//Debug.Log("Koreography Event Fired.");
		if(switchBool)
		{
			renderer.material = m1;
			switchBool = !switchBool;
		}
		else
		{
			renderer.material = m2;
			
			switchBool = !switchBool;

		}
	}
}
