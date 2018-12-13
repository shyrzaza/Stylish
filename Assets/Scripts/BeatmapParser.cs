using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BeatmapParser : MonoBehaviour {

	List<int> hitObjectTimings;

	// Use this for initialization
	void Start () {
		hitObjectTimings = new List<int>();
		ParseBeatmap();
		CreateNewTrackAsset("DisconnectedT", 242050.6f, 10674432);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ParseBeatmap()
	{
		string path = "Assets/Beatmaps/Pegboard Nerds - Disconnected (Timorisu) [Hard].txt";

		StreamReader reader = new StreamReader(path);
		

		string line = "";
		string osuFileHeader = "";

		//fileheader	
		osuFileHeader = reader.ReadLine();

		//emptyspace
		line = reader.ReadLine();

		//general
		line = reader.ReadLine();
		Debug.Log(line);
		while(line != "")
		{
			line = reader.ReadLine();
		}

		//editor
		line = reader.ReadLine();
		Debug.Log(line);		
		while(line != "")
		{
			line = reader.ReadLine();
		}

		//metadata
		line = reader.ReadLine();
		Debug.Log(line);
		while(line != "")
		{
			line = reader.ReadLine();
		}

		//Difficulty
		line = reader.ReadLine();
		Debug.Log(line);
		while(line != "")
		{
			line = reader.ReadLine();
		}

		//events
		line = reader.ReadLine();
		Debug.Log(line);
		while(line != "")
		{
			line = reader.ReadLine();
		}

		//timingpoints
		line = reader.ReadLine();
		Debug.Log(line);
		while(line != "")
		{
			line = reader.ReadLine();
		}

		//colours
		line = reader.ReadLine();
		Debug.Log(line);
		string toWrite = "kleiner test";
		while(line != "")
		{
			line = reader.ReadLine();
		}

		//hitobjects
		line = reader.ReadLine();
		Debug.Log(line);
		while(line != "")
		{
			line = reader.ReadLine();
			//Debug.Log(line);
			if(line != "")
			{
				string[] split = line.Split(',');
				//Debug.Log(split[2]);
				hitObjectTimings.Add(int.Parse(split[2]));
			}
				
		}
	}

	void CreateNewTrackAsset(string trackName, float trackLenght, int sampleLength)
	{
		string path = "Assets/CustomTracks/" + trackName + ".txt";
		StreamWriter writer = new StreamWriter(path, false);

		string line = "%YAML 1.1";
		writer.WriteLine(line);
		line = "%TAG !u! tag:unity3d.com,2011:";
		writer.WriteLine(line);
		line = "--- !u!114 &11400000";
		writer.WriteLine(line);
		line = "MonoBehaviour:";
		writer.WriteLine(line);

		line = "  m_ObjectHideFlags: 0";
		writer.WriteLine(line);
		line = "  m_CorrespondingSourceObject: {fileID: 0}";
		writer.WriteLine(line);
		line = "  m_PrefabInternal: {fileID: 0}";
		writer.WriteLine(line);
		line = "  m_GameObject: {fileID: 0}";
		writer.WriteLine(line);
		line = "  m_Enabled: 1";
		writer.WriteLine(line);
		line = "  m_EditorHideFlags: 0";
		writer.WriteLine(line);
		line = "  m_Script: {fileID: -1223223985, guid: aa81bbec85a3f423193272a7c6154986, type: 3}";
		writer.WriteLine(line);
		line = "  m_Name: " + trackName;
		writer.WriteLine(line);
		line = "  m_EditorClassIdentifier:";
		writer.WriteLine(line);
		line = "  mEventID: " + trackName;
		writer.WriteLine(line);
		
		line = "  mEventList:";
		writer.WriteLine(line);
		foreach(int i in hitObjectTimings)
		{
			float timing = (float)i;
			float sampleTiming = timing / trackLenght;
			int mySample = (int)(sampleTiming * sampleLength);

			line = "  - mStartSample: " + mySample.ToString();
			writer.WriteLine(line);
			line = "    mEndSample: " + mySample.ToString();
			writer.WriteLine(line);		
		}


		line = "  _SerializedPayloadTypes:";
		writer.WriteLine(line);
		line = "  - SonicBloom.Koreo.IntPayload";
		writer.WriteLine(line);
		line = "  _AssetPayloads: []";
		writer.WriteLine(line);
		line = "  _AssetPayloadIdxs:";
		writer.WriteLine(line);
		line = "  _ColorPayloads: []";
		writer.WriteLine(line);
		line = "  _ColorPayloadIdxs: ";
		writer.WriteLine(line);
		line = "  _CurvePayloads: []";
		writer.WriteLine(line);
		line = "  _CurvePayloadIdxs:";
		writer.WriteLine(line);
		line = "  _FloatPayloads: []";
		writer.WriteLine(line);
		line = "  _FloatPayloadIdxs:";
		writer.WriteLine(line);
		line = "  _GradientPayloads: []";
		writer.WriteLine(line);
		line = "  _GradientPayloadIdxs:";
		writer.WriteLine(line);

		line = "  _IntPayloads:";
		writer.WriteLine(line);
		foreach(int i in hitObjectTimings)
		{
			line = "  - mIntVal: 0";
			writer.WriteLine(line);	
		}

		line = "  _IntPayloadIdxs: 00";
		string after = "";
		int counter = 0;
		for(int i = 0; i < hitObjectTimings.Count; i++)
		{
			int plusOne = i + 1;
			string number = plusOne.ToString("X");
			counter = 0;
			while(number.Length < 8)
			{
				number = "0" + number;
				counter++;
			}
			after += number;
		}
		after = after.ToLower();
		for(int i = 0; i < counter; i++)
		{
			after = after + "0";
		}	
		writer.WriteLine(line + after);	
		
		line = "  _SpectrumPayloads: []";
		writer.WriteLine(line);
		line = "  _SpectrumPayloadIdxs:";
		writer.WriteLine(line);
		line = "  _TextPayloads: []";
		writer.WriteLine(line);
		line = "  _TextPayloadIdxs:";
		writer.WriteLine(line);

	

		writer.Close();
	}
}
