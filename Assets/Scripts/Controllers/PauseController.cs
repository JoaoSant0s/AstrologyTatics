using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseController : MonoBehaviour {

    public delegate void ActiveMenu(bool active);
    public static event ActiveMenu OnActiveMenu;

    [SerializeField]
    Button btnResume;
    [SerializeField]
    Button btnMenu;

    [SerializeField]
    GameObject pauseScreen;

    void Awake() {
        GameController.OnPauseAction += activePauseScreen;
    }

    void OnDestroy() {
        GameController.OnPauseAction -= activePauseScreen;
    }

    void Start() {
        btnResume.onClick.AddListener(actionResume);
        btnMenu.onClick.AddListener(actionMenu);
    }

    void activePauseScreen() {
        pauseScreen.SetActive(true);
    }

    void actionResume() {
        pauseScreen.SetActive(false);
        GameController.Instance.UserInteraction(true);
        GameController.Instance.SetResumeGameState();
    }

    void actionMenu() {
        GameController.Instance.SetResumeGameState();
        GameController.Instance.UserInteraction(true);
        pauseScreen.SetActive(false);
        if (OnActiveMenu != null) OnActiveMenu(true);
    }
}
