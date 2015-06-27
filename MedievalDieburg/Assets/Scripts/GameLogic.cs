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
	private bool m_InvIsOnScreen = false;
	
	Text InfoGPSBubble;
	Text InfoLat;
	Text InfoLng;
	Text InfoInBubble;
	Text InfoPosGarten;
	Text InfoPosHof;
	Text InventoryCounter;
	
	UnityEngine.UI.Image InventoryItem0Image;
	UnityEngine.UI.Image InventoryItem1Image;
	UnityEngine.UI.Image InventoryItem2Image;
	UnityEngine.UI.Image InventoryItem3Image;
	UnityEngine.UI.Image InventoryItem4Image;
	UnityEngine.UI.Image InventoryItem5Image;
	UnityEngine.UI.Image InventoryItem6Image;
	UnityEngine.UI.Image InventoryItem7Image;
	UnityEngine.UI.Image InventoryItem8Image;

	Text InventoryItem0Text;
	Text InventoryItem1Text;
	Text InventoryItem2Text;
	Text InventoryItem3Text;
	Text InventoryItem4Text;
	Text InventoryItem5Text;
	Text InventoryItem6Text;
	Text InventoryItem7Text;
	Text InventoryItem8Text;
	
	Animation InventoryToggleAni;

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
		
		InventoryCounter    = GameObject.Find("InventoryCounter").GetComponent<Text>();
		InventoryItem0Image = GameObject.Find("InventoryItem0").GetComponent<UnityEngine.UI.Image>();
		InventoryItem1Image = GameObject.Find("InventoryItem1").GetComponent<UnityEngine.UI.Image>();
		InventoryItem2Image = GameObject.Find("InventoryItem2").GetComponent<UnityEngine.UI.Image>();
		InventoryItem3Image = GameObject.Find("InventoryItem3").GetComponent<UnityEngine.UI.Image>();
		InventoryItem4Image = GameObject.Find("InventoryItem4").GetComponent<UnityEngine.UI.Image>();
		InventoryItem5Image = GameObject.Find("InventoryItem5").GetComponent<UnityEngine.UI.Image>();
		InventoryItem6Image = GameObject.Find("InventoryItem6").GetComponent<UnityEngine.UI.Image>();
		InventoryItem7Image = GameObject.Find("InventoryItem7").GetComponent<UnityEngine.UI.Image>();
		InventoryItem8Image = GameObject.Find("InventoryItem8").GetComponent<UnityEngine.UI.Image>();
		
		InventoryItem0Text = GameObject.Find("InventoryItem0").GetComponentInChildren<Text>();
		InventoryItem1Text = GameObject.Find("InventoryItem1").GetComponentInChildren<Text>();
		InventoryItem2Text = GameObject.Find("InventoryItem2").GetComponentInChildren<Text>();
		InventoryItem3Text = GameObject.Find("InventoryItem3").GetComponentInChildren<Text>();
		InventoryItem4Text = GameObject.Find("InventoryItem4").GetComponentInChildren<Text>();
		InventoryItem5Text = GameObject.Find("InventoryItem5").GetComponentInChildren<Text>();
		InventoryItem6Text = GameObject.Find("InventoryItem6").GetComponentInChildren<Text>();
		InventoryItem7Text = GameObject.Find("InventoryItem7").GetComponentInChildren<Text>();
		InventoryItem8Text = GameObject.Find("InventoryItem8").GetComponentInChildren<Text>();
		
		InventoryToggleAni = GameObject.Find("Inventory").GetComponent<Animation>();
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
				switch (i)
				{
					case 0:
					InventoryItem0Image.overrideSprite =(Sprite)Inventory.Items[i].item_icon;
					InventoryItem0Text.text = Inventory.Items[i].item_name;
					break;
					case 1:
					InventoryItem1Image.overrideSprite =(Sprite)Inventory.Items[i].item_icon;
					InventoryItem1Text.text = Inventory.Items[i].item_name;
					break;
					case 2:
					InventoryItem2Image.overrideSprite =(Sprite)Inventory.Items[i].item_icon;
					InventoryItem2Text.text = Inventory.Items[i].item_name;
					break;
					case 3:
					InventoryItem3Image.overrideSprite =(Sprite)Inventory.Items[i].item_icon;
					InventoryItem3Text.text = Inventory.Items[i].item_name;
					break;
					case 4:
					InventoryItem4Image.overrideSprite =(Sprite)Inventory.Items[i].item_icon;
					InventoryItem4Text.text = Inventory.Items[i].item_name;
					break;
					case 5:
					InventoryItem5Image.overrideSprite =(Sprite)Inventory.Items[i].item_icon;
					InventoryItem5Text.text = Inventory.Items[i].item_name;
					break;
					case 6:
					InventoryItem6Image.overrideSprite =(Sprite)Inventory.Items[i].item_icon;
					InventoryItem6Text.text = Inventory.Items[i].item_name;
					break;
					case 7:
					InventoryItem7Image.overrideSprite =(Sprite)Inventory.Items[i].item_icon;
					InventoryItem7Text.text = Inventory.Items[i].item_name;
					break;
					case 8:
					InventoryItem8Image.overrideSprite =(Sprite)Inventory.Items[i].item_icon;
					InventoryItem8Text.text = Inventory.Items[i].item_name;
					break;
				}

			}
		}
	}
	public void ToggleInventory()
	{

		if(!m_InvIsOnScreen)
		{
			print ("play ani fwd");
			InventoryToggleAni["InventoryAni"].speed = 1;
			InventoryToggleAni["InventoryAni"].time = 0;
			InventoryToggleAni.Play();
		}
		else
		{
			print ("play ani bwd");
			InventoryToggleAni["InventoryAni"].speed = -1;
			InventoryToggleAni["InventoryAni"].time = InventoryToggleAni.clip.length;
			InventoryToggleAni.Play();
		}
		
		m_InvIsOnScreen = !m_InvIsOnScreen;
	}

	public void QuitApp()
	{
		Application.Quit ();
	}

	public void AdjustScenePositionToPlayer()
	{
	
	}
}
