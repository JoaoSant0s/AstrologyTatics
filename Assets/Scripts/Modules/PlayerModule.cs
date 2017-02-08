using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common.Layout;
using System;

public class PlayerModule : ScriptableObject {
    [SerializeField]
    Player player;    

    public Player Player {
        get { 
            return player;
        }
    }
}


