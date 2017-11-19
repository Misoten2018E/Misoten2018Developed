using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIcons : MonoBehaviour {


	public void SetParentInit(Transform parent) {

		rectTrs.SetParent(parent);
		rectTrs.anchoredPosition = new Vector2(0, 0);
		rectTrs.localRotation = Quaternion.identity;
	}


	RectTransform _rectTrs;
	public RectTransform rectTrs {
		get {
			if (_rectTrs == null) {
				_rectTrs = GetComponent<RectTransform>();
			}
			return _rectTrs;
		}
	}
      


	IconAnimationController _animController;
	public IconAnimationController animController {
		get {
			if (_animController == null) {
				_animController = GetComponent<IconAnimationController>();
			}
			return _animController;
		}
	}
      
}
