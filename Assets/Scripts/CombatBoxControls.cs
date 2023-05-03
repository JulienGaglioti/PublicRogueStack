using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatBoxControls : MonoBehaviour
{
    public Transform CombatBox;

    private Camera _camera;
    private Vector3 _offset;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnMouseDown()
    {
        _offset = CombatBox.transform.position - GetMousePosition() + Vector3.back * 100;
    }

    private void OnMouseDrag()
    {
        CombatBox.transform.position = GetMousePosition() + _offset;
    }

    private void OnMouseUp()
    {
        CombatBox.transform.position = new Vector3(Mathf.RoundToInt(CombatBox.transform.position.x),
            Mathf.RoundToInt(CombatBox.transform.position.y), 0);
    }

    private Vector3 GetMousePosition()
    {
        var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
}
