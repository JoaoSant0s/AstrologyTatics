using UnityEngine;
using System.Collections;
using Editor.Layout;
using System;

public class Character : MonoBehaviour {

    private ArrayLayout paths;

    public ArrayLayout Paths {
        get {
            return paths;
        }
    }

    void OnMouseDown() {
        Debug.Log("Clicked: " + transform.position);
    }

    internal void SetPaths(ArrayLayout layoutPaths) {
        for (int i = 0; i < layoutPaths.rows.Length; i++) {
            var row = layoutPaths.rows[i].row;
            Debug.Log(" * \n" );
            for (int j = 0; j < row.Length; j++) {
                Debug.Log(" " + row[j] + " ");
            }
        }
        paths = layoutPaths;
    }
}
