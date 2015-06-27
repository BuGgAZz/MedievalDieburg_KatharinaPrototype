using UnityEngine;
using System.Collections;

public class TestLocationService : MonoBehaviour
{
	public static string Location;

	IEnumerator Start()
	{
		// First, check if user has location service enabled
		/*if (!Input.location.isEnabledByUser) 
		{
			print("Schluss AUS! KEIN GPS BIATSCH!");
			yield break;
		}*/
		
		// Start service before querying location
		Input.location.Start (5,5);
		
		// Wait until service initializes
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
			print(maxWait.ToString());
		}
		
		// Service didn't initialize in 20 seconds
		if (maxWait < 1)
		{
			print("Timed out");
			yield break;
		}
		
		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			print("Unable to determine device location");
			yield break;
		}
		else
		{
			// Access granted and location value could be retrieved
			print("Location: lat:" + Input.location.lastData.latitude 
			      			+ " lng:" + Input.location.lastData.longitude 
			      			+ " alt:" + Input.location.lastData.altitude 
			      			+ " horAcc:" + Input.location.lastData.horizontalAccuracy 
			      			+ " timestamp:" + Input.location.lastData.timestamp);
		}
		
		// Stop service if there is no need to query location updates continuously
		//Input.location.Stop();

	}

	void Update()
	{
		Location = "Location: lat:" + Input.location.lastData.latitude 
							+ " lng:" + Input.location.lastData.longitude 
							+ " alt:" + Input.location.lastData.altitude 
							+ " horAcc:" + Input.location.lastData.horizontalAccuracy 
							+ " timestamp:" + Input.location.lastData.timestamp;

	}
	
}
