using UnityEngine;
using System.Collections;
using System;

public class Tile : MonoBehaviour {

    Vector3 gridPosition;
	void Start () {
	
	}

    void OnMouseEnter() {
        transform.GetComponent<Renderer>().material.color = Color.blue;   
    }

    void OnMouseExit() {
        transform.GetComponent<Renderer>().material.color = Color.white;
    }

    void OnMouseDown() {
        Debug.Log("Clicked: " + gridPosition);
    }

    internal void SetGridPosition(Vector3 vector) {
        gridPosition = vector;
    }
}
