using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsCount : MonoBehaviour {


	private void Start() {

#if UNITY_DEBUG
		text = GetComponent<Text>();
		text.text = "";

#else

		Destroy(gameObject);

#endif

	}

	void Update() {

#if UNITY_DEBUG

			float fps = 1f / Time.deltaTime;

			string log = string.Format("{0}fps", System.Math.Round(fps, 2));

			text.text = log;
#endif	

	}

	Text text;
}
