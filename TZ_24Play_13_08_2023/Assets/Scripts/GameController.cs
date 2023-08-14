using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Player;

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

    [Header("Camera effects settings")]
    [SerializeField] private CinemachineVirtualCamera cinemachineCamera;
    [SerializeField] [Range(0f, 10f)] private float cameraShakeIntensity = 2f;
    [SerializeField] [Range(0f, 20f)] private float cameraShakeDuration = 1f;

    [Header("CanvasSettings")]
    [SerializeField] private GameObject startCanvas;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private GameObject endCanvas;

    private bool _gameStarted;

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

    private void Update()
    {
        WaitForStart();
        ProcessExit();
    }

    public void  BuildLevelPart(Vector3 triggerPosition)
    {
        Instantiate(levelPartPrefab, triggerPosition + levelGenerationOffset,
            Quaternion.identity, levelPartsContainer);
    }

    public IEnumerator ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin perlin =
            cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        perlin.m_AmplitudeGain = cameraShakeIntensity;
        yield return new WaitForSeconds(cameraShakeDuration);
        perlin.m_AmplitudeGain = 0f;
    }

    public void FinishGame()
    {
        gameCanvas.SetActive(false);
        endCanvas.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private void WaitForStart()
    {
        if (!_gameStarted && Input.touchCount > 0)
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        _gameStarted = true;
        
        startCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        foreach (var playerMovement in FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None))
        {
            playerMovement.StartMove();
        }
    }

    private void ProcessExit()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}