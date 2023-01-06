using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlockType = Block.EBlockType;

public class Player : MonoBehaviour
{
    [SerializeField] Material _defaultMat;
    [SerializeField] Material _translucentMat;
    [SerializeField] float _speed;

    private const string BLOCK_LAYER = "Block";

    private BlockType _curBlockType;
    private MeshRenderer _renderer;
    private float _radius;
    private float _defaultSpeed;
    private float _curSpeed;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material = _defaultMat;

        _radius = GetComponent<CapsuleCollider>().radius;

        _defaultSpeed = _speed * Time.deltaTime;
        _curSpeed = _defaultSpeed;
    }

    private void Update()
    {
        MoveInput();
    }

    /// <summary>
    /// WASD �Է¿� ���� �̵��Ѵ�.
    /// </summary>
    private void MoveInput()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(_curSpeed * Vector3.forward);
        else if (Input.GetKey(KeyCode.A))
            transform.Translate(_curSpeed * Vector3.left);
        else if (Input.GetKey(KeyCode.S))
            transform.Translate(_curSpeed * Vector3.back);
        else if (Input.GetKey(KeyCode.D))
            transform.Translate(_curSpeed * Vector3.right);
    }

    /// <summary>
    /// ���� �浹�ϰ� �ִ� ����� ���ο� �߾����� ���� 
    /// ����� ������ �޴´�.
    /// ��, InfluencedByBlock�� ȣ���Ѵ�.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(BLOCK_LAYER) && CheckInField(other.transform.position))
        {
            _curBlockType = other.GetComponent<Block>().CurType;

            InfluencedByBlock(_curBlockType, other.transform.position);
        }
    }

    /// <summary>
    /// ����� ���ο� �÷��̾��� �߾����� ������ �Ǻ��Ѵ�.
    /// </summary>
    /// <param name="blockPos"></param>
    /// <returns></returns>
    private bool CheckInField(Vector3 blockPos)
    {
        float xDif = blockPos.x - transform.position.x;
        float zDif = blockPos.z - transform.position.z;

        if (xDif < -1f * _radius || xDif > _radius)
            return false;

        if (zDif < -1f * _radius || zDif > _radius)
            return false;

        return true;
    }

    /// <summary>
    /// �Ű������� �޴� ����� Ÿ�Կ� ���� 
    /// �÷��̾ �޴� ������ �ٲ۴�.
    /// </summary>
    private const float RIVER_DEVELERATION = 0.5f;
    private void InfluencedByBlock(BlockType blockType, Vector3 blockPos)
    {
        ResetInfluence();

        switch (blockType)
        {
            case BlockType.NONE:
                break;
            case BlockType.BUSH:
                _renderer.material = _translucentMat;
                break;
            case BlockType.RIVER:
                _curSpeed *= RIVER_DEVELERATION;
                break;
            case BlockType.WALL:
                MoveOutToField(blockPos);
                break;
            default:
                Debug.LogError("Error: �������� �ʴ� ��� ����");
                break;
        }
    }

    /// <summary>
    /// �÷��̾��� ��ȭ�� �ʱ�ȭ�Ѵ�.
    /// </summary>
    private void ResetInfluence()
    {
        _renderer.material = _defaultMat;
        _curSpeed = _defaultSpeed;
    }

    /// <summary>
    /// �� ��Ͽ� ������, �� ��ŭ �ٽ� ������ �ϴ� ������ 
    /// ����� ������ �� ������ �Ѵ�.
    /// </summary>
    /// <param name="blockPos"></param>
    private void MoveOutToField(Vector3 blockPos)
    {
        Vector3 direction = transform.position - blockPos;

        direction -= direction.y * Vector3.up;

        transform.Translate(direction);
    }
}
