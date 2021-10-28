using GamePlayHexes;
using GamePlayHexes.DiceClasses;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Dice), true)]
    public class DiceFix : UnityEditor.Editor
    {
        private Dice _dice;
        private void OnEnable()
        {
            _dice = (Dice) target;
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            //Placing the dice in the right place
            if (GUILayout.Button("Right place"))
            {
                float xm = Mathf.Cos(2 * Mathf.PI / 6 * 0.5f);
                float step = VarManager.step;
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
}
