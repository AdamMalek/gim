using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockBehaviour : MonoBehaviour {

    public bool Visible = true;
    public bool Collisions = true;

    private Material material = null;
    private void Start()
    {
        material = gameObject.GetComponent<MeshRenderer>().material;
        setVisibility(Visible);
    }
    public void setCollisions(bool collision)
    {
        gameObject.GetComponents<BoxCollider>().ToList().ForEach(x =>
        {
            x.enabled = collision;
        });
        Collisions = collision;
    }
    
    public void setVisibility(bool visible)
    {
        if (Visible != visible)
        {
            var alpha = 1f;
            if (!visible)
            {
                alpha = 0.0f;
            }
            var c = material.color;
            material.color = new Color(c.r, c.g, c.b, alpha);
            Visible = visible;
        }
    }
}
