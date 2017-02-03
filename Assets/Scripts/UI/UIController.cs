using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

    [SerializeField]
    Transform campaingController;

    [SerializeField]
    Transform menuController;

    void Awake() {
        MenuUIController.OnActiveCampaing += ActiveCampaing;
        CampaingUIController.OnActiveMenu += ActiveMenu;
        PauseController.OnActiveMenu += ActiveMenu;
    }

    void OnDestroy() {
        MenuUIController.OnActiveCampaing -= ActiveCampaing;
        CampaingUIController.OnActiveMenu -= ActiveMenu;
        PauseController.OnActiveMenu -= ActiveMenu;
    }

    void ActiveCampaing(bool active) {
        campaingController.gameObject.SetActive(true);
    }

    void ActiveMenu(bool active) {
        menuController.gameObject.SetActive(true);
    }
}
