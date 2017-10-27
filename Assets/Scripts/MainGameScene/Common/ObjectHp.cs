using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHp : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private int CharacterHp = 100;

	//========================================================================================
	//                                    public - override
	//========================================================================================

	private void Start() {

		Hp = CharacterHp;
	}

	//========================================================================================
	//                                    public
	//========================================================================================

	int _hp;
	public int Hp {
		protected set { _hp = value; }
		get { return _hp; }
	}

	public int MaxHp {
		get { return CharacterHp; }
	}

	public bool isDeath { get { return _hp <= 0; } }

	public void Damage(int damage) {
		Hp -= damage;
	}

	public void Heal(int heal) {
		Hp += heal;

		if (Hp > MaxHp) {
			Hp = MaxHp;
		}
	}
}
