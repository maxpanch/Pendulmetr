using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera _mainCamera;
    [SerializeField] private Transform _player;
    [SerializeField] private float _cameraMoveSpeed = 3f;

    private void Awake()
    {
        _mainCamera = transform.GetComponent<Camera>();
    }
    private void Update()
    {
        FollowMovement();
    }
    private void FollowMovement()
    {
        float distance = Vector3.Distance(_player.transform.position, transform.position);
        Vector3 cameraMoveDirection = (_player.transform.position - transform.position).normalized;
        Debug.Log(distance);
        if (distance > 10f)
        {
            Vector3 newCameraPosition = transform.position + cameraMoveDirection * distance * _cameraMoveSpeed * Time.deltaTime;
            float distanceAfterMoving = Vector3.Distance(newCameraPosition, _player.transform.position);
            if (distanceAfterMoving > distance) newCameraPosition = _player.transform.position;
            transform.position = newCameraPosition;
        }
    }
}
