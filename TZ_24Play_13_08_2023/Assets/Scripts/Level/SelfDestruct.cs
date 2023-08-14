using UnityEngine;

namespace Level
{
    public class SelfDestruct : MonoBehaviour
    {
        [SerializeField] [Range(0f, 100f)] private float destroyDelay = 1f;

        private void Start()
        {
            Destroy(gameObject, destroyDelay);
        }
    }
}