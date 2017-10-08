using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugContainer : MonoBehaviour ,DebuggableObject{

	[SerializeField] private List<Transform> _ManageList = new List<Transform>();

	public List<Transform> ManageList {
		set { _ManageList = value; }
		get { return _ManageList; }
	}


	public void Debug(bool isDebugMode) {

		foreach (var n in ManageList) {
			var component = n.GetComponent<DebuggableObject>();
			if (component != null) {
				component.Debug(isDebugMode);
			}
		}
	}

}
