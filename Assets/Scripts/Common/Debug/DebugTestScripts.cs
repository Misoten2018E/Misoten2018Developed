using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTestScripts : SceneStartEvent {

	// Use this for initialization
	void Start () {

		StartCoroutine(aa());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void SceneDelayInit() {

		SceneEventManager.Instance.GameStart();

	}

	private IEnumerator aa() {

		yield return new WaitForSeconds(2f);

		CameraManager.Instance.SubCamera.Focus(transform,5f);

	}
}
