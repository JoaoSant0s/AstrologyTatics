using UnityEngine;
using UnityEditor;
using Common.Layout;
namespace Editor.Layout {

    [CustomPropertyDrawer(typeof(ArrayLayout))]
    public class ArrayLayoutEditor : PropertyDrawer {

        private const int width = 19;
        private const int height = 17;

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
                    
                    if (LayoutDefinition.LayoutCenter.x != j || LayoutDefinition.LayoutCenter.z != i) {                        
                        EditorGUI.PropertyField(newposition, row.GetArrayElementAtIndex(i), GUIContent.none);
                    } else {
                        row.GetArrayElementAtIndex(i).boolValue = false;
                    }

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
