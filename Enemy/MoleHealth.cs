using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleHealth : Health
{
    [SerializeField] GameObject deathEffect;
    [SerializeField] EventMessage roomMessage;
    public Animator bossAnim;
    public Enemy bossScript;
    private WarpLevelBoss moleScript;

    public void Awake()
    {
        deathEffect = Resources.Load<GameObject>("Prefabs/Effects/EnemyDeath");
        roomMessage = Resources.Load<EventMessage>("ScriptableObjects/Enemy/RoomMessage");
    }

    private void Start()
    {
        moleScript = GetComponentInParent<WarpLevelBoss>();
    }
    public override void Damage(int amountToDamage)
    {
        base.Damage(amountToDamage);
        if (currentHealth <= 0)
        {
            Death();
        }

        if (currentHealth <= 5)
        {
            bossAnim.SetBool("isAngry", true);
            bossScript.moveSpeed = 5.0f;
            moleScript.projectile.GetComponent<Projectile>().moveSpeed = 20f;
        }
    }

    void Death()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
        roomMessage.Raise();
        this.transform.parent.gameObject.SetActive(false);
    }
}
