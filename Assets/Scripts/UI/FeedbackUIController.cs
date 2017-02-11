using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class FeedbackUIController : MonoBehaviour {

    private const string VICTORY = "Você ganhou!";

    private const string DEFEAT = "Você Perdeu!";


    [SerializeField]
    Text labelFeedback;

    [SerializeField]
    Button buttonBack;


    void Awake() {
        buttonBack.onClick.AddListener(DefiningButton);
        DuelController.OnVictoryEvent += UpdateTexts;
    }

    void OnDestroy() {
        DuelController.OnVictoryEvent -= UpdateTexts;
    }

    void DefiningButton() {
        gameObject.SetActive(false);
        GameController.Instance.UserInteraction(true);
        SceneManager.LoadScene(0);
    }

    void UpdateTexts(bool victory) {
        labelFeedback.text = (victory)? VICTORY: DEFEAT;
    }
}
