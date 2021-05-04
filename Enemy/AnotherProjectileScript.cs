using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherProjectileScript : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector2 target;
    public float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.position * speed * Time.deltaTime);

        //if (transform.position.x == target.x && transform.position.y == target.y)
        //{
        //    DestroyProjectile();
        //}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.name);
            Invoke("DestroyProjectile", lifetime);
        }
    }

    void DestroyProjectile()
    {
        
        Destroy(gameObject);
    }
}
