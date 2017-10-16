using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(RelayPoint))]
public class RelayPointEditor : Editor {


	RelayPoint myObj = null;

	void OnEnable() {

		//	GetChild();
		myObj = target as RelayPoint;
	}

	public override void OnInspectorGUI() {

		base.OnInspectorGUI();

		//if ((myObj.ManageList != null)) {
		//	for (int i = 0; i < myObj.ManageList.Count; i++) {

		//		if (myObj.ManageList[i] == null) {
		//			continue;
		//		}

		//		EditorGUILayout.LabelField(myObj.ManageList[i].ToString());
		//	}
		//}

		//if (GUILayout.Button("子オブジェクトの再取得")) {

		//	GetChild();

		//}
	}
}
#endif