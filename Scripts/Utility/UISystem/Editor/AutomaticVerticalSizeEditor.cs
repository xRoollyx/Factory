using UnityEditor;
using UnityEngine;


// находиться должен в папке EDITOR, дополняет в инспекторе компонент AutomaticVerticalSize
namespace myProject{
    [CustomEditor(typeof(AutomaticVerticalSize))]
    public class AutomaticVerticalSizeEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            // отрисовка стандартных свойств
            DrawDefaultInspector();
            // target - компонент к которому привязан скрипт (AutomaticVerticalSize) 
            // GUILayout. Button - создает кнопку в инспекторе компонента 
            if (GUILayout.Button("Recalculate size")) {
                AutomaticVerticalSize myScript = (AutomaticVerticalSize)target;
                myScript.AdjustSize();
            }
        }
    
    }
}