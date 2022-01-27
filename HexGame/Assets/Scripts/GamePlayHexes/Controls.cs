using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour
{
<<<<<<< Updated upstream
    [SerializeField] public GameObject cameraObject;
    [SerializeField] public GameObject[] buttons;
    
    [SerializeField] private Animator wizard;
    [SerializeField] private Animator hand;
    
    
    [SerializeField] private float cameraRadius = 150f;
    [SerializeField] private float cameraHeight = 50f;
    [SerializeField] private float cameraAngle = 45f;
    
    private Vector3 _startMousePos;
    private float[] _buttonAngles = new float[7];
    private bool _drag;
=======
    public class Controls : MonoBehaviour
    {
        [SerializeField] private SceneController sceneController;
        
        
        [SerializeField] private GameObject buttons;

        [SerializeField] private float cameraRadius = 150f;
        [SerializeField] private float cameraHeight = 50f;

        public static GameObject CameraObject;
        private Vector3 _startMousePos;
        private float _buttonAngles;
        private bool _drag;
>>>>>>> Stashed changes

    
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
<<<<<<< Updated upstream
            wizard.SetTrigger("move");
            hand.SetTrigger("move" + (dir+1));
            VarManager.MakeMove(dir);
=======
            CameraObject = GameObject.FindGameObjectWithTag("Camera");
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
    }
    
    private void ChangeCameraPosition(float angle)
    {
        cameraObject.transform.position = new Vector3((float)Math.Cos(2*Math.PI/360 * (angle-90f)) * cameraRadius, cameraHeight, -(float) Math.Sin(2*Math.PI/360 * (angle-90f)) * cameraRadius);
        cameraObject.transform.rotation = Quaternion.AngleAxis(angle-180, Vector3.up) * Quaternion.AngleAxis(cameraAngle, Vector3.right);
    }
    private void SaveButtonsAngles()
    {
        int s = 0;
        foreach (var b in buttons)
=======

        public void Restart()
        {
            sceneController.Restart();
        }

        public void Home()
        {
            sceneController.Home();
        }
        
        void FixedUpdate()
        {
            ChangeCameraPosition(buttons.transform.rotation.eulerAngles.z);
        }

        public void ResetButtonsAngles()
        {
            buttons.transform.rotation = new Quaternion();
        }

        private void ChangeCameraPosition(float angle)
        {
            if (!_drag) return;
            buttons.transform.rotation = Quaternion.AngleAxis(_buttonAngles + VectorAngleZ(buttons.transform.position, Input.mousePosition, _startMousePos), Vector3.forward);
            CameraObject.transform.position = new Vector3((float)Math.Cos(2*Math.PI/360 * (angle-90f)) * cameraRadius, cameraHeight, -(float) Math.Sin(2*Math.PI/360 * (angle-90f)) * cameraRadius);
            CameraObject.transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.up);
        }
        private void SaveButtonsAngles()
        {
            _buttonAngles = buttons.transform.rotation.eulerAngles.z;
        }
        
        private float VectorAngleZ(Vector3 a, Vector3 b, Vector3 c)
>>>>>>> Stashed changes
        {
         _buttonAngles[s] = b.transform.rotation.eulerAngles.z;
         s++;
        }
    }
    private float VectorAngleZ(Vector3 a, Vector3 b, Vector3 c)
    {
        float signedAngleA = (float)(Math.Atan2(a[1] - b[1], a[0] - b[0]) / Math.PI * 180) + 90f;
        float signedAngleB = (float)(Math.Atan2(a[1] - c[1], a[0] - c[0]) / Math.PI * 180) + 90f;
        signedAngleA = (signedAngleA < 0) ? signedAngleA + 360 : signedAngleA;
        signedAngleB = (signedAngleB < 0) ? signedAngleB + 360 : signedAngleB;
        return signedAngleA - signedAngleB;
    }
}
