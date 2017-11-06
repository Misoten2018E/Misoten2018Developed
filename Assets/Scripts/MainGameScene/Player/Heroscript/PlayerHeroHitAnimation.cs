using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHeroHitAnimation : HitAnimationBase{

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
        HitSpecialAnimation.Activate();
    }

    public override void Destruct()
    {

    }

    public override void Initialize(PlayerBase player)
    {
        myPlayer = player;

        var preAction = ResourceManager.Instance.Get<HitSeriesofAction>(ConstDirectry.DirPrefabsHit, ConstActionHitData.Action);
        HitWeakattack1Animation = Instantiate(preAction);
        HitWeakattack1Animation.Initialize(myPlayer);

        preAction = ResourceManager.Instance.Get<HitSeriesofAction>(ConstDirectry.DirPrefabsHit, ConstActionHitData.ActionSuction);
        HitWeakattack2Animation = Instantiate(preAction);
        HitWeakattack2Animation.Initialize(myPlayer);

        preAction = ResourceManager.Instance.Get<HitSeriesofAction>(ConstDirectry.DirPrefabsHit, ConstActionHitData.Action);
        HitWeakattack3Animation = Instantiate(preAction);
        HitWeakattack3Animation.Initialize(myPlayer);

        preAction = ResourceManager.Instance.Get<HitSeriesofAction>(ConstDirectry.DirPrefabsHit, ConstActionHitData.ActionSuction);
        HitStrongattackAnimation = Instantiate(preAction);
        HitStrongattackAnimation.Initialize(myPlayer);

        preAction = ResourceManager.Instance.Get<HitSeriesofAction>(ConstDirectry.DirPrefabsHit, ConstActionHitData.ActionSuction);
        HitSpecialAnimation = Instantiate(preAction);
        HitSpecialAnimation.Initialize(myPlayer);
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
    HitSeriesofAction HitSpecialAnimation;
}
