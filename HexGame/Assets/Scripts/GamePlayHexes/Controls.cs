using System;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePlayHexes
{
    public class Controls : MonoBehaviour
    {
        [SerializeField] private SceneController sceneController;
        
        
        [SerializeField] private GameObject buttons;

        [SerializeField] private float cameraRadius = 150f;
        [SerializeField] private float cameraHeight = 50f;
        [SerializeField] private float cameraAngle = 45f;
        
        public static GameObject cameraObject;
        private Vector3 _startMousePos;
        private float _buttonAngles;
        private bool _drag;

        public static void SetCamera()
        {
            cameraObject = GameObject.FindGameObjectWithTag("Camera");
        }

        public Animator GetAnimator()
        {
            return transform.GetComponent<Animator>();
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

        public void HideShowDices()
        {
            
        }
        public void MakeMove(int dir)
        {
            if (!_drag)
            {
                GameObject.FindGameObjectWithTag("Wizard").transform.GetChild(0).GetComponent<Animator>().SetTrigger("move");
                GameObject.FindGameObjectWithTag("Wizard").transform.GetChild(1).GetComponent<Animator>().SetTrigger("move" + (dir+1));
                VarManager.MakeMove(dir);
            }
        }

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
            cameraObject.transform.position = new Vector3((float)Math.Cos(2*Math.PI/360 * (angle-90f)) * cameraRadius, cameraHeight, -(float) Math.Sin(2*Math.PI/360 * (angle-90f)) * cameraRadius);
            cameraObject.transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.up);
        }
        private void SaveButtonsAngles()
        {
            _buttonAngles = buttons.transform.rotation.eulerAngles.z;
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
}