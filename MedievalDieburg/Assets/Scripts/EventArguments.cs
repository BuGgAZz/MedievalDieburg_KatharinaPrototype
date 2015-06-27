using UnityEngine;
using System;
using System.Collections;

public class PlayerEnterEventArgs : EventArgs
{
	GPSBubble enteredBubble;

}

public class PlayerExitEventArgs : EventArgs
{
	GPSBubble exitedBubble;
}
