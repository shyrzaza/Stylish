using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class RhythmNote
{
	public KoreographyEvent evt;
	public GameObject gameObect;
	public GameObject circleObject;
	public HitCircle circle;
	public float percentage;

	//debug todo
	public bool dirty = false;
}

public class RhythmLaneController : MonoBehaviour {

	private AudioSource audioSource;
	public AudioClip hitSound;
	public RectTransform buttonTransform;
	public GameObject UIPanel;
	public int ID;
	public Vector3 noteDirection;
	public GameObject notePrefab;

	public GameObject circlePrefab;

	Koreography koreo;

	List<RhythmNote> notes;
	RhythmNote nextNote;

	float missPercentage;

	// Use this for initialization
	void Start () {
		notes = new List<RhythmNote>();
		koreo = Koreographer.Instance.GetKoreographyAtIndex(0);	
		missPercentage = (RhythmGameManager.noteTravelSampleTime + RhythmGameManager.PERFECT_HIT_RANGE_IN_SAMPLES) / RhythmGameManager.noteTravelSampleTime;	
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateNotePositions();
	}

	public void AddNote(KoreographyEvent newNote)
	{
		RhythmNote n = new RhythmNote();
		n.evt = newNote;
		n.gameObect = (GameObject)Instantiate(notePrefab, new Vector3(0,0,90), Quaternion.identity);
		n.percentage = 0.0f;

		n.circleObject = (GameObject)Instantiate(circlePrefab, new Vector3(0,0,90f), Quaternion.identity);
		n.circle = n.circleObject.GetComponent<HitCircle>();
 		n.circleObject.transform.SetParent(UIPanel.transform);
		RectTransform rect = n.circleObject.GetComponent<RectTransform>();
		rect.position = noteDirection;
		rect.localScale = new Vector3(1,1,1);
		rect.anchorMin = buttonTransform.anchorMin;
		rect.anchorMax = buttonTransform.anchorMax;
		rect.anchoredPosition3D = Vector3.zero;
		//n.circleObject.transform.position = Vector3.zero;
		n.circle.openness = 1;
		notes.Add(n);
	}

	void UpdateNotePositions()
	{
		if(notes.Count == 0)
		{
			return;
		}

		int currentSample = koreo.GetLatestSampleTime();

		foreach(RhythmNote n in notes)
		{
			//starts at noteTravelSampleTime and goes to 0
			int distance = n.evt.StartSample - currentSample;

			//starts at zero
			float betterDistance = RhythmGameManager.noteTravelSampleTime - distance;
			//Debug.Log("b" + betterDistance);

			n.percentage =  betterDistance / RhythmGameManager.noteTravelSampleTime;
			//Debug.Log("p" + n.percentage);

			Vector3 targetPosition = new Vector3(0,0,90f) + n.percentage * noteDirection;
			//TODO maybe slerp
			n.gameObect.transform.position = targetPosition;

			//update circle
			n.circle.openness = 1 - n.percentage;
			///todo
			if(n.circle.openness <= 0.9 && n.circle.openness >= 0.5f)
			{
				n.circle.circleImage.enabled = true;
			}

			
			if(n.circle.openness < 0)
			{
				GameObject.Destroy(n.circleObject);
			}

			//TODO DEBUG HITSOUND
			
			if(n.percentage >= 0.97f)
			{
				if(!n.dirty)
				{
					n.dirty = true;
					audioSource.Play();
				}
			}		
			 
			
		}
		if(notes[0].percentage > missPercentage)
		{
			RhythmNote todelete = notes[0];
			notes.RemoveAt(0);
			GameObject.Destroy(todelete.circleObject);
			GameObject.Destroy(todelete.gameObect);
			
		}
	}

	public void ButtonPressed()
	{
		if(notes.Count == 0)
		{
			//todo failure
			Debug.Log("failure!");
			
			return;
		}

		int sampleDistance = Mathf.Abs(koreo.GetLatestSampleTime() - notes[0].evt.StartSample);
		if(sampleDistance <= RhythmGameManager.PERFECT_HIT_RANGE_IN_SAMPLES)
		{
			//todo success
			Debug.Log("correct!");

			//destroy node
			RhythmNote todelete = notes[0];
			notes.RemoveAt(0);
			GameObject.Destroy(todelete.circleObject);
			GameObject.Destroy(todelete.gameObect);
			Debug.Log("p" + todelete.percentage);
			audioSource.Play();
		}
		else
		{
			Debug.Log("failure!");			
		}
	}
}
