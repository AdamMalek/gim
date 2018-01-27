using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FallScript : MonoBehaviour {
	public virtual void EnableFalling(){
		var rigid = gameObject.GetComponent<Rigidbody>();
		if (rigid != null){
			rigid.isKinematic = false;
			rigid.AddExplosionForce(10, new Vector3(5,5,5),30, 0.5f, ForceMode.Force);
		}
	}
}
