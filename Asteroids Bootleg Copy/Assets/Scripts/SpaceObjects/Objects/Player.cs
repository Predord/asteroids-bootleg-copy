using UnityEngine;
using AsteroidsCode.Core;

namespace AsteroidsCode.SpaceObjects
{
    public class Player : SpaceObject
    {
        public float acceleration;

        public int maxLaserCharges = 3;       
        public float timeForLaserToRecharge;       
        [HideInInspector] public int currentTimeForLaserToRecharge;

        [HideInInspector] public float currentAcceleration = 0f;
        [HideInInspector] public float rotationValue = 0f;

        [SerializeField] private float rotationSpeed;
        [SerializeField] private float drag;

        public int CurrentLaserCharges
        {
            get
            {
                return currentLaserCharges;
            }
            set
            {
                if (currentLaserCharges < value)
                    currentTimeForLaserToRecharge = 0;

                currentLaserCharges = value;
            }
        }

        private int currentLaserCharges;

        private void Start()
        {
            CurrentLaserCharges = maxLaserCharges;
        }

        private void Update()
        {
            SpaceObjectMovement.Rotate(rotationSpeed, rotationValue, _transform);
            currentSpeed = SpaceObjectMovement.Move(currentSpeed, maxSpeed, _transform.up, _transform, currentAcceleration, drag);
        }
    }
}

