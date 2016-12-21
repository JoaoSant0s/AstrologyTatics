using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    void OnMouseDown() {
        Debug.Log("Clicked: " + transform.position);
    }

}
