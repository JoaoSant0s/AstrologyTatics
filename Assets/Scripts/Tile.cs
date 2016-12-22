using UnityEngine;
using System.Collections;
using System;

public class Tile : MonoBehaviour {

    public delegate void ClearTiles();
    public static event ClearTiles OnClearTiles;

    Vector3 gridPosition;
    Material materialTile;
    bool activeArea;

    Character savedCharacter;

    public Vector3 GridPosition {
        get {
            return gridPosition;
        }
    }

    void Awake() {
        Character.OnSaveCharacter += MoveCharacter;

        materialTile = transform.GetComponent<Renderer>().material;
        activeArea = false;
    }

    void OnDestroy() {
        Character.OnSaveCharacter -= MoveCharacter;
    }

    void MoveCharacter(Character character) {
        savedCharacter = character;
    }

    void OnMouseDown() {
        Debug.Log("Clicked: " + gridPosition);
        if (!activeArea) return;

        if (savedCharacter != null) {
            if (OnClearTiles != null) OnClearTiles();
            savedCharacter.transform.position = transform.position;
            savedCharacter.PostionCharacter = transform.position;
        }
    }

    void OnMouseEnter() {
        materialTile.color = Color.blue;
    }

    void OnMouseExit() {
        if (activeArea) {
            ActiveAvaliableArea();
        } else {
            DefaultMaterial();
        }
        
    }

    void DefaultMaterial() {
        materialTile.color = Color.white;
    }

    internal void ActiveAvaliableArea() {
        materialTile.color = Color.gray;
        activeArea = true;
    }

    internal void DesactiveAvaliableArea() {
        DefaultMaterial();
        activeArea = false;
    }

    internal void SetGridPosition(Vector3 vector) {
        gridPosition = vector;
    }

}
