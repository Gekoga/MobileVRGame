using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VREyeRaycaster : MonoBehaviour {

    public LayerMask interactableLayer;          //The layer of objects the gaze controller can interact with
    public Image loadingField;                   //The image of the loading bar/circle 
    [SerializeField] private float loadingSpeed; //How fast the loading bar gets full
    public GameObject viewedItem;                //The item you are looking at
    public VRInteractiveItem vrItem;             //Reference to the VRInteractiveItem script
    public float lookDistance;                   //How far the player can look

    [Space]
    public List<VRInteractiveItem> teleportPads = new List<VRInteractiveItem>(); //All the teleportpads

    [Space][Header("Item holding variables")]
    public GameObject holdGObject;               //The object to get the holdposition
    public Transform holdPosition;               //The position where the pickup will be held at
    public bool isHolding = false;               //Check if you have something in your hands

    // Use this for initialization
    void Start()
    {
        //Get the position of the holding point
        holdPosition = holdGObject.transform;

        //Make all the teleportpads out of range disapear
        foreach (VRInteractiveItem pads in teleportPads)
        {
            float distance = Vector3.Distance(this.transform.position, pads.transform.position);
            if (distance > lookDistance)
            {
                pads.gameObject.SetActive(false);
            }
            if (distance <= lookDistance)
            {
                pads.gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the player is looking at something
        RaycastHit Hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (isHolding == false)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out Hit, lookDistance, interactableLayer))
            {
                loadingField.fillAmount += loadingSpeed;
                if (loadingField.fillAmount == 1)
                {
                    viewedItem = Hit.transform.gameObject;
                    vrItem = viewedItem.GetComponent<VRInteractiveItem>();
                    vrItem.Selected();
                    foreach (VRInteractiveItem pads in teleportPads)
                    {
                        float distance = Vector3.Distance(this.transform.position, pads.transform.position);
                        if (distance > lookDistance)
                        {
                            pads.gameObject.SetActive(false);
                        }
                        if (distance <= lookDistance)
                        {
                            pads.gameObject.SetActive(true);
                        }
                    }
                }
            }
            else if (!Physics.Raycast(ray.origin, ray.direction, out Hit, lookDistance, interactableLayer))
            {
                if (viewedItem != null && vrItem.interactables != VRInteractiveItem.Interactables.Button)
                {
                    vrItem.Deselected();
                }
                loadingField.fillAmount = 0;
                vrItem = null;
                viewedItem = null;
            }
        }
    }
}
