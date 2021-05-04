using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoss : Enemy
{
    public Rigidbody2D bossRb;
    public Transform target;
    //public float chaseRadius;
    public float attackRadius;
    private SpriteRenderer myRenderer;

    //patroling
    public Transform[] path;
    private int randomPoints;
    private float randomSecs;
    public Transform currentGoal;
    public float roundingDistance;

    //This stuff here is where I shove the 8 way variables
    public GameObject enemybullet;
    private double radius = 5;
    private double pointx, pointy;
    private double angle, angle1, angle2;
    private Vector2 projposition;
    private float speed = 3;
    private float amtToMove;
    private Transform myTransform;

    //other projectile stuff
    //public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;
    public Enemy8WayProj ifuckingSwear;


    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        bossRb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerMovement>().transform;
        myRenderer = GetComponent<SpriteRenderer>();
        ifuckingSwear = GetComponent<Enemy8WayProj>();

        ifuckingSwear.enabled = false;

        //8way 
        angle = 0;
        amtToMove = speed * Time.deltaTime;
        myTransform = transform;

    }

    // Update is called once per frame
    void Update()
    {
        randomSecs = UnityEngine.Random.Range(3, 7);
        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = randomSecs;
        }
    }

    void FixedUpdate()
    {
        CheckDistance();
    }

    public virtual void CheckDistance()
    {
        //if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        //{
        if ((currentState == EnemyState.idle) || (currentState == EnemyState.walk && currentState != EnemyState.stagger))
        {
            if (Vector3.Distance(transform.position, path[randomPoints].position) > roundingDistance && Vector3.Distance(target.position, transform.position) > attackRadius)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, path[randomPoints].position, moveSpeed * Time.deltaTime);
                //Vector3 tempVector = target.transform.position - transform.position;
                if (canFire)
                {
                    ifuckingSwear.enabled = true;
                    //Vector3 tempVector = target.transform.position - transform.position;
                    //GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                    //current.GetComponent<Projectile>().Launch(tempVector);
                    canFire = false;
                }

                bossRb.MovePosition(temp);
            }

            else
            {
                ChangeGoal();
                ifuckingSwear.enabled = false;
            }
            //}
        }
    }



    public void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    private void ChangeGoal()
    {
        randomPoints = UnityEngine.Random.Range(0, path.Length);
        currentGoal = path[randomPoints];

    }

    //private double DegreeToRadian(double a)
    //{
    //    double answer;
    //    answer = Math.PI * (a / 180.0);
    //    return answer;
    //}

    // IEnumerator drawcircle()
    //{
    //    for (int i = 0; i < 360; i += 18)
    //    {
    //        angle1 = Math.Cos(DegreeToRadian(angle + i));
    //        angle2 = Math.Sin(DegreeToRadian(angle + i));
    //        pointx = radius * angle1 + myTransform.position.x;
    //        pointy = radius * angle2 + myTransform.position.y;
    //        projposition = new Vector3((float)(pointx), (float)(pointy), 0);
    //        GameObject current = Instantiate(enemybullet, projposition, Quaternion.identity);
    //        current.GetComponent<AnotherProjectileScript>().is

    //    }
    //    yield return new WaitForSeconds(0.35f);
    //}
}
