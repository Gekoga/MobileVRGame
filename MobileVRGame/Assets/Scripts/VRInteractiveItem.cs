using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRInteractiveItem : MonoBehaviour {

    public Renderer renderer;
    public Color startcolor;
    public Color newColor;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
        renderer.material.color = startcolor;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Selected ()
    {
        renderer.material.color = newColor;
    }

    public void Deselected()
    {
        renderer.material.color = startcolor;
    }
}
