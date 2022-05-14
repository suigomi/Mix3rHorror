using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Mochineko.SimpleReorderableList.Samples.Editor
{
	[CustomEditor(typeof(AppearanceManager))]
	[CanEditMultipleObjects]
	public class ReorderableListEditor : UnityEditor.Editor
	{
		private ReorderableList reorderableList1;
		private ReorderableList reorderableList2;
		private ReorderableList reorderableList3;
		private void OnEnable()
		{
			reorderableList1 = new ReorderableList(serializedObject.FindProperty("dayObjects"));
			reorderableList2 = new ReorderableList(serializedObject.FindProperty("nightObjects"));
			reorderableList3 = new ReorderableList(serializedObject.FindProperty("appearanceTuple"));
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EditorGUI.BeginChangeCheck();
			{
				EditorFieldUtility.ReadOnlyComponentField(target as MonoBehaviour, this);
				if (reorderableList1 != null)
                {
					reorderableList1.Layout();
                }
				if (reorderableList2 != null)
				{
					reorderableList2.Layout();
				}
				if (reorderableList3 != null)
                {
					reorderableList3.Layout();
				}

			}
			if (EditorGUI.EndChangeCheck())
			{
				serializedObject.ApplyModifiedProperties();
			}
		}
	}
}
