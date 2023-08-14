using UnityEngine;

namespace Level
{
    public class LevelGenerationTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameController.Instance.BuildLevelPart(transform.position);
            }
        }
    }
}