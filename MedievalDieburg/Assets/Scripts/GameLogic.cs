using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Vuforia;

public class GameLogic : MonoBehaviour
{


	public float PlayerLat;
	public float PlayerLng;

	private GPSBubble[] m_Bubbles;
	private bool m_playerInBubble;
	private string m_activeBubble;
	Text InfoGPSBubble;
	Text InfoLat;
	Text InfoLng;
	Text InfoInBubble;
	Text InfoPosGarten;
	Text InfoPosHof;
	Text InventoryCounter;
	Image InventoryItem0Image;
	Image InventoryItem1Image;
	Text InventoryItem0Text;
	Text InventoryItem1Text;

	public bool PlayerInBubble
	{
		get
		{
			return m_playerInBubble;
		}
	}

	public string ActiveBubble
	{
		get
		{
			return m_activeBubble;
		}
	}

	void Start () 
	{
		registerGPSBubbles ();
		m_playerInBubble = true;
		RegisterUICanvasElements ();
	}

	void Update()
	{
		CheckPlayerPosition ();
		PrintInfo ();
	}

	private void registerGPSBubbles()
	{
		m_Bubbles 		= new GPSBubble[3];
		m_Bubbles [0] 	= new GPSBubble ("TestBubbleUni", 49.903020d, 8.858752d, 0f, 0.00002d);
		m_Bubbles [1] 	= new GPSBubble ("Garten", 49.900476d, 8.845070d, 0f, 0.0002d);
		m_Bubbles [2]	= new GPSBubble ("Hof", 49.900393d, 8.844850d, 0f, 0.00006d); 

		foreach (GPSBubble bubble in m_Bubbles) 
		{
			//each bubble gets the same scene for testing purposes only
			// this will be handled by the scene manager in the future
			bubble.Scene = Resources.Load("Prefabs/MarktplatzSzene") as GameObject;
		}
	}

	private void CheckPlayerPosition()
	{
		PlayerLat = Input.location.lastData.latitude;
		PlayerLng = Input.location.lastData.longitude;

		foreach(GPSBubble bubble in m_Bubbles)
		{
			if((PlayerLat < bubble.Latitude + bubble.Radius && PlayerLat > bubble.Latitude - bubble.Radius) &&
			   (PlayerLng < bubble.Longitude + bubble.Radius && PlayerLng > bubble.Longitude - bubble.Radius))
			{
				m_activeBubble = bubble.Name;
				m_playerInBubble = true;
				break;
			}
			else
			{
				m_playerInBubble = false;
			}
		}
	}

	private void RegisterUICanvasElements()
	{
		InfoGPSBubble 	= GameObject.Find ("InfoGPSBubble").GetComponent<Text> ();
		InfoLat 		= GameObject.Find ("InfoLatitude").GetComponent<Text> ();
		InfoLng 		= GameObject.Find ("InfoLongitude").GetComponent<Text> ();
		InfoInBubble 	= GameObject.Find ("InBubble").GetComponent<Text> ();
		InfoPosGarten 	= GameObject.Find ("PosGarten").GetComponent<Text> ();
		InfoPosHof		= GameObject.Find("PosHof").GetComponent<Text>();
		InventoryCounter= GameObject.Find("InventoryCounter").GetComponent<Text>();
		InventoryItem0Image = GameObject.Find("InventoryItem0").GetComponent<Image>();
		InventoryItem1Image = GameObject.Find("InventoryItem1").GetComponent<Image>();
		InventoryItem0Text = GameObject.Find("InventoryItem0").GetComponentInChildren<Text>();
		InventoryItem1Text = GameObject.Find("InventoryItem1").GetComponentInChildren<Text>();
	}

	private void PrintInfo()
	{
		InfoGPSBubble.text 	  = "Player in GPS Bubble: " + m_activeBubble; 
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


	public void QuitApp()
	{
		Application.Quit ();
	}

	public void AdjustScenePositionToPlayer()
	{
	
	}
}
