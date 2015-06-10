using UnityEngine;
using System.Collections;



public class GPSBubble 
{
	protected string Name;
	protected float Longitude;
	protected float Latitude;
	protected float Altitude;
	protected float Radius;

	private bool m_PlayerInBubble;
	
	public GPSBubble(string name, float lng, float lat, float alt, float r)
	{
		Name 		= 	name;
		Longitude 	=	lng;
		Latitude 	=	lat;
		Altitude 	=	alt;
		Radius 		= 	r;
	}

	void OnEnable()
	{
		EventManager.OnPlayerEnter 	+= 	ProjectScene;
		EventManager.OnPlayerExit 	+=	StopProjectScene;
	}

	void OnDisable()
	{
		EventManager.OnPlayerEnter 	-= 	ProjectScene;
		EventManager.OnPlayerExit 	-=	StopProjectScene;
	}

	void CkeckForPlayerCoordinates()
	{
		//Envoke OnPlayerEnter & OnPlayerExit here

	}

	void ProjectScene()
	{

	}

	void StopProjectScene()
	{

	}
}
