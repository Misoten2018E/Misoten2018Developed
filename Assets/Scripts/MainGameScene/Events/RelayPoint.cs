using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RelayPoint : MonoBehaviour,DebuggableObject {

	public void Debug(bool isDebugMode) {

		var m = GetComponent<MeshRenderer>();
		m.enabled = isDebugMode;

	}
}
