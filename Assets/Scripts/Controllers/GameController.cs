using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour {

    public delegate void DefiningUserInteraction(bool interactive);
    public static event DefiningUserInteraction OnDefiningUserInteraction;

    [SerializeField]
    Vector3 cameraPosition;
    [SerializeField]
    Vector3 cameraRotation;

    Camera mainCamera;

    public static GameController instance;
    
    public static GameController Instance {
        get { return instance; }
    }

    private Character savedCharacter;

    public Character SavedCharacter {
        get {
            return savedCharacter;
        }
        set {
            savedCharacter= value;
        }
    }

    internal void UserInteraction(bool interactive) {
        if (OnDefiningUserInteraction != null) OnDefiningUserInteraction(interactive);
    }

    void Awake() {
        instance = this;
        SetCameraPosition();
    }

    void SetCameraPosition() {
        mainCamera = Camera.main;

        mainCamera.transform.position = cameraPosition;    
        mainCamera.transform.rotation = Quaternion.Euler(cameraRotation);
    }

}
