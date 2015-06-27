using UnityEngine;
using System.Collections;

public class CameraRay : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Input.GetMouseButtonDown (0))
		{
			if (Physics.Raycast(ray ,out hit))
			{
				if(hit.collider.tag == "Item")
				{
					Inventory.freeSpace--;
					Inventory.Items.Add(hit.collider.gameObject.GetComponent<Item>());
					Destroy(hit.collider.gameObject);
				}
			}
		}
		
	}
}
