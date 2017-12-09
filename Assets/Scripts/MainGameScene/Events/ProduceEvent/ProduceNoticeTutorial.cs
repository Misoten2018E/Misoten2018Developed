using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceNoticeTutorial : ProduceEventBase {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[Tooltip("使用する演出オブジェクトをセット")]
	[SerializeField] TutorialWindowController NoticeWindow;

	[SerializeField] private TutorialWindowController.AnimType StartAnimation = TutorialWindowController.AnimType.Appear;
	[SerializeField] private TutorialWindowController.AnimType EndAnimation = TutorialWindowController.AnimType.Disappear;

	//[TextArea(1,3)]
	//[SerializeField] private string Texts;

	//[SerializeField] private Sprite SpriteTexture;

	//[Tooltip("どちらかが0なら初期設定を利用する")]
	//[SerializeField] private Vector2 SpriteSize = new Vector2(0, 0);

	[Tooltip("表示時間")]
	[SerializeField] private float DisplayTime = 2f;

	//========================================================================================
	//                                     public
	//========================================================================================

	//========================================================================================
	//                                 public - override
	//========================================================================================

	public override void ProduceStart() {

		NoticeWindow.gameObject.SetActive(true);
		NoticeWindow.Animation(StartAnimation);

		//NoticeWindow.TextArea = Texts;
		//NoticeWindow.ImageArea = SpriteTexture;

		//if (SpriteSize.x != 0f && SpriteSize.y != 0f) {
		//	NoticeWindow.ImageSize = SpriteSize;
		//}

		StartCoroutine(GameObjectExtensions.DelayMethod(DisplayTime, EndProduce));
	}


	//========================================================================================
	//                                     private
	//========================================================================================

	private void EndProduce() {

		NoticeWindow.Animation(EndAnimation);
	}

}
