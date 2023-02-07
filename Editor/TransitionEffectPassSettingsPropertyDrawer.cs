using UnityEngine;
using UnityEditor;

namespace Blooper.TransitionEffects.Editor
{
	[CustomPropertyDrawer(typeof(TransitionEffectPassSettings))]
	public class TransitionEffectPassSettingsPropertyDrawer : PropertyDrawer
	{
		private bool _showImage;
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			
			var activeRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
			var typeRect = new Rect(position.x, position.y + activeRect.height, position.width, EditorGUIUtility.singleLineHeight);
			var colorRect = new Rect(position.x, typeRect.y + typeRect.height, position.width, EditorGUIUtility.singleLineHeight);
			var transitionRect = new Rect(position.x, colorRect.y + colorRect.height, position.width, EditorGUIUtility.singleLineHeight);
			var imageRect = new Rect(position.x, transitionRect.y + transitionRect.height, position.width, EditorGUIUtility.singleLineHeight);

			EditorGUI.PropertyField(activeRect, property.FindPropertyRelative("Active"), new GUIContent("Active"));
			EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("TransitionType"), new GUIContent("Transition Type"));
			EditorGUI.PropertyField(colorRect, property.FindPropertyRelative("Color"), new GUIContent("Color"));
			EditorGUI.PropertyField(transitionRect, property.FindPropertyRelative("Transition"), new GUIContent("Transition"));

			
			//drop down
			var tType = property.FindPropertyRelative("TransitionType");
			if ((TransitionType)tType.enumValueIndex == TransitionType.Texture)
			{
				_showImage = true;
				EditorGUI.PropertyField(imageRect, property.FindPropertyRelative("Image"), new GUIContent("Texture"));
			}
			else
			{
				_showImage = false;
			}

			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(UnityEditor.SerializedProperty property, UnityEngine.GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight * (_showImage ? 5 : 4);
		}
	}
}