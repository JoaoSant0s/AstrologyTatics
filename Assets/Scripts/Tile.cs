using UnityEngine;
using System.Collections;
using System;

public class Tile : MonoBehaviour {

    public delegate void ClearTiles();
    public static event ClearTiles OnClearTiles;

    Vector3 gridPosition;
    Material materialTile;
    bool activeArea;

    public Vector3 GridPosition {
        get {
            return gridPosition;
        }
    }

    public bool ActiveArea { get { return activeArea; } }

    void Awake() {
        materialTile = transform.GetComponent<Renderer>().material;
        activeArea = false;
    }

    internal void MoveCharacter() {
        if (!activeArea) return;
        var savedCharacter = GameController.Instance.SavedCharacter;
        GameController.Instance.SavedCharacter = null;

        if (savedCharacter != null) {
            if (OnClearTiles != null)
                OnClearTiles();
            //savedCharacter.transform.position = Vector3.Lerp(savedCharacter.transform.position, transform.position, 20 * Time.deltaTime);
            savedCharacter.transform.position = transform.position;
            savedCharacter.PostionCharacter = transform.position;
            savedCharacter.ActivePaths = false;
        }
    }

    void OnMouseDown() {        
        MoveCharacter();
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
