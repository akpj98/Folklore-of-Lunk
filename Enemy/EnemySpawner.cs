using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    /// <summary>
    /// Put this script on a empty gameobject and put the enemies as children under the gameobject
    /// </summary>
   

    [SerializeField] Enemy[] enemies;

    private void Awake()
    {
        enemies = GetComponentsInChildren<Enemy>();
    }

    void Start()
    {
        SetEnemiesInactive();
    }

    void SetEnemiesInactive()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = enemies.Length - 1; i >= 0; i--)
            {
                enemies[i].gameObject.SetActive(true);
            }
        }
    }
}
