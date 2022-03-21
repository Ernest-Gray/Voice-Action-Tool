#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System;

[CustomEditor(typeof(RandomVoice))]
public class RandomVoiceSOEditor : Editor
{
    private VisualElement root;
    private RandomVoice randomVoice;


    public VisualTreeAsset treeAsset;
    public override VisualElement CreateInspectorGUI()
    {
        //Set global variable for the scriptable object
        randomVoice = target as RandomVoice;
        EditorUtility.SetDirty(this);
        EditorUtility.SetDirty(target);
        EditorUtility.SetDirty(randomVoice);

        // Create a new VisualElement to be the root of our inspector UI
        root = new VisualElement();
        treeAsset.CloneTree(root);
        root.Q<Label>("Header").text = randomVoice.name;
        root.Q<PropertyField>("pf").RegisterCallback<ChangeEvent<System.Object>>(doSomething);

        return root;
    }
    public void doSomething(System.Object obj)
    {
        serializedObject.ApplyModifiedProperties();
    }
}
#endif