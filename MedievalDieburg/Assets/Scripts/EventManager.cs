using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour 
{

	public delegate void PlayerEnter();
	public static event PlayerEnter OnPlayerEnter;
	
	public delegate void PlayerExit();
	public static event PlayerExit OnPlayerExit;
}
