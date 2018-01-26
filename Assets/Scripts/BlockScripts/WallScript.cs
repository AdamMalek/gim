using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
	Material material;
	public void Awake(){
		material = gameObject.GetComponent<MeshRenderer>().material;
	}
    public void Show()
    {
		material.color = Color.white;
    }
    public void Hide()
    {
		  material.color = new Color(1,1,1,0.0f);
    }
}