using UnityEngine;

public class UtilsClass
{
    private static Camera _mainCamera;
    public static Vector3 GetMouseWorldPosition()
    {
        if (_mainCamera == null) _mainCamera = Camera.main;
        var mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
}