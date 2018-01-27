using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public List<GameObject> items = new List<GameObject>(); //List for all the pickup items
    public List<GameObject> keys = new List<GameObject>();  //List for all the keys

    public int keyCount = 0;                                //Check how many keys are collected in the game
    public int keysUsed = 0;                                //Check how many keys are used to unlock the last door

    [Header("Animations")]
    public Animator firstMainDoors;                         //The animation of the door to the second room
    public Animator secondMainDoors;                        //The animation of the door to the shutel

    bool firstOpen = false;                                 //Check if the first door is already open
    bool secondOpen = false;                                //Chekc if the second door is already open

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
        if (items.Count == 3 && !firstOpen)
        {
            print("Open the door");
            firstOpen = true;
            firstMainDoors.SetTrigger("OpenDoor");
            return;
        }
    }

    //Check if there are more then 0 keys in the inventory, and use the key
    public void UseKey()
    {
        if (keys.Count > 0)
        {
            keys.IndexOf(keys[0]);
            print("It works ");
            keysUsed++;
            keys[0].SetActive(true);
            keys.Remove(keys[0]);
            if (keysUsed == 3 && !secondOpen)
            {
                print("the locked door is unlocked");
                secondOpen = true;
                secondMainDoors.SetTrigger("OpenDoor");
                return;
            }
        }
        else if (keys.Count <= 0)
        {
            return;
        }
 
    }


}
