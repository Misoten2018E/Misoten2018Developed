using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugLogPanel : StandardAppearDebugObject {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private Button logOpenButton;

	private void Start() {
		gameObject.SetActive(false);
	}

	public void OpenLog() {

		gameObject.SetActive(true);
		logOpenButton.onClick.RemoveAllListeners();
		logOpenButton.onClick.AddListener(()=>CloseLog());

	}

	public void CloseLog() {

		logOpenButton.onClick.RemoveAllListeners();
		logOpenButton.onClick.AddListener(() => OpenLog());
		gameObject.SetActive(false);
	}
}
