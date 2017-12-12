using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTimeController : MonoBehaviour {

	[Range(0f, 10f)][Tooltip("時間を倍速させる(1なら通常)")]
	[SerializeField] public float TimeSpeed = 1f;
}
