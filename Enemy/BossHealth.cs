using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : Health
{
    [SerializeField] GameObject deathEffect;
    [SerializeField] EventMessage roomMessage;
    public Animator bossAnim;
    public Enemy bossScript;

    public void Awake()
    {
        deathEffect = Resources.Load<GameObject>("Prefabs/Effects/EnemyDeath");
        roomMessage = Resources.Load<EventMessage>("ScriptableObjects/Enemy/RoomMessage");
    }

    private void Start()
    {

    }
    public override void Damage(int amountToDamage)
    {
        base.Damage(amountToDamage);
        if (currentHealth <= 0)
        {
            Death();
        }

        if (currentHealth == 5)
        {
            bossAnim.SetBool("isAngry", true);
            bossScript.moveSpeed = bossScript.moveSpeed * 2;

            if (this.GetComponentInParent<Enemy8WayProj>() != null)
            {
                this.GetComponentInParent<Enemy8WayProj>().numberOfProjectiles = 15;
            }
        }
    }

    void Death()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
        roomMessage.Raise();
        this.transform.parent.gameObject.SetActive(false);
    }
}
