using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRInteractiveItem : MonoBehaviour
{
    public enum Interactables
    {
        Teleport,
        Pickup,
        Button,
        Key,
        Furniture,
        DoorLock,
        Deur,
    }

    public Interactables interactables;

    public VREyeRaycaster vrEye;        //Reference to the main cam
    public Inventory inv;               //Reference to the inventory script
    public QuestionManager quest;      //Reference to the question manager

    [Header("Basic stuff")]
    public Renderer renderer;           //The renderer for the colors
    public Color startcolor;            //The color the object has if you start the game
    public Color newColor;              //The selected color
    private Vector3 offset;             //The distance between the player and the center of the object
    private Vector3 startPos;           //The position it is when the game starts (Pickup)
    private Quaternion startRot;        //The rotation it is when the game starts (Pickup)
    public GameObject camHold;          //The gameobject that you use to teleport to to a pad

    [Header("Pickup only")]
    public GameObject doorLock;         //The gameobject that turns green when you collect a pickup
    public MeshRenderer doorRenderer;   //How you make doorLock change color
    public Color green;                 //The color the doorlocks will have once you've picked up a tablet

    [Header("Button only")]
    public bool rightAnswer;            //Shows if it is the right answers

    [Header("DoorLock only")]
    public bool usedAlready;            //Checks if you already unlocked one of the locks

	// Use this for initialization
	void Start()
    { 
        //Get startposition
        offset = new Vector3(0, 1.5f, 0);
        startPos = gameObject.transform.position;
        startRot = gameObject.transform.rotation;

        //Get startcolor
        if (interactables != Interactables.Button)
        {
            renderer = GetComponent<Renderer>();
            renderer.material.color = startcolor;
        }

        //Check if it is a pickup, Yes? Then get the doorRenderer
        if (interactables == Interactables.Pickup)
        {
            doorRenderer = doorLock.GetComponent<MeshRenderer>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //You have the object selected
    public void Selected ()
    {
        if (interactables != Interactables.Button) 
        {
            if (interactables != Interactables.DoorLock)
            {
                renderer.material.color = newColor;
            }
        }
        

        //Check what the function is
        switch (interactables)
        {
            case Interactables.Teleport:
                print("Teleport");
                UpdateTeleport();
                break;
            case Interactables.Pickup:
                print("Pickup");
                UpdatePickupInventory();
                break;
            case Interactables.Button:
                print("Button");
                UpdateButtons();
                break;
            case Interactables.Key:
                print("key");
                UpdateKeys();
                break;
            case Interactables.DoorLock:
                UpdateDoorlocks();
                break;
            case Interactables.Furniture:
                print("Play animation");
                break;
            case Interactables.Deur:
                print("Play ,open deur, animation");
                UpdateDeur();
                break;
            default:
                print("default");
                break;
        }
    }

    //Deselected? Give the object it's startcolor
    public void Deselected()
    {
        renderer.material.color = startcolor;
        if (interactables == Interactables.DoorLock)
        {
            if (usedAlready)
            {
                renderer.material.color = newColor;
            }
        }
    }

    #region Pickup Holding
    void UpdatePickupInventory()
    {
        //Add the item in the Inventory
        if (!inv.items.Contains(gameObject))
        {
            inv.items.Add(this.gameObject);
            inv.ItemCounter();
            doorRenderer.material.color = green;
            startcolor = newColor;

            vrEye.isHolding = true;
            StartCoroutine(HoldTimer());
            vrEye.loadingField.fillAmount = 0;
        }
        //You have allready collected this item
        else if (inv.items.Contains(gameObject))
        {
            print("you already collected this");
        }
    }

    //Move the pickup to the holdposition
    void HoldPickup()
    {
        print("This the pickup function");
        this.gameObject.transform.SetParent(vrEye.holdPosition);
        this.gameObject.transform.SetPositionAndRotation(vrEye.holdPosition.position, vrEye.holdPosition.rotation);
    }

    //Place it back in the scene on the original location
    void PutBackPickup()
    {
        this.gameObject.transform.SetParent(null);
        this.gameObject.transform.position = startPos;
        this.gameObject.transform.rotation = startRot;
        vrEye.isHolding = false;
    }

    //Timer
    IEnumerator HoldTimer()
    {
        print("Started");
        //yield return new WaitForSeconds(2);
        HoldPickup();
        yield return new WaitForSeconds(2);
        PutBackPickup();
        StopCoroutine(HoldTimer());
    }
    #endregion

    #region Teleporting
    //Teleport to the pad with an offset
    void UpdateTeleport()
    {
        camHold.transform.position = transform.position + offset;
        vrEye.loadingField.fillAmount = 0;
    }
    #endregion

    #region Buttons
    //Check if the awnser on the button is the right one if so execute the upper part if not execute the botom part
    void UpdateButtons()
    {
        if (rightAnswer)
        {
            RightAnswer();
        }
        else if (!rightAnswer)
        {
            WrongAnswer();
        }
        vrEye.loadingField.fillAmount = 0;
    }

    public void RightAnswer()
    {
        print("That is the right answer");
        //quest.QuestionChanger();
    }

    public void WrongAnswer()
    {
        print("That is the wrong answer");
    }
    #endregion

    #region Keys
    //Add the key to the inventory, send message if something imposible happens
    void UpdateKeys()
    {
        if (!inv.keys.Contains(gameObject))
        {
            inv.keys.Add(this.gameObject);
            gameObject.SetActive(false);
            inv.ItemCounter();
            inv.keyCount++;
            vrEye.loadingField.fillAmount = 0;
        }
        else if (inv.keys.Contains(gameObject))
        {
            print("how are you doing this you aren't able to see the key");
        }
    }
    #endregion

    #region Doorlocks
    //check if the door is already used if not use the key to unlock, if it is do nothing
    void UpdateDoorlocks()
    {
        if (!usedAlready)
        {
            print("doorlock");
            vrEye.loadingField.fillAmount = 0;
            if (inv.keys.Count > 0)
            {
                usedAlready = true;
                inv.UseKey();
                renderer.material.color = newColor;
            }
        }
        else if (usedAlready)
        {
            renderer.material.color = newColor;
            print("doorlock");
            vrEye.loadingField.fillAmount = 0;
        }
    }
    #endregion

    #region Deur
    void UpdateDeur()
    {
        vrEye.loadingField.fillAmount = 0;
    }
    #endregion
}
