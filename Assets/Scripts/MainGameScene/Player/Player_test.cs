using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_test : PlayerBase
{
    private enum PLAYER_TEST_STA
    {
        NORMAL = 0,
        WEAKATTACK1,
        WEAKATTACK2,
        WEAKATTACK3,
        STRONGATTACK,
        SPECIAL,
        PLAYER_TEST_STA_MAX
    }

    MultiInput input;
    PLAYER_TEST_STA player_test_sta;
    bool inputFlg;

    const int Player_test_MoveSpeed = 5;
    const int Player_test_RotationSpeed = 750;

    //Use this for initialization
    void Start () {
		//使わない方向で
	}
	
	// Update is called once per frame
	void Update () {
		//使わない方向で
	}

    override
    public void Playerinit(int playerno)
    {
        input = GetComponent<MultiInput>();
        input.PlayerNo = playerno;
        CharCon = this.GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        MoveSpeed = Player_test_MoveSpeed;
        RotationSpeed = Player_test_RotationSpeed;
        player_test_sta = PLAYER_TEST_STA.NORMAL;
        PlayerSta = (int)player_test_sta;
        inputFlg = false;
    }

    override
    public void PlayerUpdate()
    {
        velocity.x = input.GetXaxis() * MoveSpeed;
        velocity.z = input.GetYaxis() * MoveSpeed;
        velocity.y = 0;

        switch(player_test_sta)
        {
            case PLAYER_TEST_STA.NORMAL:
                Normal();
                break;
            case PLAYER_TEST_STA.WEAKATTACK1:
                NormalAction1();
                break;
            case PLAYER_TEST_STA.WEAKATTACK2:
                NormalAction2();
                break;
            case PLAYER_TEST_STA.WEAKATTACK3:
                NormalAction3();
                break;
            case PLAYER_TEST_STA.STRONGATTACK:
                StrongAction();
                break;
            case PLAYER_TEST_STA.SPECIAL:
                SpecialAction();
                break;

        }

        SetAnimatorData();
    }

    private void Normal()
    {
        if(input.GetButtonCircleTrigger())
        {
            aniendFlg = false;
            player_test_sta = PLAYER_TEST_STA.WEAKATTACK1;
            PlayerSta = (int)player_test_sta;
        }

        MoveCharacter();
    }

    private void NormalAction1()
    {
        RotationCharacter();

        if (CheckAnimationEND("NormalAction1"))
        {
            player_test_sta = PLAYER_TEST_STA.NORMAL;
            PlayerSta = (int)player_test_sta;
        }
    }

    private void NormalAction2()
    {

    }

    private void NormalAction3()
    {

    }

    private void StrongAction()
    {

    }

    private void SpecialAction()
    {

    }

    

}
