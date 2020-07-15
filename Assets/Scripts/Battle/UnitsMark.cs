using Battle.Units;
using UnityEngine;

namespace Battle
{
    public class UnitsMark : MonoBehaviour
    {
        private Manager Manager { get => Manager.Instance; }
        public UnitStatus CurrentUnit { get => Manager.currentUnit; }

        private void Start() { }

        public void Create(UnitStatus currentUnit, string path = "test2")
        {
            GameObject go = currentUnit.parent;

            GameObject turnMark = new GameObject("Circle");
            turnMark.transform.parent = go.transform;
            turnMark.transform.position = new Vector3(
                go.transform.position.x,
                go.transform.position.y - 0.75f,
                go.transform.position.z
            );
            turnMark.transform.rotation = Quaternion.Euler(90, 0, 0);
            turnMark.tag = "TurnMark";

            SpriteRenderer spriteRenderer = turnMark.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = Resources.Load<Sprite>(path);
        }

        public void DestroyTurnMark()
        {
            GameObject[] marks = GameObject.FindGameObjectsWithTag("TurnMark");
            foreach (GameObject mark in marks)
            {
                Destroy(mark);
            }
        }
    }
}
