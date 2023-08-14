using UnityEngine;

public class GameController : MonoBehaviour
{
    [field: Header("Lines settings")]
    [field: SerializeField] [field: Range(0, 15)] public int LinesCount { get; private set; } = 5;
    [field: SerializeField] [field: Range(0f, 2f)] public float LineWidth { get; private set; } = 1f;
    public static GameController Instance { get; private set; }

    [Header("Level generationSettings")]
    [SerializeField] private GameObject levelPartPrefab;
    
    [Tooltip("Offset from generation trigger")]
    [SerializeField] private Vector3 levelGenerationOffset;
    [SerializeField] private Transform levelPartsContainer;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null)
        {
            Debug.LogWarning("There is more than 1 GameController instance");
            Destroy(this);
            return;
        }
        
        Instance = this;
    }

    public void  BuildLevelPart(Vector3 triggerPosition)
    {
        Instantiate(levelPartPrefab, triggerPosition + levelGenerationOffset,
            Quaternion.identity, levelPartsContainer);
    }
}