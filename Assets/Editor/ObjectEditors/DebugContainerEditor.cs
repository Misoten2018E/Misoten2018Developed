using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(DebugContainer))]
[CanEditMultipleObjects]
public class DebugContainerEditor : Editor {

	DebugContainer myObj = null;

	void OnEnable() {

		//	GetChild();
		myObj = target as DebugContainer;
	}

	public override void OnInspectorGUI() {

		base.OnInspectorGUI();

		if ((myObj.ManageList != null)) {
			for (int i = 0; i < myObj.ManageList.Count; i++) {

				if (myObj.ManageList[i] == null) {
					continue;
				}

				EditorGUILayout.LabelField(myObj.ManageList[i].ToString());
			}
		}

		if (GUILayout.Button("子オブジェクトの再取得")) {

			GetChild();

		}
	}


	void GetChild() {

		myObj = target as DebugContainer;
		myObj.ManageList = GameObjectExtensions.FindChildOfInterface<Transform>(myObj.gameObject);

		// 自分を外す
		myObj.ManageList.Remove(myObj.transform);
	}
}
#endif