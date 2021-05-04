using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeholderBoss : Enemy
{
    public Rigidbody2D bossRb;
    public Transform target;
    //public float chaseRadius;
    public float attackRadius;
    public Animator anim;
    private SpriteRenderer myRenderer;

    //patroling
    public Transform[] path;
    private int randomPoints;
    public Transform currentGoal;
    public float roundingDistance;

    //Projectile
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        bossRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = FindObjectOfType<PlayerMovement>().transform;
        myRenderer = GetComponent<SpriteRenderer>();

        //target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;
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
                changeAnim(temp - transform.position);
                //Vector3 tempVector = target.transform.position - transform.position;
                if (canFire)
                {
                    StartCoroutine(Shoot());
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
            }
            //}
        }
    }

    private void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }
    public void changeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }
            else if (direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }
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
        randomPoints = Random.Range(0, path.Length);
        currentGoal = path[randomPoints];

    }

    private IEnumerator Shoot()
    {
        myRenderer.color = Color.red;
        yield return new WaitForSeconds(.7f);
        myRenderer.color = Color.white;
        Vector3 tempVector = target.transform.position - transform.position;
        GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
        current.GetComponent<Projectile>().Launch(tempVector);
    }
}
