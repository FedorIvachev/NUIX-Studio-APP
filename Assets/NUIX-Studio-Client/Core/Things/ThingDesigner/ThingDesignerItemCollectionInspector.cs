using UnityEngine;

using UnityEditor;

using Microsoft.MixedReality.Toolkit.Utilities.Editor;
#if UNITY_EDITOR

namespace Tsinghua.HCI.IoThingsLab
{
    /// <summary>
    /// This class is used to handle the operation with CustomEditor
    /// used for ThingDesigner and making it possible to create new buttons 
    /// inside Unity editor
    /// </summary>

    [CustomEditor(typeof(ThingDesignerItemCollection), true)]
    public class ThingDesignerItemCollectionInspector : Editor
    {
        private SerializedProperty items;
        private SerializedProperty _buttonPrefab;

        protected virtual void OnEnable()
        {
            items = serializedObject.FindProperty("items");
            _buttonPrefab = serializedObject.FindProperty("_buttonPrefab");

        }

        protected void OnInspectorGUIInsertion()
        {
            EditorGUILayout.PropertyField(items);
            EditorGUILayout.PropertyField(_buttonPrefab);
        }

        sealed public override void OnInspectorGUI()
        {

            serializedObject.Update();
            
            OnInspectorGUIInsertion();

            serializedObject.ApplyModifiedProperties();
            // Place the button at the bottom
            ThingDesignerItemCollection collection = (ThingDesignerItemCollection)target;
            if (GUILayout.Button("Add Items"))
            {
                collection.AddItemsToCollection();
            }
        }
        
    }
}
#endif // UNITY_EDITOR
