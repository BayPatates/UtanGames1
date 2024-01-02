using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Takip : MonoBehaviour {
    
    NavMeshAgent agent;
    
    public Transform Target;
    
    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update () {
        agent.destination = Target.position;
    }
}