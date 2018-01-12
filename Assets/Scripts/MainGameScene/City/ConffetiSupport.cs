using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConffetiSupport : MonoBehaviour {


	//========================================================================================
	//                                    serialize
	//========================================================================================

	//========================================================================================
	//                                    public
	//========================================================================================

	public void CreateParticle() {

		var par = Instantiate(ConffetiPrefab);
		par.transform.position = RandomArea();
	}


	// シングルトンインスタンス
	static ConffetiSupport myInstance;
	static public ConffetiSupport Instance {
		get {
			return myInstance;
		}
	}


	//========================================================================================
	//                                    private
	//========================================================================================

	// Use this for initialization
	void Start() {

		myInstance = this;
		var p = ConffetiPrefab;

		var poslist = GetComponentsInChildren<Transform>();
		PositionList.AddRange(poslist);
	}


	Vector3 RandomVector() {
		var v = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		return v.normalized;
	}


	Vector3 RandomArea() {

		int index = Random.Range(0, PositionList.Count);
		return PositionList[index].transform.position;
	}


	ParticleSupport _ConffetiPrefab;
	public ParticleSupport ConffetiPrefab {
		get {
			if (_ConffetiPrefab == null) {
				_ConffetiPrefab = ResourceManager.Instance.Get<ParticleSupport>(ConstDirectry.DirParticleEdo, ConstEffects.Conffeti);
			}
			return _ConffetiPrefab;
		}
	}


	List<Transform> PositionList = new List<Transform>();
}
