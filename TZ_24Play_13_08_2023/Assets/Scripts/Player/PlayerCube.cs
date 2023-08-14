using UnityEngine;

namespace Player
{
    public class PlayerCube : MonoBehaviour
    {
        [SerializeField] private Transform parentObject;
        [SerializeField] [Range(0f, 20f)] private float lifeTimeAfterRemoving = 10f;
        
        private PlayerProgressController _playerProgressController;
        private Transform _removedCubesContainer;

        private void Awake()
        {
            _playerProgressController = GetComponentInParent<PlayerProgressController>();
            if (_playerProgressController == null)
            {
                Debug.LogError("Could not find PlayerCubesController");
            }

            _removedCubesContainer = GameObject.FindWithTag("RemovedCubesContainer").transform;
            if (_removedCubesContainer == null)
            {
                Debug.LogError("Could not find RemovedCubesContainer");
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("WallPiece"))
            {
                _playerProgressController.RemoveCube(this);
            }
        }

        public void Remove()
        {
            parentObject.parent = _removedCubesContainer;
            Destroy(parentObject.gameObject, lifeTimeAfterRemoving);
        }
    }
}