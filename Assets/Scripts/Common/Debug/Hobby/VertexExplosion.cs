using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VertexChange))]
public class VertexExplosion : MonoBehaviour {


	VertexChange _vertChange;
	public VertexChange vertChange {
		get {
			if (_vertChange == null) {
				_vertChange = GetComponent<VertexChange>();
			}
			return _vertChange;
		}
	}


	MeshRenderer _meshRen;
	public MeshRenderer meshRen {
		get {
			if (_meshRen == null) {
				_meshRen = GetComponent<MeshRenderer>();
			}
			return _meshRen;
		}
	}
      

	// Use this for initialization
	void Start () {
		var ren = meshRen;
		var v = vertChange;
	}

	float expTime;

	// Update is called once per frame
	void Update () {

		expTime += Time.deltaTime;
		meshRen.material.SetFloat("_ExplosionTime", expTime);

	}
}
