using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropScript : MonoBehaviour {

    public Inventory inventory;

    public GameObject keySpace;
    public bool hasKey;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.O))
        {
            CollectKey();
        }
	}

    public void CollectKey()
    {
        if (inventory.keys.Count >= 1)
        {
            keySpace = inventory.gameObject;
        }

        else if (inventory.keys.Count <= 0)
        {
            return;
        }
    }

}
