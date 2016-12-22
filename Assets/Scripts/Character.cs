using UnityEngine;
using System.Collections.Generic;
using Editor.Layout;
using Common.Layout;
using System;

public class Character : MonoBehaviour {

    public delegate void DefiningPaths(Vector3 postionTile, List<Vector3> paths, bool activePaths);
    public static event DefiningPaths OnDefiningPaths;

    public delegate void SaveCharacter(Character character);
    public static event SaveCharacter OnSaveCharacter;

    private List<Vector3> paths;
    private Vector3 postionCharacter;
    bool activePaths;

    void Awake() {
        activePaths = false;
    }

    public List<Vector3> Paths {
        get {
            return paths;
        }
    }

    void OnMouseDown() {
        Debug.Log("Clicked: " + postionCharacter);
        activePaths = !activePaths;
        if (OnDefiningPaths != null) OnDefiningPaths(postionCharacter, paths, activePaths);

        if (OnSaveCharacter != null) OnSaveCharacter((activePaths ? this : null));
    }

    internal void SetPaths(ArrayLayout layoutPaths) {
        paths = LayoutDefinition.ListPaths(layoutPaths);
    }

    internal Vector3 PostionCharacter {
        get {
            return postionCharacter;
        } set {
            postionCharacter = value;
        }
    }

}
