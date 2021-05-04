using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class log : Enemy
{
    //public Rigidbody2D logRb;
    public Transform homePosition;
    //public Animator anim;
    private AudioClip walkClip;
    private AudioClip sleepClip;
    private AudioClip sleepingClip;
    private AudioClip wakingClip;
    public EventMessage slainEnemy;

    [Header("Im on fire")]
    [SerializeField] protected GameObject activateFire;
    [SerializeField] float timeOnFire = 0f;
    [SerializeField] float maxTimeOnFire = 2.3f;
    [SerializeField] int fireDamageAmout = 1;
    [SerializeField] int randomFireChance = 50;
    [SerializeField] int randomFire;

    EnemyHealth myHealth;


    public override void Awake()
    {
        base.Awake();
        //myRb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        myHealth = GetComponentInChildren<EnemyHealth>();  
    }

    public virtual void Start()
    {
        timeOnFire = maxTimeOnFire;
        
        
        currentState = EnemyState.idle;
        target = FindObjectOfType<PlayerMovement>().transform;
        anim.SetBool("wakeUp", false);

        walkClip = Resources.Load<AudioClip>("AudioClips/boom");
        sleepClip = Resources.Load<AudioClip>("AudioClips/boom");
        sleepingClip = Resources.Load<AudioClip>("AudioClips/boom");
        wakingClip = Resources.Load<AudioClip>("AudioClips/boom");

        activateFire.SetActive(false);
    }

    void FixedUpdate()
    {
        CheckDistance();       
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        
        if (other.GetComponent<Fireball>())
        {
            if (gameObject.activeInHierarchy)
            {
                randomFire = Random.Range(0, 100);
                if (randomFire > randomFireChance)
                {
                    StartCoroutine(SetMeOnFire(timeOnFire, 2, fireDamageAmout));
                }
            }
        }
        foreach (Collider2D collider in colliders)
        {
            if (collider.isTrigger)
            {
                if ((other.GetComponent<Fireball>() || other.GetComponent<Arrow>() || other.GetComponent<Knockback>() ) && currentState != EnemyState.stagger)
                {
                    previousState = currentState;
                    currentState = EnemyState.stagger;
                    Invoke("NotStaggered", 1f);
                }
                return;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        
    }



    public virtual void CheckDistance()
    {
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if(currentState == EnemyState.idle || currentState == EnemyState.walk /*&& currentState != EnemyState.stagger*/)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                myRb.MovePosition(temp);
                ChangeState(EnemyState.walk);
                anim.SetBool("wakeUp", true);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("wakeUp", false);
        }
    }

    public void NotStaggered()
    {
        currentState = previousState;
    }

    /*private void SetAnimFloat(Vector2 setVector)
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
    }*/

    IEnumerator SetMeOnFire(float damageDuration, int damageCount, int damageAmount)
    {
        int currentCount = 0;
        activateFire.SetActive(true);
        while(currentCount < damageCount)
        {
            myHealth.Damage(damageAmount);
            yield return new WaitForSeconds(damageDuration);
            currentCount++;
        }
        activateFire.SetActive(false);
    }
    #region MOVEMENT
    public void Move()
    {
        audioSource.clip = walkClip;
        audioSource.Play();
    }
    public void WakeUp()
    {
        audioSource.clip = wakingClip;
        audioSource.Play();
    }
    public void Sleep()
    {
        audioSource.clip = sleepingClip;
        audioSource.Play();
    }
    public void GoingToSleep()
    {
        audioSource.clip = sleepClip;
        audioSource.Play();
    }
    #endregion
}
