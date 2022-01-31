using UnityEngine;
using AsteroidsCode.Core;

namespace AsteroidsCode.SpaceObjects
{
    public class Saucer : SpaceObject
    {
        public int scoreValue;

        private Player player;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        private void Update()
        {
            SpaceObjectMovement.Move(maxSpeed, maxSpeed, (player._transform.position - _transform.position).normalized, _transform);
        }

        private void OnDestroy()
        {
            SpaceMetrics.Score += scoreValue;
        }
    }
}
