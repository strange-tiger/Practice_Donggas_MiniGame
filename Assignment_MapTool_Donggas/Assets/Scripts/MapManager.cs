using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using BlockType = Block.EBlockType;

[System.Serializable]
public class MapData
{
    public int[] data = new int[400];
}

public class MapManager : MonoBehaviour
{
    [Header("Block Group")]
    [SerializeField] Transform[] _rows;

    private const string FILE_NAME = "MapData.json";

    private Block[,] _blockGroup;
    private string _path;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        Debug.Assert(_rows.Length * _rows[0].childCount == new MapData().data.Length, "��� �׷� ���� ���� Ȥ�� MapData ���� �ʿ�");

        _blockGroup = new Block[_rows.Length, _rows[0].childCount];

        for (int i = 0; i < _rows.Length; ++i)
        {
            for (int j = 0; j < _rows[i].childCount; ++j)
            {
                _blockGroup[i, j] = _rows[i].GetChild(j).GetComponent<Block>();
            }
        }

        _path = Path.Combine(Application.dataPath, FILE_NAME);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SaveMap();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            LoadMap();
        }
    }

    /// <summary>
    /// �� ����� Ÿ���� int ���·� ��ȯ�ϰ� Json�� �����Ѵ�.
    /// </summary>
    private void SaveMap()
    {
        MapData mapData = new MapData();

        for (int i = 0; i < _blockGroup.GetLength(0); ++i)
        {
            for (int j = 0; j < _blockGroup.GetLength(1); ++j)
            {
                mapData.data[i * _blockGroup.GetLength(0) + j] = (int)_blockGroup[i, j].CurType;
            }
        }

        string saveJson = JsonUtility.ToJson(mapData, true);
        File.WriteAllText(_path, saveJson);
    }

    /// <summary>
    /// ������ ���� Json ������ �ε��Ͽ� ��Ͽ� �����Ѵ�.
    /// </summary>
    private void LoadMap()
    {
        /*
         * ������ ������ ������ ���ٸ� ����
         */
        if (!File.Exists(_path))
        {
            return;
        }

        MapData mapData = new MapData();

        string loadJson = File.ReadAllText(_path);
        mapData = JsonUtility.FromJson<MapData>(loadJson);

        /*
         * �����Ϳ� ������ ������ �˻�
         * �ִٸ� ����
         */
        int typeMax = (int)BlockType.MAX;
        foreach (int type in mapData.data)
        {
            if (type < 0 || type >= typeMax)
            {
                return;
            }
        }

        /*
         * ��Ͽ� �ε��� �����͸� ����
         */
        for (int i = 0; i < _blockGroup.GetLength(0); ++i)
        {
            for (int j = 0; j < _blockGroup.GetLength(1); ++j)
            {
                _blockGroup[i, j].CurType = (BlockType)mapData.data[i * _blockGroup.GetLength(0) + j];
            }
        }
    }
}
