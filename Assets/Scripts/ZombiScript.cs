using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour {

    Animator Zombianim;
    public Transform Hedef;
    NavMeshAgent Agent;

    public float mesafe;

    // Use this for initialization
    void Start()
    {
        Zombianim=GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();


        // Update is called once per frame
    void Update()
    {
        Zombianim.SetFloat("hiz",Agent.velocity.magnitude);


         mesafe = Vector3.Distance(transform.position, Hedef.position);




        if (mesafe <= 10)
        {
            Agent.enabled=true;
            Agent.destination = Hedef.position;
        }
        else
        {
            Agent.enabled=false;
        }
        }
    }
}