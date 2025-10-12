using UnityEditor;
using UnityEngine;


// находиться должен в папке EDITOR, дополняет в инспекторе компонент WorldGeneration
namespace myProject{
    [CustomEditor(typeof(WorldGeneration))]
    public class WorldGenerationEditor : UnityEditor.Editor{
        public override void OnInspectorGUI(){
            WorldGeneration myScript = (WorldGeneration)target;
            // отрисовка стандартных свойств
            DrawDefaultInspector();
            // target - компонент к которому привязан скрипт (WorldGeneration) 
            // GUILayout. Button - создает кнопку в инспекторе компонента 
            if (GUILayout.Button("Recalculate percent")){
                myScript.RecalculatePercent();
            }

            if (GUILayout.Button("ResetTileType")){
                myScript.ResetTileType();
            }
            if (GUILayout.Button("Smooth")){
                myScript.Smooth();
            }
        
        }
    }
}