using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Speed settings")]
        [SerializeField] [Range(0f, 100f)] private float forwardSpeed = 10f;
        
        [Tooltip("Movement speed between lines")]
        [SerializeField] [Range(0f, 100f)] private float horizontalSpeed = 10f;

        [Header("Stickman settings")]
        [SerializeField] private Transform stickman;
        [Tooltip("Lift height after cube pickup")]
        [SerializeField] [Range(0f, 10f)] private float stickmanLiftHeight = 1.5f;
        
        [Header("Initialization settings")]
        [SerializeField] [Range(0, 10)] private int currentLine = 2;

        private bool _isMoving = true;
        private bool _isMovingBetweenLines;

        private void Update()
        {
            MoveForward();
        }

        public void LiftStickman()
        {
            stickman.position += Vector3.up * stickmanLiftHeight;
        }
        
        public void SwitchLine(int line)
        {
            
            if (!_isMoving ||
                _isMovingBetweenLines ||
                line < 0 ||
                line >= GameController.Instance.LinesCount ||
                line == currentLine)
            {
                return;
            }

            StartCoroutine(MoveBetweenLines((line - currentLine) * GameController.Instance.LineWidth));
            currentLine = line;
        }
        

        private void MoveForward()
        {
            if (!_isMoving) return;
            
            transform.position += Vector3.forward * (forwardSpeed * Time.deltaTime);
        }
        
        private void ChangeXPosition(float newXPosition)
        {
            Vector3 currentPosition = transform.position;
            transform.position = new Vector3(newXPosition, currentPosition.y, currentPosition.z);
        }
        
        private IEnumerator MoveBetweenLines(float offset)
        {
            _isMovingBetweenLines = true;
            float startPosition = transform.position.x;
            float endPosition = startPosition + offset;
            float travelPercent = 0f;
        
            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * horizontalSpeed / Mathf.Abs(startPosition - endPosition)
                 * GameController.Instance.LineWidth;
                float xPosition = Mathf.Lerp(startPosition, endPosition, travelPercent);
                ChangeXPosition(xPosition);
                yield return new WaitForEndOfFrame();
            }
            ChangeXPosition(endPosition);
            _isMovingBetweenLines = false;
        }
    }
}
