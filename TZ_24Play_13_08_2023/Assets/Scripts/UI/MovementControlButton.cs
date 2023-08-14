using UnityEngine;
using UnityEngine.EventSystems;
using Player;

namespace UI
{
    public class MovementControlButton : MonoBehaviour, IPointerMoveHandler
    {
        [SerializeField] private int lineNumber;

        [SerializeField] private PlayerMovement playerMovement;
        
    
        public void OnPointerMove(PointerEventData eventData)
        {
            playerMovement.SwitchLine(lineNumber);
        }
    }
}