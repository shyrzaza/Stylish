using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	private AudioSource mainMusic;
	float dsptimesong;
	// Use this for initialization
	void Start () {
		mainMusic = GetComponent<AudioSource>();
		dsptimesong = (float) AudioSettings.dspTime;

		mainMusic.Play();
		StartCoroutine(SlowTime(4, 0.05f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator SlowTime(float length, float amount)
	{
		//todo 
		yield return new WaitForSeconds(length);
		Debug.Log("Slow");
		mainMusic.pitch = amount;
		Time.timeScale = amount;
		Time.fixedDeltaTime = Time.timeScale * 0.02f;
		yield return new WaitForSeconds(length * amount);
		Debug.Log("Normal");
		Time.timeScale = 1;
		mainMusic.pitch = 1;
		yield return 0;
	}
}
