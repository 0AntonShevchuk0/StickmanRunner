using UnityEngine;

namespace Level
{
    public class Pickup : MonoBehaviour
    {
        private void Start()
        {
            InitializePosition();
        }

        private void InitializePosition()
        {
            float offsetMultiplier = Random.Range(0, GameController.Instance.LinesCount)
                                     * GameController.Instance.LineWidth;
            transform.position += Vector3.right * offsetMultiplier;
        }
    }
}