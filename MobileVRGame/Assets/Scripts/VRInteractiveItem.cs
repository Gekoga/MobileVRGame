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
    public Vector3 startPos;

    public GameObject doorLock;
    public MeshRenderer doorRenderer;

    public VREyeRaycaster vrEye; //reference to the main cam
    public Inventory inv; //reference to the inventory script

    public Interactables interactables;

	// Use this for initialization
	void Start()
    { 
        offset = new Vector3(0, 2, 0);
        startPos = gameObject.transform.position;

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

                    vrEye.isHolding = true;
                    StartCoroutine(HoldTimer());
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
