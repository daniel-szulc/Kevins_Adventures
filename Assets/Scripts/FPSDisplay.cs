using UnityEngine;
using System.Collections;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
	float deltaTime = 0.0f;
	public static FPSDisplay instance;
	public Texture2D texture;
	void Update()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
	}
 
	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;
 
		GUIStyle style = new GUIStyle();
 
		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h * 3 / 100;
		style.normal.textColor = new Color (0.0f, 0.0f, 0.5f, 1.0f);
		//style.normal.background = texture;
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		GUI.Label(rect, text, style);
	}
	
	
	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{

			if (this != instance)
				Destroy(this.gameObject);
		}
        
	}
	
}