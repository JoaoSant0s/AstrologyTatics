using UnityEngine;
using UnityEditor;

namespace Editor.Layout {

    [CustomPropertyDrawer(typeof(ArrayLayout))]
    public class ArrayLayoutEditor : PropertyDrawer {

        private const int height = 17;
        private const int width = 19;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.PrefixLabel(position, label);
            Rect newposition = position;

            SerializedProperty data = property.FindPropertyRelative("rows");

            if (data.arraySize != width)
                data.arraySize = width;

            for (int j = 0; j < data.arraySize; j++) {
                newposition.y += 18f;
                SerializedProperty row = data.GetArrayElementAtIndex(j).FindPropertyRelative("row");
                newposition.height = 18f;

                if (row.arraySize != height)
                    row.arraySize = height;

                newposition.width = position.width / width;
                for (int i = row.arraySize - 1; i >= 0; i--) {

                    if(width / 2 == j && height / 2 == i) {
                        row.GetArrayElementAtIndex(i).boolValue = true;
                    }
                    
                    EditorGUI.PropertyField(newposition, row.GetArrayElementAtIndex(i), GUIContent.none);
                    newposition.y += 18f;
                }

                newposition.y = position.y;
                newposition.x += newposition.width;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return 18f * (height + 1);
        }

    }
}
