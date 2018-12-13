using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLengthDebugger : MonoBehaviour {

	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		Debug.Log("SampleLength is " + audioSource.clip.samples);
		Debug.Log("Length is " + audioSource.clip.length);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
