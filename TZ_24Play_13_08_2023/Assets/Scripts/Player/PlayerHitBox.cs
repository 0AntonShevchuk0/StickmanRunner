using UnityEngine;

namespace Player
{
    public class PlayerHitBox : MonoBehaviour
    {
        private PlayerCubesController _playerCubesController;

        private void Awake()
        {
            _playerCubesController = GetComponentInParent<PlayerCubesController>();
            if (_playerCubesController == null)
            {
                Debug.LogError("Could not find PlayerCubesController");
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Pickup"))
            {
                _playerCubesController.PickupCube();
                Destroy(other.gameObject);
            }
        }
    }
}