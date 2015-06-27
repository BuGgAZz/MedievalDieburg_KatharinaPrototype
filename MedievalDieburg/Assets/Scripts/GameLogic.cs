using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Vuforia;

public class GameLogic : MonoBehaviour
{
	public float PlayerLat;
	public float PlayerLng;

	private GPSBubble[] m_Bubbles;
	private bool m_playerInBubble;
	private GPSBubble m_activeBubble;

	Text InfoGPSBubble;
	Text InfoLat;
	Text InfoLng;
	Text InfoInBubble;
	Text InfoPosGarten;
	Text InfoPosHof;
	Text InventoryCounter;
	UnityEngine.UI.Image InventoryItem0Image;
	UnityEngine.UI.Image InventoryItem1Image;
	Text InventoryItem0Text;
	Text InventoryItem1Text;

	public bool PlayerInBubble
	{
		get
		{
			return m_playerInBubble;
		}
	}

	public GPSBubble ActiveBubble
	{
		get
		{
			return m_activeBubble;
		}
	}

	void Start () 
	{
		registerGPSBubbles ();
		//m_playerInBubble = true;
		RegisterUICanvasElements ();
	}

	void Update()
	{
		CheckPlayerPosition ();
		PrintInfo ();

	}

	// All Existing GPS bubbles for this apk will be instanciated here
	private void registerGPSBubbles()
	{
		m_Bubbles 		= new GPSBubble[3];
		m_Bubbles [0] 	= new GPSBubble ("TestBubbleUni", 49.903020d, 8.858752d, 0f, 0.00002d);
		m_Bubbles [1] 	= new GPSBubble ("Garten", 49.900476d, 8.845070d, 0f, 0.00005d);
		m_Bubbles [2]	= new GPSBubble ("Hof", 49.900393d, 8.844850d, 0f, 0.00006d); 

		foreach (GPSBubble bubble in m_Bubbles) 
		{
			//each bubble gets the same scene for testing purposes only
			// this will be handled by the scene manager in the future
			bubble.Scene = Resources.Load("Prefabs/MarktplatzSzene") as GameObject;
		}
	}

	// Tracks the Player's GPS coordinates and Tests if the Player enters a bubble.
	// If so, the active bubble will start projecting its AR content and adjust 
	// it's contents position relative to the tracked player position
	private void CheckPlayerPosition()
	{
		PlayerLat = Input.location.lastData.latitude;
		PlayerLng = Input.location.lastData.longitude;

		foreach(GPSBubble bubble in m_Bubbles)
		{
			if((PlayerLat < bubble.Latitude + bubble.Radius && PlayerLat > bubble.Latitude - bubble.Radius) &&
			   (PlayerLng < bubble.Longitude + bubble.Radius && PlayerLng > bubble.Longitude - bubble.Radius))
			{
				m_activeBubble = bubble;
				m_playerInBubble = true;
				break;
			}
			else
			{
				m_playerInBubble = false;
			}
		}

		if (m_playerInBubble) 
		{
			AdjustScenePositionToPlayer(Input.location.lastData.longitude,Input.location.lastData.latitude, ActiveBubble);
			if(!ActiveBubble.IsActive)
			{
				ActiveBubble.IsActive = true;
				EventManager.OnPlayerEnter(null, EventArgs.Empty);
			}
		} 
		else if (ActiveBubble.IsActive)
		{
			EventManager.OnPlayerExit(null, EventArgs.Empty);
			ActiveBubble.IsActive = false;
		}
	}

	// subscribes UI elements to its variables
	private void RegisterUICanvasElements()
	{
		InfoGPSBubble 	= GameObject.Find ("InfoGPSBubble").GetComponent<Text> ();
		InfoLat 		= GameObject.Find ("InfoLatitude").GetComponent<Text> ();
		InfoLng 		= GameObject.Find ("InfoLongitude").GetComponent<Text> ();
		InfoInBubble 	= GameObject.Find ("InBubble").GetComponent<Text> ();
		InfoPosGarten 	= GameObject.Find ("PosGarten").GetComponent<Text> ();
		InfoPosHof		= GameObject.Find("PosHof").GetComponent<Text>();
		InventoryCounter= GameObject.Find("InventoryCounter").GetComponent<Text>();
		InventoryItem0Image = GameObject.Find("InventoryItem0").GetComponent<UnityEngine.UI.Image>();
		InventoryItem1Image = GameObject.Find("InventoryItem1").GetComponent<UnityEngine.UI.Image>();
		InventoryItem0Text = GameObject.Find("InventoryItem0").GetComponentInChildren<Text>();
		InventoryItem1Text = GameObject.Find("InventoryItem1").GetComponentInChildren<Text>();
	}


	// Prints the available Infromation to its UI elemets
	private void PrintInfo()
	{
		if (m_activeBubble!= null)
			InfoGPSBubble.text 	  = "Player in GPS Bubble: " + m_activeBubble.Name; 
		else
			InfoGPSBubble.text 	  = "Player not in GPS Bubble" ;

		InfoLat.text 		  = "Player Lat = " + PlayerLat;
		InfoLng.text		  = "Player Lng = " + PlayerLng;
		InfoInBubble.text 	  = "Player in Bubble = " + m_playerInBubble;
		InfoPosGarten.text 	  = "Garten:" + m_Bubbles [1].Latitude + " \n\t\t\t" + m_Bubbles [1].Longitude;
		InfoPosHof.text 	  = "Hof:\t" + m_Bubbles [2].Latitude + " \n\t\t" + m_Bubbles [2].Longitude;
		InventoryCounter.text = "Free inventory space"+Inventory.freeSpace.ToString();
		
		if(Inventory.Items.Count > 0)
		{
			for(int i = 0; i < Inventory.Items.Count; i++ )
			{
				if(i == 0)
				{
					InventoryItem0Image.overrideSprite =(Sprite)Inventory.Items[0].item_icon;
					InventoryItem0Text.text = Inventory.Items[0].item_name;
				}
				if(i == 1)
				{
					InventoryItem1Image.overrideSprite = (Sprite)Inventory.Items[1].item_icon;
					InventoryItem1Text.text = Inventory.Items[1].item_name;
				}
			}
		}
	}

	// locates player in real space via GPS and translates its movements into Unity coordinates, then
	// adjusts scene position according to the player's movement to simulate a static position of the 
	// projected scene as the scene would normally move according to the device the device.	
	public void AdjustScenePositionToPlayer(float playerGPSPosX, float playerGPSPosZ, GPSBubble activeBubble)
	{
		double 	scenePosXMin = activeBubble.Longitude - activeBubble.Radius;
		double 	scenePosXMax = activeBubble.Longitude - activeBubble.Radius;
		double 	scenePosZMin = activeBubble.Latitude - activeBubble.Radius;
		double 	scenePosZMax = activeBubble.Latitude - activeBubble.Radius;
		
		float 	currentPlayerPosX;
		float 	currentPlayerPosZ;
		float 	newPlayerPosX;
		float 	newPlayerPosZ;
		float 	lastPlayerPosX = 0;
		float 	lastPlayerPosZ  = 0;
		float 	deltaPlayerPosX;
		float 	deltaPlayerPosZ;

		Debug.Log ( "LastPosX= " +lastPlayerPosX + " LastPosZ = " +lastPlayerPosZ);
		//Translate GPS player coordinates into Unity Coordinates
		currentPlayerPosX = MathF.Mapf(playerGPSPosX,(float)scenePosXMin,(float)scenePosXMax,-9,9) ;
		currentPlayerPosZ = MathF.Mapf(playerGPSPosZ,(float)scenePosZMin,(float)scenePosZMax,-9,9) ;

		//get travled distance on X and Z
		if (lastPlayerPosX != 0 && lastPlayerPosZ != 0) 
		{
			deltaPlayerPosX = currentPlayerPosX - lastPlayerPosX;
			deltaPlayerPosZ = currentPlayerPosZ - lastPlayerPosZ;
		}
		else 
		{
			lastPlayerPosX = currentPlayerPosX;
			lastPlayerPosZ = currentPlayerPosZ;

			deltaPlayerPosX = currentPlayerPosX - lastPlayerPosX;
			deltaPlayerPosZ = currentPlayerPosZ - lastPlayerPosZ;
		}
				
		lastPlayerPosX = currentPlayerPosX;
		lastPlayerPosZ = currentPlayerPosZ;

		// Move scene in the opposite direction of player to appear still in real space
		newPlayerPosX = activeBubble.Scene.transform.position.x - deltaPlayerPosX;
		newPlayerPosZ = activeBubble.Scene.transform.position.z - deltaPlayerPosZ;

		activeBubble.Scene.transform.position = new Vector3 (newPlayerPosX, activeBubble.Scene.transform.position.y, newPlayerPosZ);
	}

	public void QuitApp()
	{
		Application.Quit ();
	}
}
