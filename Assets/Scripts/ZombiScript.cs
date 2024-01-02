using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class ZombieScript : MonoBehaviour {

    Animator Zombianim;
    public Transform Hedef;
    NavMeshAgent Agent;

    public float mesafe;
    public float health;
    public static float score=0;
    bool isDeath=false;

    void death(){
        if(isDeath)
        {
            return;       
        }
        score++;
        print(score);
        Destroy(gameObject,7f);
        isDeath=true;
    }
    // Use this for initialization
    void Start()
    {
        Zombianim=GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();


    }    // Update is called once per frame
    void Update()
    {
        Zombianim.SetFloat("hız",Agent.velocity.magnitude);
        Zombianim.SetFloat("can",health);

        mesafe = Vector3.Distance(transform.position, Hedef.position);



        if(health>0)
        { 
        if (mesafe <= 1000)
        {
            Agent.enabled=true;
            Agent.destination = Hedef.position;
        }
        else
        {
            Agent.enabled=false;
        }
        }
        else
        {
            death();
           // score=score+1;
           // Destroy(gameObject,7f);
        }

     
    if(score>19)
    {
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
    SceneManager.LoadScene(3);

     // Load the next scene
    }    

    }

}
