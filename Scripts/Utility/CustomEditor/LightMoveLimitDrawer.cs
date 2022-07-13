using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Game.SpotLight.Struct;

namespace Utility.CustomEditor
{
#if UNITY_EDITOR
    //MEMO: 参考: https://colab.research.google.com/drive/1pqRjiR5nsaFW23sZzqoDPOOciV-izdbE?authuser=1
    [CustomPropertyDrawer(typeof(MoveLimitStruct))]
    public class LightMoveLimitDrawer : PropertyDrawer
    {
        private const string RightPosString = "right.pos";
        private const string RightRotString = "right.rot";
        private const string LeftPosString = "left.pos";
        private const string LeftRotString = "left.rot";

        private class PropertyData
        {
            public SerializedProperty rightPosProperty;
            public SerializedProperty rightRotProperty;
            public SerializedProperty leftPosProperty;
            public SerializedProperty leftRotProperty;
        }

        private Dictionary<string, PropertyData> _propertyDataPerPropertyPath = new Dictionary<string, PropertyData>();
        private PropertyData _property;

        private float LineHeight
        {
            get { return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing; }
        }

        private void Init(SerializedProperty property)
        {
            if (_propertyDataPerPropertyPath.TryGetValue(property.propertyPath, out _property))
            {
                return;
            }

            _property = new PropertyData();
            _property.rightPosProperty = property.FindPropertyRelative(RightPosString);
            _property.rightRotProperty = property.FindPropertyRelative(RightRotString);
            _property.leftPosProperty = property.FindPropertyRelative(LeftPosString);
            _property.leftRotProperty = property.FindPropertyRelative(LeftRotString);
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
                // プロパティ名を表示して折り畳み状態を得る
                property.isExpanded = EditorGUI.Foldout(new Rect(fieldRect), property.isExpanded, label);
                if (property.isExpanded)
                {
                    using (new EditorGUI.PropertyScope(fieldRect, GUIContent.none, property))
                    {
                        EditorGUI.indentLevel += 1;
                        EditorGUIUtility.labelWidth = 60f;
                        
                        
                        
                        fieldRect.width /= 2;
                        //Debug.Log($"fieldRect.width:{fieldRect.width}");
                        if (fieldRect.width<=154)
                        {
                            fieldRect.width = 154;
                        }
                        //fieldRect.xMax = 30;
                        //fieldRect.xMin = fieldRect.x-10;
                        fieldRect.y += LineHeight;

                        var rightFieldRect = fieldRect;
                        
                        rightFieldRect = EditorGUI.PrefixLabel(rightFieldRect,new GUIContent("Left"));
           
                        //rightFieldRect.min = new Vector2(rightFieldRect.x+40, rightFieldRect.y);
                        //rightFieldRect.max = new Vector2(rightFieldRect.x+190, rightFieldRect.y);
                        rightFieldRect.x += 50;
                        var firstRect = rightFieldRect;
                        EditorGUI.PropertyField(new Rect(firstRect), _property.leftPosProperty);
                        
                        var secondRect = rightFieldRect;
                        secondRect.x += firstRect.width;
                        EditorGUI.PropertyField(new Rect(secondRect), _property.leftRotProperty);

                        
                        
                        fieldRect.y += LineHeight;
                        var secondFieldRect = fieldRect;
                        
                        secondFieldRect = EditorGUI.PrefixLabel(secondFieldRect,new GUIContent("Right"));
                        
                        secondFieldRect.x += 50;
                        var thirdRect = secondFieldRect;
                        EditorGUI.PropertyField(new Rect(thirdRect), _property.rightPosProperty);

                        var fourthRect = secondFieldRect;
                        fourthRect.x += thirdRect.width;
                        EditorGUI.PropertyField(new Rect(fourthRect), _property.rightRotProperty);

                    }
                }
            }
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            Init(property);
            // (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) x 行数 で描画領域の高さを求める
            return LineHeight * (property.isExpanded ? 3 : 1);
        }
    }
    #endif
}