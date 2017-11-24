using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHitAnimation : HitAnimationBase{

    //========================================================================================
    //                                    inspector
    //========================================================================================



    //========================================================================================
    //                                    public override
    //========================================================================================

    public override void HitAnimationWeakattack1()
    {
       
    }

    public override void HitAnimationWeakattack2()
    {
       
    }

    public override void HitAnimationWeakattack3()
    {
       
    }

    public override void HitAnimationStrongattack()
    {
        HitStrongAtack1Animation.Activate();
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

        var preAction = ResourceManager.Instance.Get<HitSeriesofAction>(ConstDirectry.DirPrefabsHitPlayer, ConstActionHitData.ActionSpecialistStrong);
        HitStrongAtack1Animation = Instantiate(preAction);
        HitStrongAtack1Animation.Initialize(myPlayer);

        

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


    HitSeriesofAction HitStrongAtack1Animation;
   
}
