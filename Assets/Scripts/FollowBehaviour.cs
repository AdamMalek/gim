using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

public class FollowBehaviour : MonoBehaviour
{
    public Transform target;
    public float CameraDistance = 1f;

    private Vector3 offset;

    private List<GameObject> hidden;
    // Use this for initialization
    void Start()
    {
        var pos = target.position;

        var r = new Ray(transform.position, transform.forward);
        var end = r.GetPoint(CameraDistance);
        target.position = end;
        offset = target.transform.position - transform.position;
        target.position = pos;

        hidden = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        var pos = target.position;
        transform.SetPositionAndRotation(pos - offset, transform.rotation);

        foreach (var toHide in hidden)
        {
            toHide.GetComponent<MeshRenderer>().enabled = true;
        }
        hidden.Clear();
        
        //var vertexes = target.GetComponent<VertexComponent>();
        //foreach (var v in vertexes.GetVertexArray())
        //{
        //    RaycastHit hit;
        //    if (Physics.Linecast(transform.position, v, out hit))
        //    {
        //        var h = false;
        //        try
        //        {
        //            h = null == hit.transform.gameObject.GetComponent<VertexComponent>();
        //        }
        //        catch (Exception e)
        //        {
        //            h = false;
        //        }
        //        if (h)
        //        {
        //            hidden.Add(hit.transform.gameObject);
        //            //hit.transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
        //            Debug.DrawLine(transform.position, hit.point, Color.red);
        //        }
        //        foreach (var toHide in hidden)
        //        {
        //            toHide.GetComponent<MeshRenderer>().enabled = false;
        //        }
        //    }
        //}

        var vertexes = target.GetComponent<VertexComponent>();
        foreach (var v in vertexes.GetVertexArray())
        {
            var dir = v - transform.position;
            var ray = new Ray(transform.position, dir);
            //Debug.DrawRay(transform.position, dir);
            var res = Physics.RaycastAll(ray,Vector3.Distance(transform.position,v));
            foreach (var hit in res)
            {
                var h = false;
                try
                {
                    h = null == hit.transform.gameObject.GetComponent<VertexComponent>();
                }
                catch (Exception e)
                {
                    h = false;
                }
                if (h)
                {
                    hidden.Add(hit.transform.gameObject);
                    //hit.transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    //Debug.DrawLine(transform.position, hit.point, Color.red);
                }
                foreach (var toHide in hidden)
                {
                    toHide.GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }
    }
}
