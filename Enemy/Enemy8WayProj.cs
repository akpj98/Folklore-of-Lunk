using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy8WayProj : MonoBehaviour
{
    
    public int numberOfProjectiles;
    public GameObject enemybullet;
    Vector2 startPoint;

    float radius, moveSpeed;



    //yeah
    private float fireDelaySeconds;
    public bool canFire = true;
    private float randomSecs;


    void Start()
    {

        radius = 5f;
        moveSpeed = 5f;


    }

    // Update is called once per frame
    void Update()
    {
        startPoint = this.transform.position;

        randomSecs = UnityEngine.Random.Range(3, 7);
        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = randomSecs;
        }
        else
        {
            canFire = false;
        }

    }
    void FixedUpdate()
    {
        if (canFire)
        {
            SpawnProjectiles(numberOfProjectiles);
        }
    }



    void SpawnProjectiles(int numberOfProjectiles)
    {
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i <= numberOfProjectiles - 1; i++)
        {

            float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeed;

            var proj = Instantiate(enemybullet, startPoint, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity =
                new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            angle += angleStep;
        }
    }


}
