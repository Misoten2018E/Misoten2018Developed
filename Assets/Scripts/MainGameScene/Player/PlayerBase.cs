using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : SceneStartEvent{

    const int ChangeMoveSpeed = 5;

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
    public int nodamagetime;

    protected GameObject myPlayer;
    protected float MoveSpeed;
    protected float RotationSpeed;
    protected Vector3 velocity;

    protected Animator animator;
    protected Transform Model;

    protected int PlayerSta;

    protected ObjectHp HP;
    
    protected int Attack;
    protected bool nodamageflg;
    float nodamtotal = 0;

    // Use this for initialization
    void Start () {
		//使わない方向で
	}
	
	// Update is called once per frame
	void Update () {
		//使わない方向で
	}

    public void ForciblyMove(Vector3 moveposition)
    {
        Vector3 myposition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        velocity = moveposition - myposition;
        velocity.y = 0.0f;
        velocity.Normalize();
        velocity *= ChangeMoveSpeed;

        MoveCharacter();

        PlayerSta = 0;
        SetAnimatorData();
    }

    public void PlayerCityIn(Vector3 moveposition)
    {
        Vector3 myposition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        velocity = moveposition - myposition;
        velocity.y = 0.0f;
        velocity.Normalize();
        velocity *= ChangeMoveSpeed;
        
        MoveCharacter();

        PlayerSta = 0;
        SetAnimatorData();
    }

    public void PlayerCityOut(Vector3 moveposition)
    {
        Vector3 myposition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        velocity = moveposition - myposition;
        velocity.y = 0.0f;
        velocity.Normalize();
        velocity *= ChangeMoveSpeed;
        
        MoveCharacter();

        PlayerSta = 0;
        SetAnimatorData();
    }

    public void ChangeAttack(int attack)
    {
        Attack += attack;
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

    public virtual void PlayerPause()
    {

    }

    public Vector3 GetBodyPosition()
    {
        return animator.bodyPosition;
    }

    public GameObject GetPlayerObj()
    {
        return myPlayer;
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
            CharCon.center = new Vector3(0,10,0);
        }
        else
        {
            CharCon.center = new Vector3(0, 1, 0);
        }
        
    }

    public bool PlayerIsDeath()
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
            Vector3 bodypos = GetBodyPosition();
            bodypos.y = 0.0f;
            transform.position = bodypos;
            return true;
        }
        else
        {
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
                nodamageflg = false;
            }
        }
    }
}
