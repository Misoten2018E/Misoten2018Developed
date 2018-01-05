using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class StandardAppearDebugMesh : MonoBehaviour ,DebuggableObject{

	public void Debug(bool isDebugMode) {
		var m = GetComponent<MeshRenderer>();
		m.enabled = isDebugMode;
	}



#if !UNITY_DEBUG

	private void Awake() {
		var m = GetComponent<MeshRenderer>();
		m.enabled = false;
	}
#endif

}
