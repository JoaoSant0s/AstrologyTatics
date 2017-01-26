using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CampaingUIController : MonoBehaviour {

    public delegate void DefiningPlayers();
    public static event DefiningPlayers OnDefiningPlayers;

    public delegate void ActiveMenu(bool active);
    public static event ActiveMenu OnActiveMenu;

    [SerializeField]
    Button btnStart;
    [SerializeField]
    Button btnReturn;


    void Start() {
        btnStart.onClick.AddListener(actionStart);
        btnReturn.onClick.AddListener(actionReturn);        
    }


    void actionStart() {
        gameObject.SetActive(false);        
        if (OnDefiningPlayers != null) OnDefiningPlayers();
    }

    void actionReturn() {
        gameObject.SetActive(false);
        if (OnActiveMenu != null) OnActiveMenu(true);
    }

}
