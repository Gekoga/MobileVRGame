using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public List<GameObject> items = new List<GameObject>(); //List for all the pickup items
    public List<GameObject> keys = new List<GameObject>();  //List for all the keys

    public GameObject door;

    [Header("animations")]
    public Animator doorOneLeft;
    public Animator doorOneRight;

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
            doorOneLeft.SetTrigger("OpenDoor");
            doorOneRight.SetTrigger("OpenDoor");
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

    public void UseKey()
    {
        if (keys.Count > 0)
        {
            keys.IndexOf(keys[0]);
            print("It works ");
            keys[0].SetActive(true);
            keys.Remove(keys[0]);
        }
        else if (keys.Count <= 0)
        {
            return;
        }
 
    }


}
