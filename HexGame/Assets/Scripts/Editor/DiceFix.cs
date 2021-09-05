using DiceClasses;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Dice), true)]
public class DiceFix : Editor
    {
        private Dice _dice;

        private void OnEnable()
        {
            _dice = (Dice) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Right place"))
            {
                float xm = Mathf.Cos(2 * Mathf.PI / 6 * 0.5f);
                float step = _dice.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<VarManager>().step;
                _dice.gameObject.transform.position = 
                    new Vector3(
                        -step * xm * _dice.col, 
                        1f,
                        -step * _dice.row + step/2 * _dice.col
                    );
                _dice.transform.Translate(0,1.5f,0);
                _dice.gameObject.transform.name = $"{_dice.row}_{_dice.col}";
            }
        }
    }
