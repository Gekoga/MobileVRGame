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
    public VREyeRaycaster vrEye; //reference to the main cam
    public Vector3 offset; //the distance between the player and the center of the object

    public Interactables interactables;

	// Use this for initialization
	void Start()
    { 
        offset = new Vector3(0, 2, 0);
        renderer = GetComponent<Renderer>();
        renderer.material.color = startcolor;
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
                StartCoroutine(LookAtDelay());
                vrEye.transform.position = transform.position + offset;
                break;
            case Interactables.Pickup:
                print("Pickup");
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

    IEnumerator LookAtDelay()
    {
        yield return new WaitForSeconds(1);
        StopCoroutine(LookAtDelay());
    }
}
