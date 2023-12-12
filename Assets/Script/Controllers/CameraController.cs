using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Define.CameraMode _mode = Define.CameraMode.QuerterView;

    [SerializeField]
    private Vector3 _delta = new Vector3(0f, 30f, -20f);

    [SerializeField]
    private GameObject player = null;

    public void SetPlayer(GameObject playerObj) { player = playerObj; }

    void Start()
    {
        
    }

    void LateUpdate()
    {
        if (_mode == Define.CameraMode.QuerterView)
        {
            if (!player.IsValid())
            {
                return;
            }

            RaycastHit hit;
            if (Physics.Raycast(player.transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Wall")))
            {
                // 방향 벡터의 크기
                float dist = (hit.point - player.transform.position).magnitude * 0.8f;
                transform.position = player.transform.position + _delta.normalized * dist;
            }
            else
            {
                transform.position = player.transform.position + _delta;
                transform.LookAt(player.transform);
            }
        }
    }

    public void SetQuerterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuerterView;
        _delta = delta;
    }
}
