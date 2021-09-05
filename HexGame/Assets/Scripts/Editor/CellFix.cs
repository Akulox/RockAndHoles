using CellClasses;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Cell), true)]
public class CellFix : Editor
{
    private Cell _cell;

    private void OnEnable()
    {
        _cell = (Cell) target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Right place"))
        {
            float xm = Mathf.Cos(2 * Mathf.PI / 6 * 0.5f);
            float step = _cell.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<VarManager>().step;
            _cell.gameObject.transform.position = 
                new Vector3(
                -step * xm * _cell.col, 
                1f,
                -step * _cell.row + step/2 * _cell.col
            );
            _cell.gameObject.transform.name = $"{_cell.row}_{_cell.col}";
        }
    }
}
