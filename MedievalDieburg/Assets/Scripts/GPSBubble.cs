using UnityEngine;
using System.Collections;



public class GPSBubble 
{

	private string 	m_name;
	private double 	m_longitude;
	private double 	m_latitude;
	private float 	m_altitude;
	private double 	m_radius;

	private bool	m_playerInBubble;
	#region Variablen Getter
	public string Name
	{
		get
		{
			return m_name;
		}
	}

	public double Longitude
	{
		get
		{
			return m_longitude;
		}
	}

	public double Latitude
	{
		get
		{
			return m_latitude;
		}
	}

	public float Altitude
	{
		get
		{
			return m_altitude;
		}
	}
	public double Radius
	{
		get
		{
			return m_radius;
		}
	}

	public GPSBubble(string name, double lat, double lng, float alt, double r)
	{
		m_name 		= 	name;
		m_latitude 	=	lat;
		m_longitude =	lng;
		m_altitude 	=	alt;
		m_radius 	= 	r;
	}
	#endregion 

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
