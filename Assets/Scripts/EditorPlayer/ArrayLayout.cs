using UnityEngine;
using System.Collections;

namespace Editor.Layout { 

    [System.Serializable]
    public class ArrayLayout {

        [System.Serializable]
        public struct rowData {
            public bool[] row;
        }

        public rowData[] rows = new rowData[15];
    }

}

