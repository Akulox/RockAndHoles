using System;
using TMPro;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    public float speed;
    public static bool active = true;
    private Vector3 _startPos;
    private float _targetPosX;
    private float _targetPosZ;

    void Start()
    {
        _targetPosX = transform.position.x;
        _targetPosZ = transform.position.z;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && active)
        {
            _startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) && active)
        {
            float posX = _startPos.x - Input.mousePosition.x;
            float posY = _startPos.y - Input.mousePosition.y;
            _targetPosX = Mathf.Clamp(transform.position.x - posX/10, -15.25f, 15.25f);
            _targetPosZ = Mathf.Clamp(transform.position.z - posY/10, -21f, 5);
            _startPos = Input.mousePosition;
        }
        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, _targetPosX, speed * Time.deltaTime),
            transform.position.y,
            Mathf.Lerp(transform.position.z, _targetPosZ, speed * Time.deltaTime));
    }
}
