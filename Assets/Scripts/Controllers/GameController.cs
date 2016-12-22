using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    [SerializeField]
    Vector3 cameraPosition;
    [SerializeField]
    Vector3 cameraRotation;

    [SerializeField]
    GameObject charactersSet;

    [SerializeField]
    LevelsModule levelsModule;

    Camera mainCamera; 
    
    void Awake() {
        SetCameraPosition();
        TilesController.OnDefiningCharacters += DefineLevel;
    }

    void OnDestroy() {
        TilesController.OnDefiningCharacters -= DefineLevel;
    }

    void DefineLevel() {
        levelsModule.DefineLevel(charactersSet);
    }

    void SetCameraPosition() {
        mainCamera = Camera.main;

        mainCamera.transform.position = cameraPosition;    
        mainCamera.transform.rotation = Quaternion.Euler(cameraRotation);
    }

}
