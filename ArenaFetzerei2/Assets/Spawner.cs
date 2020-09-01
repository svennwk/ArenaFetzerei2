using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float spawnDelay = 5f;
    public GameObject EnemyPrefab;

    IEnumerator Start() {
        while(true) {
            yield return new WaitForSeconds(spawnDelay * Random.Range(0.8f,1.2f));
            Spawn();
        }
    }

    void Spawn() {
        Instantiate(EnemyPrefab, this.transform.position, Quaternion.identity);
    }
}
