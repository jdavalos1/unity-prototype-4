using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    public Rigidbody rbEnemy;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rbEnemy = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirNormd = (player.transform.position - transform.position).normalized;
        rbEnemy.AddForce(lookDirNormd * speed);

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
