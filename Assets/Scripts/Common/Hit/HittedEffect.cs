using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[System.Serializable]
public class HittedEffect {



	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private EffectType Type;

	[SerializeField] private bool isFollowParentRotation = false;

	//========================================================================================
	//                                     public
	//========================================================================================

	public enum EffectType {

		Hero1,
		Hero2,
		Hero3,
		Hero_Strong,
		Evil1,
		Evil2,
		Evil3,
		Evil_Strong,
		Specialist1,
		Specialist2,
		Specialist3,
		Specialist_Strong,
		Enemy,

	}

	public EffectBase CreateParticle(Vector3 HittedPos , Vector3 AttackPos) {

		int num = (int)Type;
		var par = ResourceManager.Instance.Get<EffectBase>(DirectryNames[num], EffectNames[num]);

		if (isFollowParentRotation) {

			Vector3 dir = (HittedPos - AttackPos);
			dir.y = dir.y < 0f ? -dir.y : dir.y;
			dir.Normalize();

			MyEffect = GameObject.Instantiate(par);
			EffectSupport.Follow(MyEffect, HittedPos, dir);
		}
		else {

			MyEffect = GameObject.Instantiate(par);
			EffectSupport.FollowPosition(MyEffect, (HittedPos));
		}

		return MyEffect;
	}

	//========================================================================================
	//                                    protected
	//========================================================================================


	EffectBase _MyEffect;
	public EffectBase MyEffect {
		private set { _MyEffect = value; }
		get { return _MyEffect; }
	}
      

	//========================================================================================
	//                                     private
	//========================================================================================

	private readonly ReadOnlyCollection<string> DirectryNames = Array.AsReadOnly(new string[] {
		ConstDirectry.DirParticle,
		ConstDirectry.DirParticle,
		ConstDirectry.DirParticle,
		ConstDirectry.DirParticle,

		ConstDirectry.DirParticle,
		ConstDirectry.DirParticle,
		ConstDirectry.DirParticle,
		ConstDirectry.DirParticle,

		ConstDirectry.DirParticleEdo,
		ConstDirectry.DirParticleEdo,
		ConstDirectry.DirParticleEdo,
		ConstDirectry.DirParticleEdo,

		ConstDirectry.DirParticle,
	});

	private readonly ReadOnlyCollection<string> EffectNames = Array.AsReadOnly(new string[] {
		ConstEffects.HeroHit,
		ConstEffects.HeroHit,
		ConstEffects.HeroHit,
		ConstEffects.HeroHit,

		ConstEffects.ViranHit,
		ConstEffects.ViranHit,
		ConstEffects.ViranHit,
		ConstEffects.ViranHit,

		ConstEffects.StandardHittedEffect,
		ConstEffects.StandardHittedEffect,
		ConstEffects.StandardHittedEffect,
		ConstEffects.StandardHittedEffect,

		ConstEffects.Hitted,
	});
}
