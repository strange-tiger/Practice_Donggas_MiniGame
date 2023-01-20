using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationMarker : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// ��ġ ���� �� Ȱ��ȭ
    /// </summary>
    /// <param name="position"></param>
    public void Mark(Vector3 position)
    {
        transform.position = (Vector3)position;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}
