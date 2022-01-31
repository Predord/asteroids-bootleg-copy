using UnityEngine;

namespace AsteroidsCode.Core
{
    public static class SpaceObjectMovement 
    {
        public static void Rotate(float rotationSpeed, float rotationValue, Transform _transform)
        {
            _transform.Rotate(Vector3.forward, rotationSpeed * rotationValue * Time.deltaTime);
        }

        public static float Move(float currentSpeed, float maxSpeed, Vector3 direction, Transform _transform, float acceleration = 0, float drag = 0)
        {
            currentSpeed = Mathf.Clamp(currentSpeed + (acceleration - drag) * Time.deltaTime, 0f, maxSpeed);

            _transform.Translate(currentSpeed * direction, Space.World);
            _transform.position = new Vector2(
                    GetPositionInsideBounds(_transform.position.x, SpaceMetrics.worldWidth),
                    GetPositionInsideBounds(_transform.position.y, SpaceMetrics.worldHeight));

            return Mathf.Min(currentSpeed, maxSpeed);
        }

        private static float GetPositionInsideBounds(float currentPosition, float maxValue)
        {
            if(currentPosition < -maxValue)
            {
                return maxValue;
            }
            else if(currentPosition > maxValue)
            {
                return -maxValue;
            }

            return currentPosition;
        }
    }
}
