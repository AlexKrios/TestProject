using UnityEngine;

public class BattleMark : MonoBehaviour
{
    public void Create(UnitStatus currentUnit, string path = "test2") 
    {
        if (currentUnit.status == "Dead")
        {
            return;
        }

        GameObject go = currentUnit.gameObject;

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

    public void MarkedTarget(UnitStatus currentUnit)
    {
        if (currentUnit.team == "Enemy")
        {
            return;
        }

        Create(currentUnit);
        foreach (int index in currentUnit.target)
        {
            currentUnit.gameObject.GetComponent<Personage>().MarkedTarget(index);
        }
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
