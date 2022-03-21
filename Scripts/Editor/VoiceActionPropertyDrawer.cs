#if UNITY_EDITOR 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(VoiceAction))]
public class VoiceActionPropertyDrawer : PropertyDrawer
{
    public VisualTreeAsset tree;
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // Create a new VisualElement to be the root the property UI
        var root = new VisualElement();
        tree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Voice Action Tool/VisualTrees/NewVoiceActionInspector.uxml");
        tree.CloneTree(root);


        // Return the finished UI
        return root;
    }
}
#endif


