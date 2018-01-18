using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    public void Show()
    {
        enabled = true;
    }
    public void Hide()
    {
        enabled = false;
    }

    public void EnableFalling()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
