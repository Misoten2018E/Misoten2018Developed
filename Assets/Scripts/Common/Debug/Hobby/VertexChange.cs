using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class VertexChange : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private MeshTopology MeshType = MeshTopology.Triangles;


	//========================================================================================
	//                                    メンバ
	//========================================================================================


	MeshFilter _meshFilter;
	public MeshFilter meshFilter {
		private set { _meshFilter = value; }
		get {
			if (_meshFilter == null) {
				_meshFilter = GetComponent<MeshFilter>();
			}
			return _meshFilter;
		}
	}
      

	// Use this for initialization
	void Start () {

		meshFilter.mesh.MarkDynamic();
		ChangeMeshType(MeshType);

	}

	public void ChangeMeshType(MeshTopology type) {

		MeshType = type;
		VertexChanger.VertexChange(meshFilter, MeshType);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


public class VertexChanger {

	static public void VertexChange(MeshFilter filter, MeshTopology type) {

		filter.mesh.SetIndices(filter.mesh.GetIndices(0), type, 0);

	}

}