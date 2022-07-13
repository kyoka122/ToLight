using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Game.SpotLight.Struct;

namespace Utility.CustomEditor
{
    //MEMO: 参考: https://colab.research.google.com/drive/1pqRjiR5nsaFW23sZzqoDPOOciV-izdbE?authuser=1
    //[CustomPropertyDrawer(typeof(PosAndRot))]

#if UNITY_EDITOR
    public class PosAndRotPropertyDrawer : PropertyDrawer
    {
        private const string PosString="pos";
        private const string RotString="rot";
        
        private class PropertyData
        {
            public SerializedProperty posProperty;
            public SerializedProperty rotProperty;
        }

        private Dictionary<string, PropertyData> _propertyDataPerPropertyPath = new Dictionary<string, PropertyData>();
        private PropertyData _property;

        private float LineHeight { get { return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing; } }

        private void Init(SerializedProperty property)
        {
            if (_propertyDataPerPropertyPath.TryGetValue(property.propertyPath, out _property)){
                return;
            }

            _property = new PropertyData();
            _property.posProperty = property.FindPropertyRelative(PosString);
            _property.rotProperty = property.FindPropertyRelative(RotString);
            _propertyDataPerPropertyPath.Add(property.propertyPath, _property);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Init(property);
            var fieldRect = position;
            // インデントされた位置のRectが欲しければこっちを使う
            //var indentedFieldRect   = EditorGUI.IndentedRect(fieldRect);
            fieldRect.height = LineHeight;
        

            // Prefab化した後プロパティに変更を加えた際に太字にしたりする機能を加えるためPropertyScopeを使う
            using (new EditorGUI.PropertyScope(fieldRect, label, property))
            {
                //fieldRect.x += 60;
                //fieldRect.width /= 2;

                EditorGUI.PropertyField(new Rect(fieldRect), _property.posProperty);

                // Typeを描画
                fieldRect.x += fieldRect.width;
                EditorGUI.PropertyField(new Rect(fieldRect), _property.rotProperty);
            }

        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            Init(property);
            // (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) x 行数 で描画領域の高さを求める
            return LineHeight;
        }
    }
#endif
}