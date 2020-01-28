using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
//[System.Serializable]
public class FarmerMovementScript : MonoBehaviour
{
    
    //public Simulation s;
    public float speed, dodgeSpeed;
    public float minDistance, maxDistance; 

    public float minWaitTime, maxWaitTime;

    public int team; //0 for no team, >=1 for a team
    public Transform teamTransform;
    public int radius;
    public int id;
    public GameObject nameMe;
    public GameObject myCoat; //The color of the coat
    //public GameObject makeItStop; //Where is my Animation?
    //TODO delete after it all works, debug
    public Transform helper;

    private Vector3 goal;
    private float walkUntil;

    private float waitEndTime;

    private bool towardsTeam;
    
    private Rigidbody2D rigidbody2d;

    private List<Farmer> farmers;
    private Animator animator;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponentInChildren<Animator>();
        waitEndTime = 0f;
        rigidbody2d = GetComponent<Rigidbody2D>();
        pickGoal(false);
        GameObject userInput = GameObject.FindGameObjectWithTag("Information");
        farmers = userInput.GetComponent<HandleInput>().GetFarmers();
        nameMe.GetComponent<TextMesh>().text = farmers[id].name;
        //Maybe here finally coloring
        if (team == 0)
        {
            nameMe.GetComponent<TextMesh>().color = farmers[id].color;
        }
        else
        {
            nameMe.GetComponent<TextMesh>().color = new Color(0.116f, 0.624f, 0.467f);
        }
        myCoat.GetComponent<SpriteRenderer>().color = farmers[id].color;
    }

    public void SetTowardsTeam(bool towardsTeamNew){
        towardsTeam = towardsTeamNew;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Time.time >waitEndTime){
            
            //team direction if the team is too far away
            if (team!=0&&towardsTeam) {
                goal += (dodgeSpeed)*(1/Vector3.Distance(teamTransform.position,transform.position))*Time.deltaTime*(teamTransform.position-transform.position);
            }
            
            //keep goaldirection normalized
            if(Vector3.Distance(goal, transform.position)>1f||Vector3.Distance(goal,transform.position)<1f)
                goal = Vector3.Normalize(goal);

            //calculate progress in movement
            float progress = speed*Time.deltaTime/Vector3.Distance(transform.position+goal,transform.position);
            
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
        nameMe.transform.position = transform.position + new Vector3(-0.3f,1f,0f);

        //TODO Make it stop! Don't forget to delete the = in the second if!

        if((goal+transform.position).x < transform.position.x && myCoat.GetComponent<SpriteRenderer>().flipX == false)
        {
            //makeItStop.GetComponent<Animation>().Play();
            myCoat.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if((goal + transform.position).x >= transform.position.x && myCoat.GetComponent<SpriteRenderer>().flipX == true)
        {
            //makeItStop.GetComponent<Animation>().Play();
            myCoat.GetComponent<SpriteRenderer>().flipX = false;
        }
        /*else if((goal + transform.position).x == transform.position.x)
        {
            makeItStop.GetComponent<Animation>().Stop();
        }*/
        animator.SetBool("walk",Time.time>waitEndTime);
    }
  
    private void pickGoal(bool waiting){
        //pick a new goal direction at random and determine how long to walk
        float x = Random.Range(-1f,1f);
        float y = Mathf.Cos(x*(Mathf.PI/2));
        if (Random.Range(0f, 1f) < 0.5f)
            y *= -1f;
        goal = new Vector3(x,y,0);
        if (team!=0&&towardsTeam) { //Team handling
            goal += teamTransform.position - transform.position;
            goal = Vector3.Normalize(goal);
        }
        if (waiting) //walktime
            walkUntil = waitEndTime + Random.Range(minDistance,maxDistance);
        else 
            walkUntil = Time.time + Random.Range(minDistance,maxDistance);
    }

    public void OnTriggerStay2D(Collider2D other){
        //return if the collider is of the own team
        if (team != 0 && (other.gameObject.tag.Equals(team + "in") || other.gameObject.tag.Equals(team + "out")||other.gameObject.tag.Equals(team.ToString()))) {
            return;
        }

        if (other.tag.Equals("Wall")) {
            goal += (dodgeSpeed)*(1/transform.position.magnitude)*Time.deltaTime*(-transform.position);
            return;
        }
        //continue changing direction if others stay too close
        goal -= (dodgeSpeed)*(1/Vector3.Distance(other.transform.position,transform.position))*Time.deltaTime*(other.transform.position-transform.position);
    }

    public void OnTriggerEnter2D(Collider2D other){
        //return if the collider is of the own team
        if (team != 0 && other.gameObject.tag.Equals(team + "in") ) {
            towardsTeam = false;
            return;
        }

        if (team != 0 && (other.gameObject.tag.Equals(team + "out")||other.gameObject.tag.Equals(team.ToString())))
            return;

    
        //change direction when another farmer comes too close
        waitEndTime = Time.time+Random.Range(0f, 2f);
        pickGoal(true);
        if (other.gameObject.tag.Equals("Wall")) {
            goal -= transform.position/10;
        }
        else 
            goal -= other.transform.position - transform.position;
        goal = Vector3.Normalize(goal);
        
    }

    public void OnTriggerExit2D(Collider2D other){
        if (team == 0)
            return;
        if (other.gameObject.tag.Equals(team + "out")) {
            //left team zone, reorient
            waitEndTime = Time.time+Random.Range(0f,2f);
            pickGoal(true);
            goal += other.transform.position - transform.position;
            goal = Vector3.Normalize(goal);
        }

        if (other.gameObject.tag.Equals(team + "in")) {
            //approaching team zone end, soft direction change
            towardsTeam = true;
        }
    }
}