using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    public Rigidbody rbEnemy;
    private GameObject player;
    public int pointsWorth;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rbEnemy = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            CheckPosition();
        }
    }

    void CheckPosition()
    {
        Vector3 lookDirNormd = (player.transform.position - transform.position).normalized;
        rbEnemy.AddForce(lookDirNormd * speed);

        if (transform.position.y < -10)
        {
            gameManager.UpdatePoints(pointsWorth);
            Destroy(gameObject);
        }
    }
}
