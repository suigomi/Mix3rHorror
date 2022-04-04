using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Mochineko.SimpleReorderableList.Samples.Editor
{
	[CustomEditor(typeof(Appearance))]
	[CanEditMultipleObjects]
	public class ReorderableListEditor : UnityEditor.Editor
	{
		private ReorderableList reorderableList;

		private void OnEnable()
		{
			reorderableList = new ReorderableList(serializedObject.FindProperty("appearanceTuple"));
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EditorGUI.BeginChangeCheck();
			{
				EditorFieldUtility.ReadOnlyComponentField(target as MonoBehaviour, this);

				if (reorderableList != null)
					reorderableList.Layout();
			}
			if (EditorGUI.EndChangeCheck())
			{
				serializedObject.ApplyModifiedProperties();
			}
		}
	}
}
