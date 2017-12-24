using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : SceneStartEvent{

    const int ChangeMoveSpeed = 5;
    const float ChangeScale = 0.05f;
    protected const float InitY = 1.0f;

    CharacterController _CharCon;
    public CharacterController CharCon
    {
        protected set {
            if(_CharCon == null)
            {
                _CharCon = gameObject.GetComponent<CharacterController>();
            }
        }
        get { return _CharCon; }
    }

    public int no;
    public float nodamagetime;

    protected GameObject myPlayer;
    protected float MoveSpeed;
    protected float RotationSpeed;
    protected Vector3 velocity;

    protected Animator animator;
    protected Transform Model;
    protected Transform RootPos;

    protected int PlayerSta;

    protected ObjectHp HP;
    
    protected float Attack;
    protected bool nodamageflg;
    float nodamtotal = 0;
    protected float cpynodammax;

    // Use this for initialization
    void Start () {
		//使わない方向で
	}
	
	// Update is called once per frame
	void Update () {
		//使わない方向で
	}

    public void ForciblyMove(Vector3 moveposition,float speed)
    {
        Vector3 myposition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        velocity = moveposition - myposition;
        velocity.y = 0.0f;
        velocity.Normalize();
        velocity *= speed;

        MoveCharacter();

        CharCon.center = new Vector3(0,1 + no ,0);

        SetAnimatorData();
    }

    public void PlayerCityIn(Vector3 moveposition,Vector3 startpos)
    {
        Vector3 myposition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        velocity = moveposition - myposition;
        velocity.y = 0.0f;
        velocity.Normalize();
        velocity *= ChangeMoveSpeed;
        
        MoveCharacter();

        float maxlength = (Mathf.Abs(moveposition.x - startpos.x) * Mathf.Abs(moveposition.x - startpos.x)) +
                            (Mathf.Abs(moveposition.y - startpos.y) * Mathf.Abs(moveposition.y - startpos.y)) +
                            (Mathf.Abs(moveposition.z - startpos.z) * Mathf.Abs(moveposition.z - startpos.z));

        float nowlength = (Mathf.Abs(transform.position.x - startpos.x) * Mathf.Abs(transform.position.x - startpos.x)) +
                            (Mathf.Abs(transform.position.y - startpos.y) * Mathf.Abs(transform.position.y - startpos.y)) +
                            (Mathf.Abs(transform.position.z - startpos.z) * Mathf.Abs(transform.position.z - startpos.z));

        float S = 1 - nowlength / maxlength;
        
        if (S < 0.1f)
        {
            S = 1;
        }
        Model.localScale = new Vector3(S, S, S);

        PlayerSta = 0;
        SetAnimatorData();
    }

    public void PlayerCityOut(Vector3 moveposition, Vector3 startpos)
    {
        Vector3 myposition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        velocity = moveposition - myposition;
        velocity.y = 0.0f;
        velocity.Normalize();
        velocity *= ChangeMoveSpeed;
        
        MoveCharacter();

        float maxlength = (Mathf.Abs(moveposition.x - startpos.x) * Mathf.Abs(moveposition.x - startpos.x)) +
                            (Mathf.Abs(moveposition.y - startpos.y) * Mathf.Abs(moveposition.y - startpos.y)) +
                            (Mathf.Abs(moveposition.z - startpos.z) * Mathf.Abs(moveposition.z - startpos.z));

        float nowlength = (Mathf.Abs(transform.position.x - startpos.x) * Mathf.Abs(transform.position.x - startpos.x)) +
                            (Mathf.Abs(transform.position.y - startpos.y) * Mathf.Abs(transform.position.y - startpos.y)) +
                            (Mathf.Abs(transform.position.z - startpos.z) * Mathf.Abs(transform.position.z - startpos.z));

        float S = nowlength / maxlength;
        
        if (S > 0.4f)
        {
            S = 1;
        }
        Model.localScale = new Vector3(S,S,S);

        PlayerSta = 0;
        SetAnimatorData();
    }

    public void AttackUP(float attack,float speed)
    {
        Attack *= attack;
        MoveSpeed *= speed;
        print("stk"+ Attack);
    }

    public void AttackDOWN(float attack,float speed)
    {
        Attack /= attack;
        MoveSpeed /= speed;
        print("stk" + Attack);
    }

    public int GetCharacterSta()
    {
        return PlayerSta;
    }

    public virtual void Playerinit(GameObject playerobj)
    {

    }

    public virtual void PlayerUpdate()
    {
     
    }

    public virtual void PlayerDamage(HitObjectImpact damage)
    {

    }

    public virtual void PlayerDamageMotion()
    {

    }

    public virtual void PlayerPause()
    {

    }

    public Vector3 GetBodyPosition()
    {
        return RootPos.position;
    }

    public GameObject GetPlayerObj()
    {
        return myPlayer;
    }

    public int GetHP()
    {
        return HP.Hp;
    }

    public void SetCharConNoHit(bool flg)
    {
        var c = CharCon;
        if (c == null)
        {
            return;
        }

        if (flg)
        {
            CharCon.center = new Vector3(0,10+no*3,0);
        }
        else
        {
            CharCon.center = new Vector3(0, 0, 0);
        }
        
    }

    public virtual bool PlayerIsDeath()
    {
        bool res = false;
        
        if (HP.isDeath)
        {
            res = true;
        }

        return res;
    }

    protected void MoveCharacter()
    {
        Vector3 direction = new Vector3(velocity.x, velocity.y, velocity.z);
        if (direction.sqrMagnitude > 0f)
        {
            Vector3 forward = Vector3.Slerp(transform.forward, direction, RotationSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction));
            transform.LookAt(transform.position + forward);
        }

		var c = CharCon;
		if (c == null) {
			return;
		}
        CharCon.Move(velocity * Time.deltaTime);
    }

    protected void RotationCharacter()
    {
        Vector3 direction = new Vector3(velocity.x, velocity.y, velocity.z);
        if (direction.sqrMagnitude > 0f)
        {
            Vector3 forward = Vector3.Slerp(transform.forward, direction, RotationSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction));
            transform.LookAt(transform.position + forward);
        }
        
        var c = CharCon;
		if (c == null) {
			return;
		}
		CharCon.Move(new Vector3(0,0,0) * Time.deltaTime);
    }

    protected void SetAnimatorData()
    {
        animator.SetFloat("Speed", velocity.magnitude);
        animator.SetInteger("Playersta", PlayerSta);
    }

    protected bool CheckAnimationEND(string str)
    {
        
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if(info.IsName(str) && info.normalizedTime > 1.0f)
        {
            Vector3 bodypos = Model.transform.position;
            bodypos.y = InitY;
            transform.position = bodypos;
            
            return true;
        }
        else
        {
            return false;
        }
    }

	protected bool CheckAnimationENDforRate(string str ,float EndRate) {

		AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
		if (info.IsName(str) && info.normalizedTime > EndRate) {
			Vector3 bodypos = Model.transform.position;
			bodypos.y = InitY;
			transform.position = bodypos;

			return true;
		}
		else {
			return false;
		}
	}

	protected bool CheckAnimationSTART(string str)
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (info.IsName(str) && info.normalizedTime < 0.1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void ModelTransformReset()
    {
        Vector3 v = new Vector3(transform.position.x, 0.0f, transform.position.z);

        Model.position = v;
        Model.rotation = new Quaternion(0, 0, 0, 0);
    }

    protected void NoDameCnt()
    {
        if (nodamageflg)
        {
            nodamtotal += Time.deltaTime;

            if (nodamtotal >= nodamagetime)
            {
                nodamtotal = 0;
                nodamagetime = cpynodammax;
                nodamageflg = false;
            }
        }
    }

    public void NoDamageON(float time)
    {
        nodamagetime = time;
        nodamageflg = true;
    }

    protected float GetAninormalizedTime(string str)
    {
        float time = 0;

        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (info.IsName(str))
        {
            time = info.normalizedTime;
        }

        return time;
    }
}
