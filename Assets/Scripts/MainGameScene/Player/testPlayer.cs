using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayer : MonoBehaviour {

	MultiInput input;
	public int no;
	public float MoveSpeed;
	public float RotationSpeed;

	CharacterController CharCon;
	Vector3 velocity;

	// Use this for initialization
	void Start() {
		input = this.GetComponent<MultiInput>();
		CharCon = this.GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update() {
		if (input.GetXaxis() != 0) {
			print("X" + no + input.GetXaxis());
		}
		if (input.GetYaxis() != 0) {
			print("Y" + no + input.GetYaxis());
		}

		velocity.x = input.GetXaxis() * MoveSpeed;
		velocity.z = input.GetYaxis() * MoveSpeed;

		Vector3 direction = new Vector3(velocity.x, 0, velocity.z);
		if (direction.sqrMagnitude > 0f) {
			Vector3 forward = Vector3.Slerp(transform.forward, direction, RotationSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction));
			transform.LookAt(transform.position + forward);
		}

		CharCon.Move(velocity * Time.deltaTime);

		if (input.GetButtonCircleTrigger()) {
			print("ButtonCircleTrigger" + no);
		}

		if (input.GetButtonSquareTrigger()) {
			print("GetButtonSquareTrigger" + no);
		}

		if (input.GetButtonTriangleTrigger()) {
			print("GetButtonTriangleTrigger" + no);
		}

		if (input.GetButtonCrossTrigger()) {
			print("GetButtonCrossTrigger" + no);
		}
	}
}
