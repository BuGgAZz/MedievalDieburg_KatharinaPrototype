﻿using UnityEngine;
using System;
using System.Collections;



public class GPSBubble 
{

	public GameObject Scene;

	private string 	m_name;
	private double 	m_longitude;
	private double 	m_latitude;
	private float 	m_altitude;
	private double 	m_radius;

	private bool	m_isActive;

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

	public bool IsActive
	{
		get
		{
			return m_isActive;
		}
		set
		{
			m_isActive = IsActive;
		}
	}
	#endregion 

	public GPSBubble(string name, double lat, double lng, float alt, double r)
	{
		m_name 		= 	name;
		m_latitude 	=	lat;
		m_longitude =	lng;
		m_altitude 	=	alt;
		m_radius 	= 	r;
	}


	private void OnEnable()
	{
		EventManager.PlayerEnter += ProjectScene;
		EventManager.PlayerExit += StopProjectScene;
	}

	private void OnDisable()
	{
		EventManager.PlayerEnter -= ProjectScene;
		EventManager.PlayerExit -= StopProjectScene;
	}
	
	private void DetermineProjectedScene(string activeBubble)
	{
		switch (activeBubble) 
		{
		case "Garten"	: /* GartenSceneLaden*/ ; break;
		case "Hof" 		: /* HofSceneLaden*/	; break; 
		}
	}

	public void ProjectScene(object sender, EventArgs e)
	{
		if (m_isActive) 
		{
			Scene.GetComponent<Renderer> ().enabled = true;
		}
	}

	public void StopProjectScene(object sender, EventArgs e)
	{	
		if (m_isActive) 
		{
			Scene.GetComponent<Renderer> ().enabled = false;
		}
	}
}
