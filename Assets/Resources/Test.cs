using UnityEngine;

public class Test : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.forward * 50 * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.name);
        Debug.Log(BattleManager.targetUnit.gameObject.name);
        if (col.gameObject.name.Contains(BattleManager.targetUnit.gameObject.name))
        {
            Debug.Log("Destroy");
            Destroy(gameObject);
        }        
    }
}
