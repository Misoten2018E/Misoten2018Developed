using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenInfoManager : MonoBehaviour {

	[SerializeField] List<ScreenInfoAnim> AnimList;
	

	public void AnimStart(int num) {
		if (AnimList.Count <= num) {
			return;
		}
		AnimList[num].StartAnimation();
	}

	public void AnimEnd(int num) {
		AnimList[num].EndAnimation();
	}
}
