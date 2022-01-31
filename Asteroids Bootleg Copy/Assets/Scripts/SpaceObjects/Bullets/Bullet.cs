using UnityEngine;
using AsteroidsCode.Core;

namespace AsteroidsCode.SpaceObjects
{
    public class Bullet : SpaceObject
    {
        [SerializeField] protected LayerMask[] layerMask;
        [SerializeField] protected float liveTime;

        private void Start()
        {
            Destroy(gameObject, liveTime);
        }

        private void Update()
        {
            SpaceObjectMovement.Move(maxSpeed, maxSpeed, _transform.up, _transform);
        }
    }
}
