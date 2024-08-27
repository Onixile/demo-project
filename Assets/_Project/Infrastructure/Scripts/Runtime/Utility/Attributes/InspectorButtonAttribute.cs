using System;
using System.Reflection;
using _Project.Infrastructure.Scripts.Runtime.Utility.Extensions;
using UnityEngine;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _Project.Infrastructure.Scripts.Runtime.Utility.Attributes
{
  [AttributeUsage(AttributeTargets.Method)]
  public class InspectorButtonAttribute : PropertyAttribute
  {
    public readonly string ButtonLabel;

    public InspectorButtonAttribute(string buttonLabel = null) =>
      ButtonLabel = buttonLabel;
  }
  
#if UNITY_EDITOR
  [CustomEditor(typeof(Object), true)]
  public class InspectorButtonAttributeDrawer : Editor
  {
    public override void OnInspectorGUI()
    {
      DrawDefaultInspector();

      Object targetObject = target;
      MethodInfo[] methods = targetObject.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

      foreach (var method in methods)
      {
        var attributes = method.GetCustomAttributes(typeof(InspectorButtonAttribute), true);
        if (attributes.Length > 0)
        {
          var buttonAttribute = (InspectorButtonAttribute)attributes[0];
          string buttonLabel = string.IsNullOrEmpty(buttonAttribute.ButtonLabel) ? method.Name.AddSpaceAfterCapital() : buttonAttribute.ButtonLabel;

          if (GUILayout.Button(buttonLabel))
            method.Invoke(targetObject, null);
        }
      }
    }
  }
  #endif
}