using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TimelineSystem))]
public class TimelineSystemEditor : Editor {

	TimelineSystem myObj = null;

	void OnEnable() {

		//	GetChild();
		myObj = target as TimelineSystem;
	}

	public override void OnInspectorGUI() {

		base.OnInspectorGUI();

		if ((myObj.Group != null)) {
			for (int i = 0; i < myObj.Group.Count; i++) {

				if (myObj.Group[i] == null) {
					EditorGUILayout.LabelField("null");
					continue;
				}

				EditorGUILayout.LabelField(myObj.Group[i].ToString());
			}
		}
	}
}
