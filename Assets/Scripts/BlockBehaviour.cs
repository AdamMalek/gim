using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockBehaviour : MonoBehaviour {

    public bool wallsVisible = true;
    public bool floorVisible = true;
    public bool Collisions = true;

    private Material brickMaterial = null;
    private Material currentFloorMaterial = null;
    private Material floorMaterial = null;
    private Material transparencyMaterial = null;
    public void Init(Material transparencyMaterial)
    {
        this.transparencyMaterial = transparencyMaterial;
        var materials = gameObject.GetComponent<MeshRenderer>().materials;
        brickMaterial = materials[0];
        floorMaterial = materials[1];
        setWallsVisibility(wallsVisible);
        setFloorVisibility(floorVisible);
    }
    public void setCollisions(bool collision)
    {
        gameObject.GetComponents<BoxCollider>().ToList().ForEach(x =>
        {
            x.enabled = collision;
        });
        Collisions = collision;
    }
    
    public void setWallsVisibility(bool visible)
    {
        if (wallsVisible != visible)
        {
            var alpha = 1f;
            if (!visible)
            {
                alpha = 0.0f;
            }
            var c = brickMaterial.color;
            brickMaterial.color = new Color(c.r, c.g, c.b, alpha);
            wallsVisible = visible;
        }
    }

    public void setFloorVisibility(bool visible)
    {
        if (floorVisible != visible)
        {
            var mat = floorMaterial;
            if (!visible) mat = transparencyMaterial;
            var mats = GetComponent<MeshRenderer>().materials;
            mats[1] = mat;
            GetComponent<MeshRenderer>().materials = mats;
        }
    }
}
