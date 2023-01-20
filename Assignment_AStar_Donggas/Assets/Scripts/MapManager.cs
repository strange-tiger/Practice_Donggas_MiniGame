using System.Collections.Generic;
using UnityEngine;
using Define;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrototype;
    [SerializeField] int mapSize;

    [Header("Player Init Position")]
    [SerializeField] int playerPosX;
    [SerializeField] int playerPosZ;

    public bool[,] Map { get; private set; }
    public int MapSize { get => mapSize; }

    private Stack<GameObject> obstaclePool;
    private Stack<GameObject> usedObstacles;

    private void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// �ʱ�ȭ�� ����
    /// �ν��Ͻ�ȭ, ��ֹ��� ������Ʈ Ǯ ����, �� �ʱ� ���� ����
    /// </summary>
    private void Initialize()
    {
        Map = new bool[mapSize, mapSize];
        obstaclePool = new Stack<GameObject>();
        usedObstacles = new Stack<GameObject>();

        InitObstaclePool();

        InitMap();
    }

    /// <summary>
    /// ��ֹ��� ������Ʈ Ǯ�� ����
    /// Stack���� ����
    /// </summary>
    private void InitObstaclePool()
    {
        for (int i = 0; i < mapSize * mapSize; ++i)
        {
            GameObject obstacle = Instantiate(obstaclePrototype);
            obstacle.SetActive(false);

            obstaclePool.Push(obstacle);
        }
    }
    
    /// <summary>
    /// Map�� �� ��ġ�� ��ֹ��� ��ġ������ 20% Ȯ���� ���
    /// </summary>
    private void InitMap()
    {
        for (int i = 0; i < Map.GetLength(0); ++i)
        {
            for (int j = 0; j < Map.GetLength(1); ++j)
            {
                if (i == playerPosX && j == playerPosZ)
                {
                    continue;
                }
                else if (Random.Range(Percent.MIN, Percent.MAX) < Percent.OBSTACLE)
                {
                    Map[i, j] = true;

                    InstallObstacle(i, j);
                }
                else
                {
                    Map[i, j] = false;
                }
            }
        }
    }

    /// <summary>
    /// ��ֹ��� �Ű������� ���� ��ġ�� ��ġ
    /// ������Ʈ Ǯ���� ��ֹ��� ����, usedObstacles�� �����ϰ� Ȱ��ȭ
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    private void InstallObstacle(int x, int z)
    {
        GameObject obstacle = obstaclePool.Pop();
        usedObstacles.Push(obstacle);

        obstacle.transform.position = new Vector3(x, 0, z);
        obstacle.SetActive(true);
    }
}
