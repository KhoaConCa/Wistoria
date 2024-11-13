using UnityEditor;
using UnityEngine;

/// <summary>
/// Attribute để hiển thị danh sách Tag trong Inspector.
/// </summary>
public class TagSelectorAttribute : PropertyAttribute { }

/// <summary>
/// Custom Property Drawer cho TagSelectorAttribute.
/// </summary>
[CustomPropertyDrawer(typeof(TagSelectorAttribute))]
public class TagSelectorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Chỉ hỗ trợ biến kiểu string
        if (property.propertyType == SerializedPropertyType.String)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Hiển thị danh sách các tag
            property.stringValue = EditorGUI.TagField(position, label, property.stringValue);

            EditorGUI.EndProperty();
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "Use [TagSelector] with string.");
        }
    }
}
