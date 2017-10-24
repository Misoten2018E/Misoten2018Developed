using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour {

    const int ChangeMoveSpeed = 5;

    protected float MoveSpeed;
    protected float RotationSpeed;
    protected CharacterController CharCon;
    protected Vector3 velocity;

    protected Animator animator;

    protected int PlayerSta;
    protected Vector3 CharacterPosition;
    protected bool aniendFlg;

    // Use this for initialization
    void Start () {
		//使わない方向で
	}
	
	// Update is called once per frame
	void Update () {
		//使わない方向で
	}

    public void ChangeCharacterAction(Vector3 moveposition)
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

    public virtual void Playerinit(int playerno)
    {

    }

    public virtual void PlayerUpdate()
    {
     
    }

    public Vector3 GetCharacterPosition()
    {
        return CharacterPosition;
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

        CharacterPosition = transform.position;
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
        animator.SetFloat("Speed", CharCon.velocity.magnitude);
        animator.SetInteger("Playersta", PlayerSta);
    }

    protected bool CheckAnimationEND(string str)
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if(info.IsName(str) && info.normalizedTime > 1.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
