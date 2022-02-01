using AsteroidsCode.Core;

namespace AsteroidsCode.SpaceObjects
{
    public class Saucer : SpaceObject
    {
        public int scoreValue;

        public Player player;

        private void Update()
        {
            if(player != null)
            {
                SpaceObjectMovement.Move(maxSpeed, maxSpeed, (player._transform.position - _transform.position).normalized, _transform);
            }          
        }

        private void OnDisable()
        {
            SpaceMetrics.Score += scoreValue;
        }
    }
}
