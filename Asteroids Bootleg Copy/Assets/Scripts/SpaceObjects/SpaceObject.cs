using UnityEngine;

namespace AsteroidsCode.SpaceObjects
{
    public class SpaceObject : MonoBehaviour
    {
        [HideInInspector] public Transform _transform;

        [SerializeField] protected float maxSpeed;
     
        private void Awake()
        {
            _transform = transform;
        }
    }
}
