using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : log
{
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;

    private SpriteRenderer myRenderer;

    public override void Awake()
    {
        base.Awake();
        myRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Start()
    {
        base.Start();
    }
    private void Update()
    {
        fireDelaySeconds -= Time.deltaTime;
        if(fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                if (canFire)
                {
                    ChangeState(EnemyState.walk);
                    anim.SetBool("wakeUp", true);
                    StartCoroutine(Shoot());
                    canFire = false;
                }
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("wakeUp", false);
        }
    }

    public IEnumerator Shoot()
    {
        myRenderer.color = Color.red;
        yield return new WaitForSeconds(.7f);
        myRenderer.color = Color.white;
        Vector2 tempVector = target.transform.position - transform.position;
        GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
        current.GetComponent<Projectile>().Launch(tempVector.normalized);
    }

   
}
