using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour {

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

    protected float MoveSpeed;
    protected float RotationSpeed;
    protected Vector3 velocity;

    protected Animator animator;

    protected int PlayerSta;
    protected bool aniendFlg;

    protected int HP;
    protected int Attack;

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

    public void ChangeAttack(int attack)
    {
        Attack += attack;
    }

    public virtual void Playerinit(int playerno)
    {

    }

    public virtual void PlayerUpdate()
    {
     
    }

    public virtual void PlayerDamage()
    {

    }

    public Vector3 GetBodyPosition()
    {
        return animator.bodyPosition;
    }

    public bool PlayerIsLive()
    {
        bool res = true;

        if (HP <= 0)
        {
            res = false;
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
            bodypos.y = transform.position.y;
            transform.position = bodypos;
            return true;
        }
        else
        {
            return false;
        }
    }
}
