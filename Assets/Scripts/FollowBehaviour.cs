using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

public class FollowBehaviour : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    private List<GameObject> hidden;
    void Start()
    {
        var pos = target.position;
        var r = new Ray(transform.position, transform.forward);
        var end = r.GetPoint(20);
        target.position = end;
        offset = target.transform.position - transform.position;
        target.position = pos;

        hidden = new List<GameObject>();
    }

    void Update()
    {
        var pos = target.position;
        transform.SetPositionAndRotation(pos - offset, transform.rotation);

        foreach (var toShow in hidden)
        {
            ShowObject(toShow);
        }
        hidden.Clear();

        var vertexes = target.GetComponent<VertexComponent>();
        foreach (var v in vertexes.GetVertexArray())
        {
            var ray = new Ray(v, -offset);
            Debug.DrawRay(v, -offset, Color.red);
            var res = Physics.RaycastAll(ray,Vector3.Distance(transform.position,v));
            foreach (var hit in res)
            {
                var h = false;
                try
                {
                    h = null == hit.transform.gameObject.GetComponent<VertexComponent>();
                }
                catch (Exception)
                {
                    h = false;
                }
                if (h)
                {
                    hidden.Add(hit.transform.gameObject);
                }
                foreach (var toHide in hidden)
                {
                    hideObject(toHide);
                }
            }
        }
    }

    void hideObject(GameObject toHide)
    {
        WallScript block = null;
        if (toHide != null)
        {
            block = toHide.GetComponent<WallScript>();
        }
        if (block != null)
        {
            block.Hide();
        }
    }

    void ShowObject(GameObject toShow)
    {
        WallScript block = null;
        if (toShow != null)
        {
            block = toShow.GetComponent<WallScript>();
        }
        if (block != null)
        {
            block.Show();
        }
    }
}
