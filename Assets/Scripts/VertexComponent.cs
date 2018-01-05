using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VertexComponent : MonoBehaviour
{
    void Start()
    {
    }

    public Vector3[] GetVertexArray()
    {
        var child = gameObject.GetComponentsInChildren<VertexComponent>();
        if (child == null || child.Length == 0)
        {
            return new Vector3[] {transform.position};
        }
        return child.Select(x => x.transform.position).ToArray();
    }
}
