using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] GameObject deathEffect;
    [SerializeField] EventMessage roomMessage;
    public AudioClip hurtNoise;
    private AudioSource audioSource;

    public LootTable thisLoot;

    public void Awake()
    {
        deathEffect = Resources.Load<GameObject>("Prefabs/Effects/EnemyDeath");
        //roomMessage = Resources.Load<EventMessage>("ScriptableObjects/Enemy/RoomMessage");
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SetHealth(maxHealth);
        //deathEvent += Death();
    }
    public override void Damage(int amountToDamage)
    {
        base.Damage(amountToDamage);

        if (hurtNoise != null)
        {
            audioSource.clip = hurtNoise;
            audioSource.Play();
        }

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    private void DropLoot()
    {
        if (thisLoot != null)
        {
            powerup currant = thisLoot.LootPowerup();
            if (currant != null)
            {
                Instantiate(currant.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

    public override void Kill()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
        if (roomMessage != null)
        {
            roomMessage.Raise();
        }
        DropLoot();
        base.Kill();
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float HealthRatio()
    {
        return (float)currentHealth / (float)maxHealth;
    }
}
