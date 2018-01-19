using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public List<GameObject> items = new List<GameObject>(); //List for all the pickup items
    public List<GameObject> keys = new List<GameObject>();  //List for all the keys

    public GameObject door;

    bool alreadyOpen = false;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void ItemCounter()
    {
        //Check how many items there are in the Items list
        if (items.Count == 3 && !alreadyOpen)
        {
            print("Open the door");
            alreadyOpen = true;
            return;
        }

        //Check how many items there are in the Keys list
        if (keys.Count == 3)
        {
            door.SetActive(false);
            print("the locked door is unlocked");
            return;
        }
    }


}
