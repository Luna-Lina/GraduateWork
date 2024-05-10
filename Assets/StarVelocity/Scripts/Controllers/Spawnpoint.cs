using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarVelocity.Controllers
{
    public class Spawnpoint : MonoBehaviour
    {
        [SerializeField] private GameObject[] _obstaclePrefabs;
        [SerializeField] private GameObject[] _foodPrefabs;
        [SerializeField] private Transform[] _startPoints;
        [SerializeField] private Transform[] _endPoints;
        [SerializeField] private Transform[] _startForFood;
        [SerializeField] private Transform[] _endForFood;

        [SerializeField] private float minSpawnInterval = 2f;
        [SerializeField] private float maxSpawnInterval = 5f;
        [SerializeField] private float destroyDistance = 0.1f;

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
                SpawnFood();
                spawnTimer = Random.Range(minSpawnInterval, maxSpawnInterval);
            }
        }

        void SpawnObstacle()
        {
            GameObject obstaclePrefab = _obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Length)];
            Transform startPoint = _startPoints[Random.Range(0, _startPoints.Length)];
            GameObject obstacle = Instantiate(obstaclePrefab, startPoint.position, Quaternion.identity);

            obstacle.tag = "Obstacle";

            StartCoroutine(DestroyObstacleWhenReachedEndPoint(obstacle));
        }

        void SpawnFood()
        {
            GameObject foodPrefabs = _foodPrefabs[Random.Range(0, _foodPrefabs.Length)];
            Transform startPoint = _startForFood[Random.Range(0, _startForFood.Length)];
            GameObject food = Instantiate(foodPrefabs, startPoint.position, Quaternion.identity);

            food.tag = "Food";
        }

        IEnumerator DestroyObstacleWhenReachedEndPoint(GameObject obstacle)
        {
            Vector3 endPointPosition = _endPoints[Random.Range(0, _endPoints.Length)].position;

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