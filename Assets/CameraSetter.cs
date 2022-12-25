using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetter : MonoBehaviour
{
    [SerializeField] private Transform _cameraPosition;
    private void Awake()
    {
        var camera = Camera.main;

        camera.transform.SetParent(_cameraPosition);
        camera.transform.localPosition = Vector3.zero;
        camera.transform.rotation = _cameraPosition.rotation;
    }
}
