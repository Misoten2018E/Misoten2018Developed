using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerInput : MonoBehaviour {

	//========================================================================================
	//                                    inspector
	//========================================================================================

	//========================================================================================
	//                                     public
	//========================================================================================

	public void ChangeIcons(int PlayerSta) {

		// 同一なら取りやめ
		if (PlayerSta == NowIconNum) {
			return;
		}

		// 排除演出
		var old = NowSetIcon;
		old.animController.StartAnimRemove();

		//Destroy(old.gameObject, 1f);
		StartCoroutine(GameObjectExtensions.DelayMethod(1f, () => {
			EndAnimation(old);
		}));

		var prefab = GetResource(PlayerSta);
		NowSetIcon = Instantiate(prefab);
		NowSetIcon.SetParentInit(transform);
		NowSetIcon.animController.StartAnimSet();
	}

	//========================================================================================
	//                                 public - override
	//========================================================================================

	// Use this for initialization
	void Start() {

		NowIconNum = -1;
		var prefab = GetResource(ConstPlayerSta.NormalCharacter);
		NowSetIcon = Instantiate(prefab);
		NowSetIcon.SetParentInit(transform);
	//	NowSetIcon.transform.SetParent(transform);
	}

	//========================================================================================
	//                                     private
	//========================================================================================

	// 定数定義
	const string HeroIcons = ("HeroIcons");
	const string ViranIcons = ("ViranIcons");
	const string NormalIcons = ("NormalIcons");
	const string SpecialistIcons = ("SpecialistIcons");

	// 現在のアイコンNo
	int NowIconNum;

	// 現在セットされたアイコン
	CharacterIcons NowSetIcon;

	/// <summary>
	/// プレハブを取得
	/// </summary>
	/// <param name="playerSta"></param>
	/// <returns></returns>
	private CharacterIcons GetResource(int playerSta) {

		CharacterIcons newIcon = null;

		switch (playerSta) {

			case ConstPlayerSta.HeroCharacter:
				newIcon = ResourceManager.Instance.Get<CharacterIcons>(ConstDirectry.DirPopup, HeroIcons);
				break;

			case ConstPlayerSta.HeelCharacter:
				newIcon = ResourceManager.Instance.Get<CharacterIcons>(ConstDirectry.DirPopup, ViranIcons);
				break;

			case ConstPlayerSta.NormalCharacter:
				newIcon = ResourceManager.Instance.Get<CharacterIcons>(ConstDirectry.DirPopup, NormalIcons);
				break;

			case ConstPlayerSta.SpecialistCharacter:
				newIcon = ResourceManager.Instance.Get<CharacterIcons>(ConstDirectry.DirPopup, SpecialistIcons);
				break;

			default:
				print("異常値");
				break;
		}
		return newIcon;
	}

	private void EndAnimation(CharacterIcons oldIcon) {

		Destroy(oldIcon.gameObject);
	}
}
