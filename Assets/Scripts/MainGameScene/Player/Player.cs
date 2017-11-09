using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SceneStartEvent{
    enum PLAYER_STA
    {
        NORMAL = 0,
        CHANGE_HERO,
        CHANGE_HEEL,
        CHANGE_SPECIALIST,
        CHANGE_NORMAL,
        PLAYER_STA_MAX
    }
    const float CORRECTION = 0.5f;

    //MultiInput input;
    public int no;
    //public float MoveSpeed;
    //public float RotationSpeed;

    //CharacterController CharCon;
    //Vector3 velocity;
    public GameObject NormalCharacter;
    public GameObject HeroCharacter;
    public GameObject HeelCharacter;
    public GameObject SpecialistCharacter;
    GameObject NowCharacter;
    PlayerBase playerbase;
    int PlayerSta;
    Vector3 ChangeStartPos;
    Vector3 ChangeCenterPos;
    Vector3 ChangeGoPos;
    PLAYER_STA playersta;

    // Use this for initialization
    void Start () {
        //初期化管理のためコメントアウト
        //Vector3 workpos = new Vector3();

        ////input = GetComponent<MultiInput>();
        ////CharCon = this.GetComponent<CharacterController>();

        //workpos.Set(transform.position.x,transform.position.y, transform.position.z);
        //NowCharacter = Instantiate(NormalCharacter, workpos, Quaternion.identity) as GameObject;

        //playerbase = NowCharacter.GetComponent<PlayerBase>();
        //playerbase.Playerinit(no);
    }
	
	// Update is called once per frame
	void Update () {
       
        if (!isInitialized) return;

        float r = 6.0f;

        switch (playersta)
        {
            case PLAYER_STA.NORMAL:
                playerbase.PlayerUpdate();
                break;
            case PLAYER_STA.CHANGE_HERO:
                playerbase.ForciblyMove(ChangeGoPos);

                r = (Mathf.Abs(transform.position.x - ChangeGoPos.x) * Mathf.Abs(transform.position.x - ChangeGoPos.x)) + 
                    (Mathf.Abs(transform.position.z - ChangeGoPos.z) * Mathf.Abs(transform.position.z - ChangeGoPos.z));
                if (r < CORRECTION * CORRECTION)
                {
                    if(ChangeGoPos == ChangeCenterPos)
                    {
                        ChangeGoPos = ChangeStartPos;
                        ChangeCharacter(ConstPlayerSta.HeroCharacter);
                    }
                    else
                    {
                        playersta = PLAYER_STA.NORMAL;
                    }
                }
                
                break;
            case PLAYER_STA.CHANGE_HEEL:
                playerbase.ForciblyMove(ChangeGoPos);

                r = (Mathf.Abs(transform.position.x - ChangeGoPos.x) * Mathf.Abs(transform.position.x - ChangeGoPos.x)) +
                    (Mathf.Abs(transform.position.z - ChangeGoPos.z) * Mathf.Abs(transform.position.z - ChangeGoPos.z));
                if (r < CORRECTION * CORRECTION)
                {
                    if (ChangeGoPos == ChangeCenterPos)
                    {
                        ChangeGoPos = ChangeStartPos;
                        ChangeCharacter(ConstPlayerSta.HeelCharacter);
                    }
                    else
                    {
                        playersta = PLAYER_STA.NORMAL;
                    }
                }
                break;
            case PLAYER_STA.CHANGE_SPECIALIST:
                playerbase.ForciblyMove(ChangeGoPos);

                r = (Mathf.Abs(transform.position.x - ChangeGoPos.x) * Mathf.Abs(transform.position.x - ChangeGoPos.x)) +
                    (Mathf.Abs(transform.position.z - ChangeGoPos.z) * Mathf.Abs(transform.position.z - ChangeGoPos.z));
                if (r < CORRECTION * CORRECTION)
                {
                    if (ChangeGoPos == ChangeCenterPos)
                    {
                        ChangeGoPos = ChangeStartPos;
                        ChangeCharacter(ConstPlayerSta.SpecialistCharacter);
                    }
                    else
                    {
                        playersta = PLAYER_STA.NORMAL;
                    }
                }
                break;

        }

        if (!playerbase.PlayerIsLive())
        {

        }

        transform.position = playerbase.GetBodyPosition();
        
    }

    public override void SceneMyInit()
    {
        Vector3 workpos = new Vector3();

        //input = GetComponent<MultiInput>();
        //CharCon = this.GetComponent<CharacterController>();

        workpos.Set(transform.position.x, transform.position.y, transform.position.z);
        NowCharacter = Instantiate(NormalCharacter, workpos, Quaternion.identity) as GameObject;

        playerbase = NowCharacter.GetComponent<PlayerBase>();
        playerbase.Playerinit(no);
        PlayerSta = ConstPlayerSta.NormalCharacter;
        playersta = PLAYER_STA.NORMAL;

        isInitialized = true;
    }

    public int GetPlayerSta() { return PlayerSta; }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == ConstTags.EnemyAttack)
        {
            playerbase.PlayerDamage();
        }

        if (other.gameObject.tag == ConstTags.HeelArea && PlayerSta != ConstPlayerSta.HeelCharacter && playersta == PLAYER_STA.NORMAL)
        {
            playersta = PLAYER_STA.CHANGE_HEEL;
            ChangeStartPos = transform.position;
            ChangeCenterPos = other.gameObject.transform.position;
            ChangeStartPos.y = 0.0f;
            ChangeCenterPos.y = 0.0f;
            ChangeGoPos = ChangeCenterPos;
        }

        if (other.gameObject.tag == ConstTags.HeroArea && PlayerSta != ConstPlayerSta.HeroCharacter && playersta == PLAYER_STA.NORMAL)
        {
            playersta = PLAYER_STA.CHANGE_HERO;
            ChangeStartPos = transform.position;
            ChangeCenterPos = other.gameObject.transform.position;
            ChangeStartPos.y = 0.0f;
            ChangeCenterPos.y = 0.0f;
            ChangeGoPos = ChangeCenterPos;
        }

        if (other.gameObject.tag == ConstTags.SpecialistArea && PlayerSta != ConstPlayerSta.SpecialistCharacter && playersta == PLAYER_STA.NORMAL)
        {
            playersta = PLAYER_STA.CHANGE_SPECIALIST;
            ChangeStartPos = transform.position;
            ChangeCenterPos = other.gameObject.transform.position;
            ChangeStartPos.y = 0.0f;
            ChangeCenterPos.y = 0.0f;
            ChangeGoPos = ChangeCenterPos;
        }
    }

    private void ChangeCharacter(int Changechar)
    {
        Vector3 workpos = new Vector3();
        workpos.Set(transform.position.x, 0.0f, transform.position.z);

        Destroy(NowCharacter);

        switch (Changechar)
        {
            case ConstPlayerSta.NormalCharacter:
                NowCharacter = Instantiate(NormalCharacter, workpos, Quaternion.identity) as GameObject;

                playerbase = NowCharacter.GetComponent<PlayerBase>();
                playerbase.Playerinit(no);
                PlayerSta = ConstPlayerSta.NormalCharacter;
                break;
            case ConstPlayerSta.HeroCharacter:
                NowCharacter = Instantiate(HeroCharacter, workpos, Quaternion.identity) as GameObject;

                playerbase = NowCharacter.GetComponent<PlayerBase>();
                playerbase.Playerinit(no);
                PlayerSta = ConstPlayerSta.HeroCharacter;
                break;
            case ConstPlayerSta.HeelCharacter:
                NowCharacter = Instantiate(HeelCharacter, workpos, Quaternion.identity) as GameObject;

                playerbase = NowCharacter.GetComponent<PlayerBase>();
                playerbase.Playerinit(no);
                PlayerSta = ConstPlayerSta.HeelCharacter;
                break;
            case ConstPlayerSta.SpecialistCharacter:
                NowCharacter = Instantiate(SpecialistCharacter, workpos, Quaternion.identity) as GameObject;

                playerbase = NowCharacter.GetComponent<PlayerBase>();
                playerbase.Playerinit(no);
                PlayerSta = ConstPlayerSta.SpecialistCharacter;
                break;
        }
    }
}
