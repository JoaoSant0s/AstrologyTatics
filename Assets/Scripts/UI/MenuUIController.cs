using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuUIController : MonoBehaviour {

    public delegate void ActiveCampaing(bool active);
    public static event ActiveCampaing OnActiveCampaing;

    [SerializeField]
    Button btnCampaing;

    [SerializeField]
    Button btnVersus;

    [SerializeField]
    Button btnSettings;

    void Awake() {
        btnCampaing.onClick.AddListener(actionCampaing);
        btnVersus.onClick.AddListener(actionVersus);
        btnSettings.onClick.AddListener(actionSettings);            
    }

    void actionCampaing() {
        gameObject.SetActive(false);
        if (OnActiveCampaing != null) OnActiveCampaing(true);
    }

    void actionVersus() {

    }

    void actionSettings() {

    }    

}
