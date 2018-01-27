using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : FallScript
{
    public void Show()
    {
        enabled = true;
    }
    public void Hide()
    {
        enabled = false;
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
