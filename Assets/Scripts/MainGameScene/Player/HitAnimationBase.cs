﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitAnimationBase : MonoBehaviour {

    public abstract void Initialize(PlayerBase player);
    public abstract void Destruct();

    //Actionごとに変える為コメントアウト
    //public abstract void ActionCircle();
    //public abstract void ActionCross();
    //public abstract void ActionSquare();
    //public abstract void ActionTriangle();

    public abstract void HitAnimationWeakattack1();
    public abstract void HitAnimationWeakattack2();
    public abstract void HitAnimationWeakattack3();
    public abstract void HitAnimationStrongattack();
    public abstract void HitAnimationSpecial();
}