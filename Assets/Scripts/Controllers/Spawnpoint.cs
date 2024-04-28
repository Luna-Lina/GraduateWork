using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarVelocity
{
    public class Spawnpoint : MonoBehaviour
    {
        [SerializeField] private GameObject[] obstaclePrefabs;
        [SerializeField] private Transform[] startPoints;
        [SerializeField] private Transform[] endPoints;

        public float minSpawnInterval = 2f;
        public float maxSpawnInterval = 5f;
        public float destroyDistance = 0.1f;

        private float spawnTimer;

        void Start()
        {
            spawnTimer = Random.Range(minSpawnInterval, maxSpawnInterval);
        }

        void Update()
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0f)
            {
                SpawnObstacle();
                spawnTimer = Random.Range(minSpawnInterval, maxSpawnInterval);
            }
        }

        void SpawnObstacle()
        {
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            Transform startPoint = startPoints[Random.Range(0, startPoints.Length)];
            GameObject obstacle = Instantiate(obstaclePrefab, startPoint.position, Quaternion.identity);

            obstacle.tag = "Obstacle";

            StartCoroutine(DestroyObstacleWhenReachedEndPoint(obstacle));
        }

        IEnumerator DestroyObstacleWhenReachedEndPoint(GameObject obstacle)
        {
            Vector3 endPointPosition = endPoints[Random.Range(0, endPoints.Length)].position;

            while (obstacle != null && Vector3.Distance(obstacle.transform.position, endPointPosition) > destroyDistance)
            {
                yield return null;
            }

            if (obstacle != null)
            {
                Destroy(obstacle);
            }
        }
    }
}