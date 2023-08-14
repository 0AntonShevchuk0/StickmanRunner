using UnityEngine;

namespace Level
{
    public class WallBuilder : MonoBehaviour
    {
        [SerializeField] private GameObject wallPiece;
        [SerializeField] [Range(1, 20)] private int minWallHeight = 1;
        [SerializeField] [Range(1, 20)] private int maxWallHeight = 5;
        
        private void Start()
        {
            BuildWall();
        }

        private void BuildWall()
        {
            for (int i = 0; i < GameController.Instance.LinesCount; i++)
            {
                int wallHeight = Random.Range(minWallHeight, maxWallHeight + 1);
                for (int j = 0; j < wallHeight; j++)
                {
                    GameObject newWall = Instantiate(wallPiece, transform);
                    newWall.transform.localPosition = 
                        Vector3.right * i * GameController.Instance.LineWidth + Vector3.up * j;
                }
            }
        }
    }
}