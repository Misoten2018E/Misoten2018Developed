﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormal : PlayerBase{

    private enum PLAYER_NORMAL_STA
    {
        NORMAL = 0,
        SWAY,
        DAMAGE,
        PLAYER_STA_MAX
    }

    MultiInput input;
    PLAYER_NORMAL_STA player_Normal_sta;
    
    
    const int Player_Normal_MoveSpeed = 5;
    const int Player_Normal_RotationSpeed = 750;
    
    //Use this for initialization
    void Start()
    {
        //使わない方向で
    }

    // Update is called once per frame
    void Update()
    {
        //使わない方向で
    }

    override
    public void Playerinit(GameObject playerobj)
    {
        myPlayer = playerobj;
        Player player = playerobj.GetComponent<Player>();
        no = player.no;
        input = GetComponent<MultiInput>();
        input.PlayerNo = no;
        CharCon = this.GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        Model = transform.Find("BaseModel_Hero").transform;
        HP = gameObject.GetComponent<ObjectHp>();

        MoveSpeed = Player_Normal_MoveSpeed;
        RotationSpeed = Player_Normal_RotationSpeed;
        player_Normal_sta = PLAYER_NORMAL_STA.NORMAL;
        PlayerSta = (int)player_Normal_sta;
        nodamageflg = false;
    }

    override
    public void PlayerUpdate()
    {
        velocity.x = input.GetXaxis() * MoveSpeed;
        velocity.z = input.GetYaxis() * MoveSpeed;
        velocity.y = 0;

        switch (player_Normal_sta)
        {
            case PLAYER_NORMAL_STA.NORMAL:
                Normal();
                break;
            case PLAYER_NORMAL_STA.SWAY:
                SwayAction();
                break;
            case PLAYER_NORMAL_STA.DAMAGE:
                DamageAction();
                break;
        }

        NoDameCnt();

        SetAnimatorData();
    }

    public override void PlayerDamage(HitObjectImpact damage)
    {
        if (nodamageflg) return;

        player_Normal_sta = PLAYER_NORMAL_STA.DAMAGE;
        PlayerSta = (int)player_Normal_sta;
        damage.DamageHp(HP);
        animator.SetTrigger("Damage");
        nodamageflg = true;
        ModelTransformReset();
    }

    

    private void Normal()
    {
        CharCon.center = new Vector3(0, 1, 0);

        if (input.GetButtonSquareTrigger())
        {
            player_Normal_sta = PLAYER_NORMAL_STA.SWAY;
            PlayerSta = (int)player_Normal_sta;
            ModelTransformReset();
        }

        MoveCharacter();

        if (CheckAnimationSTART("wait") || CheckAnimationSTART("run"))
        {
            ModelTransformReset();
        }
    }

    private void SwayAction()
    {
        if (CheckAnimationEND("Damage"))
        {
            player_Normal_sta = PLAYER_NORMAL_STA.NORMAL;
            PlayerSta = (int)player_Normal_sta;
            ModelTransformReset();
        }
    }

    private void DamageAction()
    {
        if (CheckAnimationEND("Damage"))
        {
            player_Normal_sta = PLAYER_NORMAL_STA.NORMAL;
            PlayerSta = (int)player_Normal_sta;
            ModelTransformReset();
        }
    }
}
