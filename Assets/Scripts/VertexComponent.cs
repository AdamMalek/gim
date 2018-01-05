using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VertexComponent : MonoBehaviour
{
    public Transform[] Vertices;

    public Vector3[] GetVertexArray()
    {
        if (Vertices == null || Vertices.Length == 0)
        {
            return new[] {transform.position};
        }
        return Vertices.Select(x => x.transform.position).ToArray();
    }
}
