#if UNITY_EDITOR 
using UnityEditor;
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


