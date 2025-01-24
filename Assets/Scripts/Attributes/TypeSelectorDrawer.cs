using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

public abstract class TypeSelectorDrawer<T> : PropertyDrawer
{
    private readonly Dictionary<string, FieldInfo[]> fieldCache = new Dictionary<string, FieldInfo[]>();
    private readonly Dictionary<string, object> instanceCache = new Dictionary<string, object>();

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.String)
        {
            EditorGUI.LabelField(position, label.text, "Use ClassSelector with a string field.");
            return;
        }

        // Get the attribute and its target type
        var targetType = typeof(T);

        // Find classes implementing the target type
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => targetType.IsAssignableFrom(t) && !t.IsAbstract)
            .ToArray();

        var classNames = types.Select(t => t.Name).ToArray();

        string selectedClass = property.stringValue;
        int selectedIndex = Mathf.Max(0, Array.IndexOf(classNames, selectedClass));
        selectedIndex = EditorGUI.Popup(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), label.text, selectedIndex, classNames);

        selectedClass = classNames[selectedIndex];
        property.stringValue = selectedClass;

        if (!instanceCache.TryGetValue(selectedClass, out var instance))
        {
            instance = Activator.CreateInstance(types[selectedIndex]);
            instanceCache[selectedClass] = instance;
        }

        if (!fieldCache.TryGetValue(selectedClass, out var fields))
        {
            fields = types[selectedIndex].GetFields().Where(f => f.IsPublic && !f.IsStatic).ToArray();
            fieldCache[selectedClass] = fields;
        }

        float yOffset = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        foreach (var field in fields)
        {
            Rect fieldRect = new Rect(position.x, position.y + yOffset, position.width, EditorGUIUtility.singleLineHeight);
            var fieldValue = field.GetValue(instance);

            string labelWithType = $"{field.FieldType.Name} {field.Name}";

            object newValue = new();
            switch (fieldValue)
            {
                case int intValue:
                    newValue = EditorGUI.IntField(fieldRect, labelWithType, intValue);
                    break;
                case float floatValue:
                    newValue = EditorGUI.FloatField(fieldRect, labelWithType, floatValue);
                    break;
                case string stringValue:
                    newValue = EditorGUI.TextField(fieldRect, labelWithType, stringValue);
                    break;
                case bool boolValue:
                    newValue = EditorGUI.Toggle(fieldRect, labelWithType, boolValue);
                    break;
                case Vector3 vectorValue:
                    newValue = EditorGUI.Vector3Field(fieldRect, labelWithType, vectorValue);
                    break;
                default:
                    EditorGUI.LabelField(fieldRect, labelWithType, $"Add type: {field.FieldType.Name}");
                    newValue = null;
                    break;
            }

            // Update the field value if it changes
            if (newValue != null && !Equals(newValue, fieldValue))
            {
                field.SetValue(instance, newValue);
            }

            yOffset += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }  
        }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Calculate height based on the selected class's fields
        if (property.propertyType != SerializedPropertyType.String) return EditorGUIUtility.singleLineHeight;

        if (fieldCache.TryGetValue(property.stringValue, out var fields))
        {
            return EditorGUIUtility.singleLineHeight * (1 + fields.Length) +
                   EditorGUIUtility.standardVerticalSpacing * fields.Length;
        }

        return EditorGUIUtility.singleLineHeight;
    }
}