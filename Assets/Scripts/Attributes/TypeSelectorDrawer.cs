using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

public abstract class TypeSelectorDrawer<T> : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.String)
        {
            // Use Reflection to find all classes that implement IClass
            var classes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(T).IsAssignableFrom(type) && !type.IsAbstract)
                .Select(type => type.Name)
                .ToArray();

            // Get the currently selected index
            int selectedIndex = Mathf.Max(0, Array.IndexOf(classes, property.stringValue));

            // Create a popup field for selection
            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, classes);
            property.stringValue = classes[selectedIndex];
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "Use ClassSelector with string.");
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }
}