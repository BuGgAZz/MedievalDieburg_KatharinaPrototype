using UnityEngine;
using System;
using System.Collections;

public static class EventManager 
{
	public static event EventHandler PlayerEnter;
	public static event EventHandler PlayerExit;



	public static void OnPlayerEnter(object sender, EventArgs e)
	{
		EventHandler evt = PlayerEnter;

		if (evt != null)
		{
			evt(sender,e);
		}
	}

	public static void OnPlayerExit(object sender, EventArgs e)
	{
		EventHandler evt = PlayerExit;

		if (evt != null) 
		{
			evt(sender,e);
		}
	}

	/*public delegate void PlayerEnter();
	public static event PlayerEnter OnPlayerEnter;
	
	public delegate void PlayerExit();
	public static event PlayerExit OnPlayerExit;*/


}
