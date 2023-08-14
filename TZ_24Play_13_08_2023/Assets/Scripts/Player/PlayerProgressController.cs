using UnityEngine;
using System.Collections.Generic;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerCubesController : MonoBehaviour
    {
        [SerializeField] private GameObject playerCubePrefab;
        [SerializeField] private Transform cubesContainer;
        [SerializeField] [Range(1, 25)] private int maxCubes;
        [SerializeField] private PlayerCube[] startCubes;
        [SerializeField] private ParticleSystem stackEffect;

        private PlayerMovement _playerMovement;
        
        private List<PlayerCube> _cubes = new();

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();

            foreach (var cube in startCubes)
            {
                _cubes.Add(cube);
            }

            if (_cubes.Count > maxCubes)
            {
                Debug.LogWarning("Too many start cubes");
            }
        }
        
        public void PickupCube()
        {
            if (_cubes.Count > maxCubes) return;
            
            _playerMovement.LiftStickman();

            GameObject newCube = Instantiate(playerCubePrefab, cubesContainer);
            _cubes.Add(newCube.GetComponentInChildren<PlayerCube>());
            Vector3 cubePosition = Vector3.up * (_cubes.Count - 1);
            newCube.transform.localPosition = cubePosition;

            if (stackEffect.isPlaying)
            {
                stackEffect.Stop();
            }
            stackEffect.transform.localPosition = cubePosition;
            stackEffect.Play();
        }

        public void RemoveCube(PlayerCube cube)
        {
            cube.Remove();
            _cubes.Remove(cube);

            if (_cubes.Count == 0)
            {
                LoseSequence();
            }
        }

        private void LoseSequence()
        {
            _playerMovement.StopMove();
        }
    }
}