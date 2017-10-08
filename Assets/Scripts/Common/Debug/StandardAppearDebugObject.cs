using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardAppearDebugObject : MonoBehaviour ,DebuggableObject{

	public void Debug(bool isDebugMode) {
		gameObject.SetActive(isDebugMode);
	}
}
