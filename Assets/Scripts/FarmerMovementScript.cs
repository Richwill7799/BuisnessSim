using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerMovementScript : MonoBehaviour
{

    public float speed, dodgeSpeed;
    public float minDistance, maxDistance; 

    public float minWaitTime, maxWaitTime;

    //TODO delete after it all works, debug
    public Transform helper;

    private Vector3 goal;
    private float walkUntil;

    private float waitEndTime;

    private Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Start()
    {
        waitEndTime = 0f;
        rigidbody2d = GetComponent<Rigidbody2D>();
        pickGoal(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Time.time >waitEndTime){
            //keep goaldirection normalized
            if(Vector3.Distance(goal, transform.position)>1f||Vector3.Distance(goal,transform.position)<1f)
                goal = Vector3.Normalize(goal);

            //calculate progress in movement
            float progress = speed*Time.deltaTime/Vector3.Distance(goal,transform.position);
            
            //move farmer
            rigidbody2d.MovePosition(Vector3.Lerp(transform.position,transform.position +goal,progress));
            
            //check if farmer is done with movement
            if(walkUntil<Time.time){
                waitEndTime = Time.time+Random.Range(minWaitTime, maxWaitTime);
                pickGoal(true);
            }
        }

        //TODO delete later, debug
        helper.position = goal+transform.position;
    }

    private void pickGoal(bool waiting){
        //pick a new goal direction at random and determine how long to walk
        float x = Random.Range(-1f,1f);
        float y = Mathf.Cos(x*(Mathf.PI/2));
        if (Random.Range(0f, 1f) < 0.5f)
            y *= -1f;
        goal = new Vector3(x,y,0);
        if (waiting) 
            walkUntil = waitEndTime + Random.Range(minDistance,maxDistance);
        else 
            walkUntil = Time.time + Random.Range(minDistance,maxDistance);
    }

    public void OnTriggerStay2D(Collider2D other){
        //continue changing direction if others stay too close
        goal -= (other.transform.position-transform.position)*(1/Vector3.Distance(other.transform.position,transform.position))*Time.deltaTime*dodgeSpeed;
    }

    public void OnTriggerEnter2D(Collider2D other){
        //change direction when another farmer comes too close
        pickGoal(false);
        goal -= other.transform.position - transform.position;
        goal = Vector3.Normalize(goal);
    }
}