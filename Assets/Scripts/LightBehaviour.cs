using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehaviour : MonoBehaviour
{
    public Transform target;
    float delta = 0.2f;
    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            delta+=0.1f;
            Debug.Log(delta);
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            delta -= 0.1f;
            Debug.Log(delta);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            pos.y+=delta;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            pos.y -= delta;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            pos.x += delta;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            pos.x -= delta;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            pos.z += delta;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            pos.z -= delta;
        }
        transform.SetPositionAndRotation(pos,transform.rotation);
        if (target != null)
        {
            transform.LookAt(target);
        }
    }
}
