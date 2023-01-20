using UnityEngine;
using Define;
using System;

public class DestinationSetter : MonoBehaviour
{
    [SerializeField] MapManager mapManager;
    [SerializeField] DestinationMarker destinationMarker;

    public event Action<Vector3> DestinationChanged;
    public Vector3? Destination { get; private set; } = new Vector3?();

    private void Update()
    {
        // ���콺 ������ Ŭ���� �ϸ� ���콺�� ����Ų ������ �������� ����
        if (Input.GetMouseButtonDown(1))
        {
            Destination = SetDestination();
        }
    }

    /// <summary>
    /// �������� ���콺�� ����Ų ������ ����
    /// </summary>
    /// <returns>Vector3�� Nullable�� ��ȯ</returns>
    private Vector3? SetDestination()
    {
        // ���콺�� ����Ų ��ġ�� �޴´�.
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // ���콺�� x, z ��ġ�� �ݿø�
        float destPosX = RoundPosition(mousePos.x);
        float destPosZ = RoundPosition(mousePos.z);

        // ����Ų Ÿ���� ��ġ�� ���
        Vector3? dest;

        // ����Ų ��ġ�� ��ֹ��� �ִ��� �Ǵ�
        // ��ֹ��� ������ null ��ȯ
        // ������ ��ġ ��ȯ
        if (mapManager.Map[(int)destPosX, (int)destPosZ])
        {
            dest = null;
        }
        else
        {
            dest = new Vector3(destPosX, PointTile.TILE_HEIGHT, destPosZ);
        }

        // ������ ǥ��
        MarkDestination(dest);

        return dest;
    }

    private float RoundPosition(float pos)
    {
        pos = Mathf.Round(pos);

        pos = Mathf.Clamp(pos, 0f, (float)mapManager.MapSize - 1f);

        return pos;
    }

    /// <summary>
    /// destination Mark ������Ʈ�� ������ ������ ǥ��
    /// </summary>
    /// <param name="position"></param>
    private void MarkDestination(Vector3? position)
    {
        if (position.HasValue)
        {
            destinationMarker.Mark((Vector3)position);
            DestinationChanged.Invoke((Vector3)position);
        }
    }
}
