using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : log
{
    private AudioClip swordClip;
    private SpriteRenderer myRenderer;

    public override void Awake()
    {
        base.Awake();
        //anim = GetComponent<Animator>();
        //myRb = GetComponent<Rigidbody2D>();
        myRenderer = GetComponent<SpriteRenderer>();
        swordClip = Resources.Load<AudioClip>("AudioClips/schwing");
        target = FindObjectOfType<PlayerMovement>().transform;
    }



    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                myRb.MovePosition(temp);
                ChangeState(EnemyState.walk);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) <= attackRadius)
        {
            if (currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                StartCoroutine(AttackCo());
            }
        }
    }

    public IEnumerator AttackCo()
    {
        currentState = EnemyState.attack;
        myRenderer.color = Color.red;
        yield return new WaitForSeconds(.3f);
        anim.SetBool("attack", true);
        yield return new WaitForSeconds(1f);
        myRenderer.color = Color.white;
        currentState = EnemyState.walk;
        anim.SetBool("attack", false);
    }

    public void Schwing()
    {
        audioSource.clip = swordClip;
        audioSource.PlayOneShot(swordClip, 0.7f);
    }
}