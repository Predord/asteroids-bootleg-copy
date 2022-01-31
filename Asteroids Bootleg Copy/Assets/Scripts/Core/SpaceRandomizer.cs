using UnityEngine;

namespace AsteroidsCode.Core
{
    public static class SpaceRandomizer
    {
        private const int playerMask = 1 << 6;
        private const int turnsToFindNewPosition = 5;
        private const float minRadiusToSpawn = 3;

        public static Vector2 GetRandomEdgePosition(out Quaternion rotation)
        {
            Vector2 spawnPos;

            for(int i = 0; i < turnsToFindNewPosition; i++)
            {
                //Choose spawn point on horizontal or vertical lines: 0 - horizontal, 1 - vertical
                if (Random.Range(0, 2) == 0)
                {
                    //Choose spawn point on lower or upper part of screen, 0 - lower, 1 - upper
                    if (Random.Range(0, 2) == 0)
                    {
                        spawnPos = new Vector2(Random.Range(-SpaceMetrics.worldWidth, SpaceMetrics.worldWidth), -SpaceMetrics.worldHeight);
                        rotation = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(-30f, 30f)));
                    }
                    else
                    {
                        spawnPos = new Vector2(Random.Range(-SpaceMetrics.worldWidth, SpaceMetrics.worldWidth), SpaceMetrics.worldHeight);
                        rotation = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(150f, 210f)));
                    }
                }
                else
                {
                    //Choose spawn point on left or right part of screen, 0 - left, 1 - right
                    if (Random.Range(0, 2) == 0)
                    {
                        spawnPos = new Vector2(-SpaceMetrics.worldWidth, Random.Range(-SpaceMetrics.worldHeight, SpaceMetrics.worldHeight));
                        rotation = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(-30f, 30f)));
                    }
                    else
                    {
                        spawnPos = new Vector2(SpaceMetrics.worldWidth, Random.Range(-SpaceMetrics.worldHeight, SpaceMetrics.worldHeight));
                        rotation = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(-60f, -120f)));
                    }
                }

                Collider[] targetsInCheckRadius = Physics.OverlapSphere(spawnPos, minRadiusToSpawn, playerMask);

                if (targetsInCheckRadius.Length == 0)
                    return spawnPos;

                for (int j = 0; j < targetsInCheckRadius.Length; j++)
                {
                    Transform target = targetsInCheckRadius[j].transform;
                    float distanceToTarget = Vector3.Distance(spawnPos, target.position);

                    if (distanceToTarget > minRadiusToSpawn)
                    {
                        return spawnPos;
                    }
                }
            }

            spawnPos = new Vector2(-SpaceMetrics.worldWidth, -SpaceMetrics.worldHeight);
            rotation = Quaternion.Euler(new Vector3(0f, 0f, 45f));

            return spawnPos;
        }
    }
}
