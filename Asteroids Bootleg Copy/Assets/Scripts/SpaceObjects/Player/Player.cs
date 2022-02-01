using System;
using UnityEngine;
using AsteroidsCode.Core;
using AsteroidsCode.UI;

namespace AsteroidsCode.SpaceObjects
{
    public class Player : SpaceObject
    {
        public LayerMask[] deathLayerMasks;

        public float acceleration;

        public int maxLaserCharges = 3;       
        public int timeForLaserToRecharge;       

        [HideInInspector] public float currentAcceleration = 0f;
        [HideInInspector] public float rotationValue = 0f;

        [SerializeField] private float rotationSpeed;
        [SerializeField] private float drag;

        public event Action OnGameOver;

        public float CurrentSpeed
        {
            get
            {
                return currentSpeed;
            }
            set
            {
                if (currentSpeed == value)
                    return;

                currentSpeed = value;
                UIManager.Instance.SetSpeed(value);
            }
        }

        private float currentSpeed;

        public int CurrentLaserCharges
        {
            get
            {
                return currentLaserCharges;
            }
            set
            {
                if (currentLaserCharges < value)
                    CurrentTimeForLaserToRecharge = 0;

                currentLaserCharges = value;
                UIManager.Instance.SetLaserCharges(currentLaserCharges);
            }
        }

        private int currentLaserCharges;

        public int CurrentTimeForLaserToRecharge
        {
            get
            {
                return currentTimeForLaserToRecharge;
            }
            set
            {
                currentTimeForLaserToRecharge = value;
                UIManager.Instance.SetLaserRecharge(timeForLaserToRecharge - currentTimeForLaserToRecharge);
            }
        }

        private int currentTimeForLaserToRecharge;

        private void Start()
        {
            CurrentLaserCharges = maxLaserCharges;
        }

        private void Update()
        {
            SpaceObjectMovement.Rotate(rotationSpeed, rotationValue, _transform);
            CurrentSpeed = SpaceObjectMovement.Move(CurrentSpeed, maxSpeed, _transform.up, _transform, currentAcceleration, drag);

            UIManager.Instance.SetPlayerPosition(_transform.position);
            UIManager.Instance.SetPlayerRotation(_transform.rotation.eulerAngles.z);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            for (int i = 0; i < deathLayerMasks.Length; i++)
            {
                if ((deathLayerMasks[i].value & (1 << collision.gameObject.layer)) > 0)
                {
                    Destroy(gameObject);
                }
            }
        }

        private void OnDestroy()
        {
            UIManager.Instance.GameOver();
            OnGameOver?.Invoke();            
        }
    }
}

