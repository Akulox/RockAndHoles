using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour
{
    public GameObject cameraObject;
    public GameObject[] buttons;
    bool _drag;
    private Vector3 _startMousePos;
    private float[] _buttonAngles = new float[7];
    public float cameraRadius = 150f;
    public float cameraHeight = 50f;
    public float cameraAngle = 45f;
    public Animator wizard;
    public Animator hand;

    float VectorAngleZ(Vector3 a, Vector3 b, Vector3 c)
    {
        float signedAngleA = (float)(Math.Atan2(a[1] - b[1], a[0] - b[0]) / Math.PI * 180) + 90f;
        float signedAngleB = (float)(Math.Atan2(a[1] - c[1], a[0] - c[0]) / Math.PI * 180) + 90f;
        signedAngleA = (signedAngleA < 0) ? signedAngleA + 360 : signedAngleA;
        signedAngleB = (signedAngleB < 0) ? signedAngleB + 360 : signedAngleB;
        return signedAngleA - signedAngleB;
    }

    private void SaveButtonsAngles()
    {
        int s = 0;
        foreach (var k in buttons)
        {
            _buttonAngles[s] = k.transform.rotation.eulerAngles.z;
            s++;
        }
    }

    void ChangeCameraPosition(float angle)
    {
        cameraObject.transform.position = new Vector3((float)Math.Cos(2*Math.PI/360 * (angle-90f)) * cameraRadius, cameraHeight, -(float) Math.Sin(2*Math.PI/360 * (angle-90f)) * cameraRadius);
        cameraObject.transform.rotation = Quaternion.AngleAxis(angle-180, Vector3.up) * Quaternion.AngleAxis(cameraAngle, Vector3.right);
    }

    void FixedUpdate()
    {
        if (_drag)
        {
            int s = 0;
            foreach (var k in buttons)
            {
                k.transform.rotation = Quaternion.AngleAxis(_buttonAngles[s] + VectorAngleZ(buttons[6].transform.position, Input.mousePosition, _startMousePos), Vector3.forward);
                s++;
            }
            ChangeCameraPosition(buttons[0].transform.rotation.eulerAngles.z);
        }
    }
    public void StartDrag()
    {
        _drag = true;
        _startMousePos = Input.mousePosition;
        SaveButtonsAngles();
    }
    public void EndDrag()
    {
        _drag = false;
    }
    public void MakeMove(int dir)
    {
        if (!_drag)
        {
            wizard.SetTrigger("move");
            hand.SetTrigger("move" + (dir+1));
            VarManager.MakeMove(dir);
        }
    }

    public void Restart()
    {
        VarManager.DictionariesClear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Home()
    {
        VarManager.DictionariesClear();
        SceneManager.LoadScene("LevelMap");
    }
}
