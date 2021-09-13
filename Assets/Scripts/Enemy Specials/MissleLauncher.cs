using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleLauncher : MonoBehaviour
{
    public float missleLaunchDistance = 2;

    private GameObject player;
    private GameObject rocketPrefab;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rocketPrefab = GameObject.FindObjectOfType<SpawnManager>().enemyRocketPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        float playerDist = Vector3.Distance(transform.position, player.transform.position);

        if (playerDist <= missleLaunchDistance)
        {
            GameObject tempRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
            tempRocket.GetComponent<HomingRocket>().SetTarget(player.transform);
        }
    }
}
