using UnityEngine;

namespace Player
{
    public class PlayerHitBox : MonoBehaviour
    {
        private PlayerProgressController _playerProgressController;

        private void Awake()
        {
            _playerProgressController = GetComponentInParent<PlayerProgressController>();
            if (_playerProgressController == null)
            {
                Debug.LogError("Could not find PlayerCubesController");
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Pickup"))
            {
                _playerProgressController.PickupCube();
                Destroy(other.gameObject);
            }
        }
    }
}