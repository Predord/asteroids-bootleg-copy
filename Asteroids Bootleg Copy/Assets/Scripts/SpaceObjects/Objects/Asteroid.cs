using UnityEngine;
using AsteroidsCode.Core;

namespace AsteroidsCode.SpaceObjects
{
    public class Asteroid : SpaceObject
    {
        public Asteroid debrisPrefab;

        public int scoreValue;

        private static int minDebrisSpawns = 2;
        private static int maxDebrisSpawns = 3;

        private void Update()
        {
            SpaceObjectMovement.Move(maxSpeed, maxSpeed, _transform.up, _transform);
        }

        private void OnDisable()
        {
            if (debrisPrefab != null && SpaceSpawner.spawnDebris && !SpaceSpawner.applicationQuitting)
            {
                for (int i = 0; i < Random.Range(minDebrisSpawns, maxDebrisSpawns + 1); i++)
                {
                    Instantiate(debrisPrefab, _transform.position, Quaternion.Euler(new Vector3(0f, 0f, Random.Range(-180f, 180f))), transform.parent);
                    SpaceSpawner.AsteroidsCount++;
                }
            }

            SpaceMetrics.Score += scoreValue;
            SpaceSpawner.AsteroidsCount--;
        }
    }
}
