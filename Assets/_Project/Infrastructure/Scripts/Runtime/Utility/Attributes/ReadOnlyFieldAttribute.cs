using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _Project.Infrastructure.Scripts.Runtime.Utility.Attributes
{
  [AttributeUsage(AttributeTargets.Field)]
  public class ReadOnlyFieldAttribute : PropertyAttribute
  {
  }

#if UNITY_EDITOR
  [CustomPropertyDrawer(typeof(ReadOnlyFieldAttribute))]
  public class ReadOnlyFieldDrawer : PropertyDrawer
  {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      GUI.enabled = false;
      EditorGUI.PropertyField(position, property, label);
      GUI.enabled = true;
    }
  }
#endif
}