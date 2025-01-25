using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

public abstract class TypeSelectorDrawer<T> : PropertyDrawer
{
    private static Dictionary<Type, Type[]> classCache = new Dictionary<Type, Type[]>();
    private static Dictionary<string, FieldInfo[]> fieldCache = new Dictionary<string, FieldInfo[]>();
    private static Dictionary<string, object> instanceCache = new Dictionary<string, object>();

    private Type[] GetClassesImplementingInterface(Type targetType)
    {
        if (!classCache.TryGetValue(targetType, out var types))
        {
            types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => targetType.IsAssignableFrom(t) && !t.IsAbstract)
                .ToArray();

            classCache[targetType] = types;
        }
        return types;
    }

    private object GetOrCreateInstance(string className, Type classType)
    {
        if (!instanceCache.TryGetValue(className, out var instance))
        {
            instance = Activator.CreateInstance(classType);
            instanceCache[className] = instance;
        }
        return instance;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.String)
        {
            EditorGUI.LabelField(position, label.text, "Use ClassSelector with a string field.");
            return;
        }

        var targetType = typeof(T);
        var types = GetClassesImplementingInterface(targetType);
        var classNames = types.Select(t => t.Name).ToArray();

        EditorGUI.BeginProperty(position, label, property);

        EditorGUILayout.LabelField(label.text, EditorStyles.boldLabel);

        // Dropdown for selecting class
        int selectedIndex = Mathf.Max(0, Array.IndexOf(classNames, property.stringValue));
        EditorGUI.BeginChangeCheck();
        selectedIndex = EditorGUILayout.Popup("Select Class", selectedIndex, classNames);

        if (EditorGUI.EndChangeCheck())
        {
            property.stringValue = classNames[selectedIndex];
        }

        // Get selected class and instance
        var selectedClass = classNames[selectedIndex];
        var selectedClassType = types[selectedIndex];
        var instance = GetOrCreateInstance(selectedClass, selectedClassType);

        // Get or cache fields
        if (!fieldCache.TryGetValue(selectedClass, out var fields))
        {
            fields = selectedClassType.GetFields().Where(f => f.IsPublic && !f.IsStatic).ToArray();
            fieldCache[selectedClass] = fields;
        }

        // Display editable fields
        foreach (var field in fields)
        {
            var fieldValue = field.GetValue(instance);
            string labelWithType = $"{field.FieldType.Name} {field.Name}";

            if (fieldValue is int intValue)
                field.SetValue(instance, EditorGUILayout.IntField(labelWithType, intValue));
            else if (fieldValue is float floatValue)
                field.SetValue(instance, EditorGUILayout.FloatField(labelWithType, floatValue));
            else if (fieldValue is string stringValue)
                field.SetValue(instance, EditorGUILayout.TextField(labelWithType, stringValue));
            else if (fieldValue is bool boolValue)
                field.SetValue(instance, EditorGUILayout.Toggle(labelWithType, boolValue));
            else if (fieldValue is Vector3 vectorValue)
                field.SetValue(instance, EditorGUILayout.Vector3Field(labelWithType, vectorValue));
            else
                EditorGUILayout.LabelField($"{field.Name} (Unsupported Type)");
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }
}