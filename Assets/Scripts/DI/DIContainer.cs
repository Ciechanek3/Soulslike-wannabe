using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class DIContainer
{
    private readonly Dictionary<Type, Dictionary<string, object>> _dependencies = new();

    public void Register<T>(T dependency, string tag = null)
    {
        var type = dependency.GetType();
        if (!_dependencies.ContainsKey(type))
        {
            _dependencies[type] = new Dictionary<string, object>();
        }
        _dependencies[type][tag ?? string.Empty] = dependency;
    }

    public void AutoRegister(object target)
    {
        var type = target.GetType();

        foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        {
            var registerAttribute = field.GetCustomAttribute<RegisterAttribute>();
            if (registerAttribute != null)
            {
                var fieldValue = field.GetValue(target);
                if (fieldValue != null)
                {
                    Register(fieldValue, registerAttribute.Tag);
                }
            }
        }

        foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        {
            var registerAttribute = property.GetCustomAttribute<RegisterAttribute>();
            if (registerAttribute != null)
            {
                var propertyValue = property.GetValue(target);
                if (propertyValue != null)
                {
                    Register(propertyValue, registerAttribute.Tag);
                }
            }
        }
    }

    public object Resolve(Type type, string tag = null)
    {
        if (_dependencies.TryGetValue(type, out var taggedInstances))
        {
            if (taggedInstances.TryGetValue(tag ?? string.Empty, out var instance))
            {
                return instance;
            }
        }
        Debug.LogError("Missing Dependency with " + tag);
        return default(object);
    }

    public void InjectDependencies(object target)
    {
        var type = target.GetType();

        foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        {
            var injectAttribute = field.GetCustomAttribute<InjectAttribute>();
            if (injectAttribute != null)
            {
                var dependency = Resolve(field.FieldType, injectAttribute.Tag);
                field.SetValue(target, dependency);
            }
        }

        foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        {
            var injectAttribute = property.GetCustomAttribute<InjectAttribute>();
            if (injectAttribute != null)
            {
                var dependency = Resolve(property.PropertyType, injectAttribute.Tag);
                property.SetValue(target, dependency);
            }
        }
    }
}
