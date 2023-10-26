using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour
{
    [SerializeField]
    private Define.CameraMode _mode = Define.CameraMode.QuerterView;

    [SerializeField]
    private Vector3 _delta = new Vector3(0f, 30f, -20f);

    [SerializeField]
    private GameObject player = null;


    void Start()
    {
        
    }

    void Update()
    {
        if (_mode == Define.CameraMode.QuerterView)
        {
            transform.position = player.transform.position + _delta;
            transform.LookAt(player.transform);
        }
    }

    public void SetQuerterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuerterView;
        _delta = delta;
    }
}
