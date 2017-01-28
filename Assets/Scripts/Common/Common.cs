using UnityEngine;
using System.Collections.Generic;
using Editor.Layout;
using System;

namespace Common.Layout {
    public class ColorKey {
        public enum ColorKeyList {
            red,
            green,
            blue
        }

        public static Color GetColor(ColorKey.ColorKeyList colorEnum) {
            var auxColor = Color.white;
            switch (colorEnum) {
                case ColorKey.ColorKeyList.red:
                    auxColor = Color.red;
                    break;
                case ColorKey.ColorKeyList.green:
                    auxColor = Color.green;
                    break;
                case ColorKey.ColorKeyList.blue:
                    auxColor = Color.blue;
                    break;
            }

            return auxColor;
        }
    }
       
    public class Common {}
}
