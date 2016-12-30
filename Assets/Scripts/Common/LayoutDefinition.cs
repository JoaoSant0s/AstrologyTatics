using UnityEngine;
using System.Collections.Generic;
using Editor.Layout;
using System;

namespace Common.Layout { 

    public class LayoutDefinition {

        private static Vector3 layoutCenter = new Vector3(9, 0, 8);
        private const int multiplierBase = 5;

        public static Vector3 LayoutCenter {
            get {
                return layoutCenter;
            }
        }

        internal static Vector3 ConvertPostion(Vector3 position) {
            return position * multiplierBase;
        }

        public static List<Vector3> ListPaths(ArrayLayout layoutPaths) {
            List<Vector3> listPaths = new List<Vector3>();

            for (int i = 0; i < layoutPaths.rows.Length; i++) {
                var row = layoutPaths.rows[i].row;
                for (int j = 0; j < row.Length; j++) {
                    Vector3 path = new Vector3(i, 0, j);
                    if (row[j] && !listPaths.Contains(path)) {
                        listPaths.Add( (path - layoutCenter) * multiplierBase );
                    }
                }
            }

            return listPaths;
        }
                
    }

}


