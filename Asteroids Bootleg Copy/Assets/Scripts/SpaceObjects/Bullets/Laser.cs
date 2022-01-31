using UnityEngine;

namespace AsteroidsCode.SpaceObjects
{
    public class Laser : Bullet
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            for (int i = 0; i < layerMask.Length; i++)
            {
                if ((layerMask[i].value & (1 << collision.gameObject.layer)) > 0)
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
