using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpLevelBoss : Enemy
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

    //Projectile
    public GameObject projectile;
    //public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;
    public GameObject mole;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        bossRb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerMovement>().transform;
        myRenderer = GetComponent<SpriteRenderer>();
        mole.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        randomSecs = Random.Range(3, 7);
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

        mole.SetActive(true);
        bossRb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(.86f);
        Vector3 tempVector = target.transform.position - transform.position;
        GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
        current.GetComponent<Projectile>().Launch(tempVector);
        yield return new WaitForSeconds(0.5f);
        mole.SetActive(false);
        bossRb.constraints = RigidbodyConstraints2D.None;

    }

}
