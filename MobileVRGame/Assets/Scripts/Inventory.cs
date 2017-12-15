using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public List<GameObject> items = new List<GameObject>();
    public List<GameObject> keys = new List<GameObject>();

    bool alreadyOpen = false;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (items.Count == 4 && !alreadyOpen)
        {
            print("Open the door");
            alreadyOpen = true;
            return;
        }
	}


}
