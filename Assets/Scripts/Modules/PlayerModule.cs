using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common.Layout;
using System;

public class PlayerModule : ScriptableObject {
    [SerializeField]
    List<Player> levels;    

    internal List<Player> Levels {
        get { 
            return levels;
        }
    }
}


