using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour {

	enum PopupType {
		Normal,
	}

	//IFPopUp Create(PopupType type) {

	//}
}

interface IFPopUp {

	void Open(int x, int y,int offsetX = 0 ,int offsetY = 0);
	void Open(Transform trs3D, int offsetX = 0, int offsetY = 0);

	void Close();
}