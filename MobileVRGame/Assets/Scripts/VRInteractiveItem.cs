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

    //public GameObject ;
    public Vector3 startPos;

    // Use this for initialization
    void Start()
    {
        startPos = gameObject.transform.position;

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
                vrEye.isHolding = true;
                StartCoroutine(HoldTimer());
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


#region Pickup Holding
    void HoldPickup()
    {
        print("This the pickup function");
        this.gameObject.transform.SetParent(vrEye.holdPosition);
        this.gameObject.transform.SetPositionAndRotation(vrEye.holdPosition.position, vrEye.holdPosition.rotation);
    }
    
    void PutBackPickup()
    {
        this.gameObject.transform.SetParent(null);
        this.gameObject.transform.position = startPos;
        vrEye.isHolding = false;
    }

    IEnumerator HoldTimer()
    {
        print("Started");
        yield return new WaitForSeconds(2);
        HoldPickup();
        yield return new WaitForSeconds(4);
        PutBackPickup();
        StopCoroutine(HoldTimer());
    }
#endregion
}
