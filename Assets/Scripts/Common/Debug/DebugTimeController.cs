using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugTimeController : MonoBehaviour {

	[Range(0f, 20f)][Tooltip("時間を倍速させる(1なら通常)")]
	[SerializeField] public float TimeSpeed = 1f;

	[SerializeField] public Slider slider;
	[SerializeField] public Text text;

#if UNITY_DEBUG

	private void Awake() {

		Time.timeScale = TimeSpeed;
		slider.value = TimeSpeed;
		TimeSpeedChange();
	}


#endif

	public void TimeSpeedChange() {

		Time.timeScale = slider.value;
		text.text = string.Format("{0:0.00} 倍速" ,slider.value);
	}
}
