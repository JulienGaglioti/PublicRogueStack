using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float MousePanSensibility;
    public float ZoomSpeed;
    public float MinSize;
    public float MaxSize;
    public CinemachineVirtualCamera VirtualCamera;

    private Vector3 _startingPosition;

    // public float EdgeSize;
    // public float PanEdgeSpeed;
    
    private Vector3 _startingPanPosition;
    private Vector3 _startingMousePosition;
    private Vector3 _mouseOffset;

    private Camera _mainCamera;
    private bool _mousePressed;

    private void Start()
    {
        _mainCamera = Camera.main;
        _startingPosition = VirtualCamera.transform.position;
    }

    private void Update()
    {
        //HandlePanUpdate();
        HandleZoomUpdate();
        HandleDragUpdate();
    }

    private void HandleZoomUpdate()
    {
        float size = VirtualCamera.m_Lens.OrthographicSize;
        size += Input.GetAxis("Mouse ScrollWheel") * -ZoomSpeed;
        size = Mathf.Clamp(size, MinSize, MaxSize);

        VirtualCamera.m_Lens.OrthographicSize = size;
    }

    private void HandleDragUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.CompareTag("Background"))
                {
                    _mousePressed = true;
                    _startingMousePosition = Input.mousePosition;
                    _startingPanPosition = VirtualCamera.transform.position;
                }
            }
        }

        if (_mousePressed)
        {
            _mouseOffset = Input.mousePosition - _startingMousePosition;
            _mouseOffset.z = 0;
            VirtualCamera.transform.position = _startingPanPosition - (_mouseOffset * MousePanSensibility);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _mousePressed = false;
        }
    }
    
    private void HandlePanUpdate()
    {
        /*if (Input.mousePosition.x > Screen.width - EdgeSize)
        {
            VirtualCamera.transform.localPosition += VirtualCamera.transform.right * PanEdgeSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.x < EdgeSize)
        {
            VirtualCamera.transform.localPosition += VirtualCamera.transform.right * -1 * PanEdgeSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.y > Screen.height - EdgeSize)
        {
            VirtualCamera.transform.localPosition += VirtualCamera.transform.up * PanEdgeSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.y < EdgeSize)
        {
            VirtualCamera.transform.localPosition += VirtualCamera.transform.up * -1 * PanEdgeSpeed * Time.deltaTime;
        }*/
    }
}

