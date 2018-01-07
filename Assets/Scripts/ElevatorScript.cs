using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Constraints;
using UnityEditor;
using UnityEngine;
using UnityEngineInternal;

public class ElevatorScript : MonoBehaviour
{
    public float TargetHeight = 2;
    public float ElevationTime;

    [SerializeField]
    private bool running = false;

    private Vector3 initPos;
    private Vector3 targetPos;
    private float curr = 0f;
    private bool up;
    private bool canStart = true;
    private Collider trigger;
    private GameManager gm;
    public void Start()
    {
        gm = FindObjectOfType<GameManager>();
        initPos = transform.position;
        targetPos = new Vector3(initPos.x, TargetHeight, initPos.z);
        up = TargetHeight > transform.position.y;
        trigger = GetComponents<Collider>().Last();
    }

    public void OnTriggerStay(Collider other)
    {
        Debug.DrawLine(other.bounds.min,other.bounds.max);
        if (canStart && !running && trigger.bounds.Contains(other.bounds.max) && trigger.bounds.Contains(other.bounds.min))
        {
            StartElevator();
            canStart = false;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        canStart = true;
    }

    public void StartElevator()
    {
        running = true;
        GetComponentsInChildren<Collider>().Where(x=> x.gameObject.tag == "ElevatorBounds").ToList().ForEach(x=> x.enabled = true);
        if (up)
        {
            gm.ShowLevel((int)targetPos.y/2);
        }
        else
        {
            gm.ShowLevel((int)initPos.y/2);
        }
    }

    public void StopElevator()
    {
        running = false;
        GetComponentsInChildren<Collider>().Where(x => x.gameObject.tag == "ElevatorBounds").ToList().ForEach(x => x.enabled = false);
        if (up)
        {
            gm.HideLevel((int)targetPos.y / 2);
        }
    }

    public void Update()
    {
        if (running)
        {
            curr += Time.deltaTime;
            Vector3 pos;
            if (up)
            {
                pos = Vector3.Lerp(initPos, targetPos, curr/ElevationTime);
            }
            else
            {
                pos = Vector3.Lerp(targetPos, initPos, curr/ElevationTime);
            }
            transform.SetPositionAndRotation(pos,Quaternion.identity);
        }
        if ((up && transform.position.y >= targetPos.y) || (!up && transform.position.y <= initPos.y))
        {
            curr = 0;
            up = !up;
            StopElevator();
        }
    }
}
