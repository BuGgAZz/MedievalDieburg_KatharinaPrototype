using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour 
{

	private GPSBubble[] Bubbles;

	void Start () 
	{
	
	}

	void Update()
	{

	}

	void registerGPSBubbles()
	{
		Bubbles = new GPSBubble[0];
		Bubbles[0] = new GPSBubble ("TestBubbleUni", 49.903020f, 8.858752f, 0f, 5f);
	}
}
