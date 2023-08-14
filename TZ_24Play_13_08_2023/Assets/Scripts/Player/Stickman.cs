using UnityEngine;

namespace Player
{
    public class Stickman : MonoBehaviour
    {
        public void FinishGame()
        {
            GameController.Instance.FinishGame();
        }
    }
}