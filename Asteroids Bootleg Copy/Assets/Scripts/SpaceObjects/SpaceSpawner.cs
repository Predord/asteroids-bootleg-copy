using System;
using System.Collections;
using UnityEngine;
using AsteroidsCode.Core;

namespace AsteroidsCode.SpaceObjects
{
    public class SpaceSpawner : MonoBehaviour
    {
        public Asteroid asteroidPrefab;
        public Saucer saucerPrefab;
        public Player playerPrefab;

        public float timeForSaucerToSpawn = 15f;
        public int startingAsteroidAmount = 7;

        private Player player;
        private Coroutine saucerSpawn;

        public static int AsteroidsCount
        {
            get
            {
                return asteroidsCount;
            }
            set
            {
                asteroidsCount = value;

                if(asteroidsCount < minAsteroidsToSpawnNew && spawnDebris && !applicationQuitting)
                {
                    SpawnNewAsteroids?.Invoke(asteroidsToSpawn);
                }
            }
        }

        private static int asteroidsCount;

        public static bool applicationQuitting = false;
        public static bool spawnDebris = true;

        private static int minAsteroidsToSpawnNew = 4;
        private static int asteroidsToSpawn = 4;

        private static event Action<int> SpawnNewAsteroids;

        private void Awake()
        {
            SpawnAsteroid(startingAsteroidAmount);
            AsteroidsCount = startingAsteroidAmount;
        }

        private void OnEnable()
        {
            SpawnNewAsteroids += SpawnAsteroid;
        }

        private void OnDisable()
        {
            SpawnNewAsteroids -= SpawnAsteroid;

            if(player != null)
            {
                player.OnGameOver -= StopSaucerSpawn;
            }              
        }

        private void OnApplicationQuit()
        {
            applicationQuitting = true;
        }

        public void SpawnPlayer()
        {
            if (player != null)
            {
                Debug.LogWarning("Trying to spawn new player when old still exists");
                return;
            }

            ClearAllObjects();
            SpaceMetrics.Score = 0;

            player = Instantiate(playerPrefab);
            player.OnGameOver += StopSaucerSpawn;

            SpawnAsteroid(startingAsteroidAmount);
            AsteroidsCount = startingAsteroidAmount;

            if (saucerSpawn == null)
                saucerSpawn = StartCoroutine(SpawnSaucer());
        }

        public void ClearAllObjects()
        {
            spawnDebris = false;

            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            spawnDebris = true;
        }

        public void StopSaucerSpawn()
        {
            StopCoroutine(saucerSpawn);

            saucerSpawn = null;
        }

        private void SpawnAsteroid(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Vector2 spawnPos = SpaceRandomizer.GetRandomEdgePosition(out Quaternion rotation);

                Instantiate(asteroidPrefab, spawnPos, rotation, transform);
            }      
        }

        private IEnumerator SpawnSaucer()
        {
            while (!applicationQuitting)
            {
                yield return new WaitForSeconds(timeForSaucerToSpawn);
                Vector2 spawnPos = SpaceRandomizer.GetRandomEdgePosition(out _);

                Saucer instance = Instantiate(saucerPrefab, spawnPos, Quaternion.identity, transform);
                instance.player = player;
            }

            saucerSpawn = null;
        }
    }
}
