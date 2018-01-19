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
#else
	private void Awake() {

		Time.timeScale = 1.0f;
	}

#endif

	public void TimeSpeedChange() {

		Time.timeScale = slider.value;
		TimeSpeed = slider.value;
		text.text = string.Format("{0:0.00} 倍速" , TimeSpeed);
	}

	public void TimeSpeedChange(float speed) {

		Time.timeScale = slider.value = speed;
		TimeSpeed = speed;
		text.text = string.Format("{0:0.00} 倍速", TimeSpeed);
	}

	public void TimeSpeedReset() {

		Time.timeScale = slider.value = 1f;
		TimeSpeed = 1f;
		text.text = string.Format("{0:0.00} 倍速", TimeSpeed);
	}
}
