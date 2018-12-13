using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ButtonUIManager : MonoBehaviour {

	public float UIScale;
	public Vector2[] directions;
	// Use this for initialization
	public RectTransform[] buttonTransforms;
	void Start () {

	}

	/// <summary>
	/// Called when the script is loaded or a value is changed in the
	/// inspector (Called in the editor only).
	/// </summary>
	void OnValidate()
	{
		for(int i = 0; i < 6; i++)
		{
			float aspect = Screen.height / Screen.width;
			Vector2 dir = new Vector2(directions[i].normalized.x, directions[i].normalized.y);
			Vector2 newPos = new Vector2(0.5f, 0.5f) + dir * UIScale;
			buttonTransforms[i].anchorMin = newPos;
			buttonTransforms[i].anchorMax = newPos;
			buttonTransforms[i].anchoredPosition = Vector2.zero;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
