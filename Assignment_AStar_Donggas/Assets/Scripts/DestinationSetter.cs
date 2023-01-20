using UnityEngine;
using Define;

public class DestinationSetter : MonoBehaviour
{
    [SerializeField] MapManager mapManager;
    
    public Vector3? Destination { get; private set; } = new Vector3();

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
        float destPosX = Mathf.Round(mousePos.x);
        float destPosZ = Mathf.Round(mousePos.z);

        // ����Ų Ÿ���� ��ġ�� ���
        Vector3? dest = new Vector3(destPosX, PointTile.tileHeight, destPosZ);

        // ����Ų ��ġ�� ��ֹ��� �ִ��� �Ǵ�
        // ��ֹ��� ������ null ��ȯ
        if (mapManager.Map[(int)destPosX, (int)destPosZ])
        {
            dest = null;
        }

        return dest;
    }
}
