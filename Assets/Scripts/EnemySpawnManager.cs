using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnManager : MonoBehaviour {

    public Camera camera;
    
    public List<Vector3> spawnPoints;
    
    public GameObject Enemy1Prefab;
    public GameObject birbEnemyPrefab;
    public GameObject bigBigPrefab;
    
    private float time = 0f;

    private int bigBigSpawnCount = 0;
    private float timeInterval = 10f;
    
    private int rangeLeft = 1;
    private int rangeRight = 4;

    private void Start() {
        spawnPoints.Add(new Vector3(0, 0, 0));
        spawnPoints.Add(new Vector3(0, 0, 0));
        spawnPoints.Add(new Vector3(0, 0, 0));
        spawnPoints.Add(new Vector3(0, 0, 0));
    }

    private void Update() {
        Vector3 bottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 bottomRight = camera.ViewportToWorldPoint(new Vector3(1, 0, 0));
        Vector3 topRight = camera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Vector3 topLeft = camera.ViewportToWorldPoint(new Vector3(0, 1, 0));
        spawnPoints[0] = bottomLeft;
        spawnPoints[1] = bottomRight;
        spawnPoints[2] = topRight;
        spawnPoints[3] = topLeft;
        time += Time.deltaTime;
        if (time >= timeInterval) {
            int i = 0;
            while (i < 4) {
                if (Vector3.Distance(GameObject.Find("Player").transform.position, spawnPoints[i]) > 50f) {
                    // if (PlayerMechanics.score > 20) {
                    //     rangeRight = 4;
                    // }
                    int randomNumber = Random.Range(rangeLeft, rangeRight);
                    if (randomNumber == 1) {
                        Instantiate(birbEnemyPrefab, new Vector3(spawnPoints[i].x, spawnPoints[i].y, 0f), Quaternion.identity);
                        bigBigSpawnCount++;
                    }
                    else if (randomNumber == 2) {
                        Instantiate(Enemy1Prefab, new Vector3(spawnPoints[i].x, spawnPoints[i].y, 0f), Quaternion.identity);
                        bigBigSpawnCount++;
                    }
                    else if (randomNumber == 3) {
                        Instantiate(bigBigPrefab, new Vector3(spawnPoints[i].x, spawnPoints[i].y, 0f), Quaternion.identity);
                        if (bigBigSpawnCount >= 5) {
                            bigBigSpawnCount = 0;
                        }
                    }
                }
                i++;
            }
            time = 0f;
        }
    }
}