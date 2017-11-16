using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeelHitAnimation : HitAnimationBase{

    //========================================================================================
    //                                    inspector
    //========================================================================================



    //========================================================================================
    //                                    public override
    //========================================================================================

    public override void HitAnimationWeakattack1()
    {
        HitWeakattack1Animation.Activate();
    }

    public override void HitAnimationWeakattack2()
    {
        HitWeakattack2Animation.Activate();
    }

    public override void HitAnimationWeakattack3()
    {
        HitWeakattack3Animation.Activate();
    }

    public override void HitAnimationStrongattack()
    {
        HitStrongattackAnimation.Activate();
    }

    public override void HitAnimationSpecial()
    {

    }

    public override void Destruct()
    {

    }

    public override void Initialize(PlayerBase player)
    {
        myPlayer = player;

        var preAction = ResourceManager.Instance.Get<HitSeriesofAction>(ConstDirectry.DirPrefabsHitHero, ConstActionHitData.ActionHeroWeak1);
        HitWeakattack1Animation = Instantiate(preAction);
        HitWeakattack1Animation.Initialize(myPlayer);

        preAction = ResourceManager.Instance.Get<HitSeriesofAction>(ConstDirectry.DirPrefabsHitHero, ConstActionHitData.ActionHeroWeak2);
        HitWeakattack2Animation = Instantiate(preAction);
        HitWeakattack2Animation.Initialize(myPlayer);

        preAction = ResourceManager.Instance.Get<HitSeriesofAction>(ConstDirectry.DirPrefabsHitHero, ConstActionHitData.ActionHeroWeak3);
        HitWeakattack3Animation = Instantiate(preAction);
        HitWeakattack3Animation.Initialize(myPlayer);

        preAction = ResourceManager.Instance.Get<HitSeriesofAction>(ConstDirectry.DirPrefabsHitHero, ConstActionHitData.ActionHeroStrong);
        HitStrongattackAnimation = Instantiate(preAction);
        HitStrongattackAnimation.Initialize(myPlayer);

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    //========================================================================================
    //                                    private
    //========================================================================================


    PlayerBase _myPlayer;
    public PlayerBase myPlayer
    {
        private set { _myPlayer = value; }
        get { return _myPlayer; }
    }


    HitSeriesofAction HitWeakattack1Animation;
    HitSeriesofAction HitWeakattack2Animation;
    HitSeriesofAction HitWeakattack3Animation;
    HitSeriesofAction HitStrongattackAnimation;
}
