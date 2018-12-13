using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class RhythmGameManager : MonoBehaviour {

	public RhythmLaneController[] laneControllers;	
	public GameObject[] noteprefabs;
	//rot schwarz weiss gruen gelb blau
	public RectTransform[] buttonTransforms;
	public float noteSpeed;
	private float invertedNoteSpeed;
	private int SampleRate = 44100;
	Queue<KoreographyEvent>[] lanes;
	KoreographyEvent[] nextEvent;
	public string TrackID;
	Koreography koreo;

	public int perfectHitRangeInSamples;
	public static int PERFECT_HIT_RANGE_IN_SAMPLES;

	public static float noteTravelSampleTime;

	Vector3[] noteDirections;

	// Use this for initialization
	void Start () {

		PERFECT_HIT_RANGE_IN_SAMPLES = perfectHitRangeInSamples;
		
		invertedNoteSpeed = 1/noteSpeed;
		koreo = Koreographer.Instance.GetKoreographyAtIndex(0);

		KoreographyTrackBase rhythmTrack = koreo.GetTrackByID(TrackID);
		List<KoreographyEvent> rawEvents = rhythmTrack.GetAllEvents();

		noteTravelSampleTime = invertedNoteSpeed * SampleRate;

		nextEvent = new KoreographyEvent[6];
		noteDirections = new Vector3[6];
		lanes = new Queue<KoreographyEvent>[6];
		for(int i = 0; i < 6; i++)
		{
			Vector2 dir;
			lanes[i] = new Queue<KoreographyEvent>();
			Vector3[] corners = new Vector3[4];
			buttonTransforms[i].GetWorldCorners(corners);
			Vector3 middlePoint = corners[0] + (corners[2] - corners[0])/2;
			dir = middlePoint - new Vector3(0,0,90); 
			noteDirections[i] = new Vector3(dir.x, dir.y, 0);

			laneControllers[i].noteDirection = noteDirections[i];
		}

		for(int i = 0; i < rawEvents.Count; ++i)
		{
			KoreographyEvent evt = rawEvents[i];
			int payload = evt.GetIntValue();
			lanes[payload].Enqueue(evt);
		}

		//prepare initial events
		for(int i = 0; i < 6; i++)
		{
			if(lanes[i].Count > 0)
			{
				nextEvent[i] = lanes[i].Dequeue();
			}
		}

		Debug.Log("eventtest");
		Debug.Log(nextEvent[0].StartSample);
	}
	
	// Update is called once per frame
	void Update () {
		//check for new notes to spawn
		//Debug.Log(koreo.GetLatestSampleTime());
		CheckForNotesToSpawn();

	}

	void CheckForNotesToSpawn()
	{
		int currentSample = koreo.GetLatestSampleTime();
		for(int i = 0; i < 6; i++)
		{
			if(nextEvent[i] == null)
			{
				continue;
			}

			int distance = nextEvent[i].StartSample - currentSample;

			//Debug.Log("distance");
			//Debug.Log(distance);

			if(distance <= noteTravelSampleTime)
			{
				
				//spawn note
				laneControllers[i].AddNote(nextEvent[i]);
				//TODO

				if(lanes[i].Count > 0)
				{
					nextEvent[i] = lanes[i].Dequeue();
				}
				else
				{
					nextEvent[i] = null;
				}
			}

		}
	}

	
}
