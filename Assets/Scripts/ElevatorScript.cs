using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Constraints;
using UnityEditor;
using UnityEngine;
using UnityEngineInternal;

public class ElevatorScript : FallScript
{
    public float TargetHeight = 2;
    public float ElevationTime;
    public float speed;
    public Color targetColor;

    public bool OneWay = true;
    
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
        if (other.tag != "Player") return;         
        if (canStart && !running && trigger.bounds.Contains(other.bounds.max) && trigger.bounds.Contains(other.bounds.min))
        {
            StartElevator();
            canStart = false;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") return;  
        canStart = (OneWay && transform.position == initPos) || !OneWay;
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
        if (canStart)
        {
            var dt = (Mathf.Sin(speed * Time.time) + 1)/2;
            gameObject.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.white, targetColor, dt);
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        if ((up && transform.position.y >= targetPos.y) || (!up && transform.position.y <= initPos.y))
        {
            curr = 0;
            up = !up;
            StopElevator();
        }
    }
    public override void EnableFalling()
    {
        StopElevator();
        base.EnableFalling();
    }
}
