using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRInteractiveItem : MonoBehaviour {
    public enum Interactables
    {
        Teleport,
        Pickup,
        Button,
    }

    public Renderer renderer; //the renderer for the colors
    public Color startcolor; //the color the object has if you start the game
    public Color newColor; //the selected color
    public Vector3 offset; //the distance between the player and the center of the object

    public GameObject doorLock;
    public MeshRenderer doorRenderer;

    public VREyeRaycaster vrEye; //reference to the main cam
    public Inventory inv; //reference to the inventory script

    public Interactables interactables;

	// Use this for initialization
	void Start()
    { 
        offset = new Vector3(0, 2, 0);
        renderer = GetComponent<Renderer>();
        renderer.material.color = startcolor;
        if (interactables == Interactables.Pickup)
        {
            doorRenderer = doorLock.GetComponent<MeshRenderer>();
        }
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Selected ()
    {
        renderer.material.color = newColor;

        switch (interactables)
        {
            case Interactables.Teleport:
                print("Teleport");
                vrEye.transform.position = transform.position + offset;
                vrEye.loadingField.fillAmount = 0;
                break;
            case Interactables.Pickup:
                print("Pickup");
                if (!inv.items.Contains(gameObject))
                {
                    inv.items.Add(this.gameObject);
                    doorRenderer.material.color = startcolor;
                    startcolor = newColor;
                }
                else if (inv.items.Contains(gameObject))
                {
                    print("you already collected this");
                }
                break;
            case Interactables.Button:
                print("Button");
                break;
            default:
                print("default");
                break;
        }
    }

    public void Deselected()
    {
        renderer.material.color = startcolor;
    }
}
