using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GamePlay
{
    public class Throwable : MonoBehaviour
    {
        [Header("Lanzar Objetos")]
        [SerializeField] UnityEvent onCollideWith = new UnityEvent();
        [SerializeField] LayerMask collisionLayerMask = ~0;
        [SerializeField] bool destroyOnCollide = false;
        [SerializeField] Color gizmoColor = Color.blue;
        [SerializeField] float rangeSound;

        private void OnCollisionEnter(Collision collision)
        {
            if (collisionLayerMask == (collisionLayerMask | (1 << collision.gameObject.layer)))
            {
                onCollideWith?.Invoke();

                if (destroyOnCollide)
                    Destroy(this);
            }
        }
        public void MakeAnInterestingSound()
        {
            var sound = new Sound(transform.position, rangeSound, Sound.SoundType.Interesting);

            Sounds.MakeSound(sound);
        }

        public void MakeADangerousSound( )
        {
            var sound = new Sound(transform.position, rangeSound, Sound.SoundType.Dangerous);

            Sounds.MakeSound(sound);
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position, rangeSound);
        }
    }
}


    