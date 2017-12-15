﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VREyeRaycaster : MonoBehaviour {

    public LayerMask interactableLayer; //the layer of objects the gaze controller can interact with
    public Image loadingField; //the image of the loading bar/circle 
    [SerializeField] private float loadingSpeed; //how fast the loading bar gets full
    public GameObject viewedItem; //the item you are looking at
    public VRInteractiveItem vrItem; //reference to the VRInteractiveItem script
    [Space][Header("Item holding variables")]
    public GameObject holdGObject;
    public Transform holdPosition; //The position where the pickup will be held at
    public bool isHolding = false;

	// Use this for initialization
	void Start ()
    {
        holdPosition = holdGObject.transform;
	}

    // Update is called once per frame
    void Update()
    {
        RaycastHit Hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (isHolding == false)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out Hit, Mathf.Infinity, interactableLayer))
            {
                loadingField.fillAmount += loadingSpeed;
                if (loadingField.fillAmount == 1)
                {
                    viewedItem = Hit.transform.gameObject;
                    vrItem = viewedItem.GetComponent<VRInteractiveItem>();
                    vrItem.Selected();
                }
            }
            else if (!Physics.Raycast(ray.origin, ray.direction, out Hit, Mathf.Infinity, interactableLayer))
            {
                if (viewedItem != null)
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
