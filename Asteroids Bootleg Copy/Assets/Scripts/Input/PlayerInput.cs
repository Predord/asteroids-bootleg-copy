using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using AsteroidsCode.SpaceObjects;

namespace AsteroidsCode.Input
{
    public class PlayerInput : MonoBehaviour
    {
        public DotBullet dotBulletPrefab;
        public Laser laserPrefab;

        private bool rotatingLeft;
        private bool rotatingRight;

        private Player _player;
        private Coroutine waitForLaserRecharge;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        public void Move(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _player.currentAcceleration = _player.currentAcceleration == 0f ? _player.acceleration : 0f;
            }
        }

        public void RotateLeft(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (rotatingLeft)
                {
                    _player.rotationValue -= 1f;
                    rotatingLeft = false;
                }
                else
                {
                    _player.rotationValue += 1f;
                    rotatingLeft = true;
                }
            }     
        }

        public void RotateRight(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (rotatingRight)
                {
                    _player.rotationValue += 1f;
                    rotatingRight = false;
                }
                else
                {
                    _player.rotationValue -= 1f;
                    rotatingRight = true;
                }
            }
        }

        public void FireDotBullet(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Instantiate(dotBulletPrefab, _player._transform.position, Quaternion.LookRotation(_player._transform.forward, _player._transform.up));
            }
        }

        public void FireLaser(InputAction.CallbackContext context)
        {
            if (context.performed && _player.CurrentLaserCharges > 0)
            {
                Instantiate(laserPrefab, _player._transform.position, Quaternion.LookRotation(_player._transform.forward, _player._transform.up));
                _player.CurrentLaserCharges--;

                if(waitForLaserRecharge == null)
                    waitForLaserRecharge = StartCoroutine(LaserRecharging());
            }
        }

        private IEnumerator LaserRecharging()
        {
            while(_player.CurrentLaserCharges != _player.maxLaserCharges)
            {
                for(float time = Time.deltaTime; time <= _player.timeForLaserToRecharge; time += Time.deltaTime)
                {
                    _player.CurrentTimeForLaserToRecharge = Mathf.FloorToInt(time);

                    yield return null;
                }

                _player.CurrentLaserCharges++;
            }

            waitForLaserRecharge = null;
        }
    }
}
