using System;
using System.Collections;
using UnityEngine;
using AsteroidsCode.Core;

namespace AsteroidsCode.SpaceObjects
{
    public class AsteroidSaucerSpawner : MonoBehaviour
    {
        public Asteroid asteroidPrefab;
        public Saucer saucerPrefab;

        public float timeForSaucerToSpawn = 15f;
        public int startingAsteroidAmount = 7;        

        public static int AsteroidsCount
        {
            get
            {
                return asteroidsCount;
            }
            set
            {
                asteroidsCount = value;

                if(asteroidsCount < minAsteroidsToSpawnNew && !applicationQuitting)
                {
                    SpawnNewAsteroids?.Invoke(asteroidsToSpawn);
                }
            }
        }

        private static int asteroidsCount;

        public static bool applicationQuitting = false;
        private static int minAsteroidsToSpawnNew = 4;
        private static int asteroidsToSpawn = 4;

        private static event Action<int> SpawnNewAsteroids;

        private void Awake()
        {
            SpawnAsteroid(startingAsteroidAmount);
            AsteroidsCount = startingAsteroidAmount;
        }

        private IEnumerator Start()
        {
            while (!applicationQuitting)
            {
                yield return new WaitForSeconds(timeForSaucerToSpawn);
                Vector2 spawnPos = SpaceRandomizer.GetRandomEdgePosition(out _);

                Instantiate(saucerPrefab, spawnPos, Quaternion.identity);
            }
        }

        private void OnEnable()
        {
            SpawnNewAsteroids += SpawnAsteroid;
        }

        private void OnDisable()
        {
            SpawnNewAsteroids -= SpawnAsteroid;
        }

        private void OnApplicationQuit()
        {
            applicationQuitting = true;
        }

        private void SpawnAsteroid(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Vector2 spawnPos = SpaceRandomizer.GetRandomEdgePosition(out Quaternion rotation);

                Instantiate(asteroidPrefab, spawnPos, rotation);
            }      
        }
    }
}
