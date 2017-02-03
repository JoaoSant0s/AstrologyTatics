using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour {

    public delegate void DefiningUserInteraction(bool interactive);
    public static event DefiningUserInteraction OnDefiningUserInteraction;

    public delegate void PauseAction();
    public static event PauseAction OnPauseAction;

    public enum MachineState {
        paused,
        resume
    }

    [SerializeField]
    Vector3 cameraPosition;
    [SerializeField]
    Vector3 cameraRotation;

    Camera mainCamera;    
    MachineState gameState;

    public void SetResumeGameState() {
        gameState = MachineState.resume;
    }

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
        gameState = MachineState.resume;    
        instance = this;
        SetCameraPosition();
    }


    void Update() {
        if(Input.GetKeyDown(KeyCode.P)){
            if (gameState == MachineState.paused) return;
            gameState = MachineState.paused;
            if (OnPauseAction != null) OnPauseAction();
        }
    }

    void SetCameraPosition() {
        mainCamera = Camera.main;

        mainCamera.transform.position = cameraPosition;    
        mainCamera.transform.rotation = Quaternion.Euler(cameraRotation);
    }

}
