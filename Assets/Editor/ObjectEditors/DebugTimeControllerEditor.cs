using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(DebugTimeController))]
public class DebugTimeControllerEditor : Editor {

	DebugTimeController myObj = null;

	void OnEnable() {

		//	GetChild();
		myObj = target as DebugTimeController;
	}

	public override void OnInspectorGUI() {

		base.OnInspectorGUI();

		if (GUILayout.Button("現在の速度に設定")) {

			myObj.TimeSpeedChange(myObj.TimeSpeed);
		}

		if (GUILayout.Button("通常速度にリセット")) {

			myObj.TimeSpeedReset();

		}
	}
}

#endif