using UnityEngine;

public class Test : MonoBehaviour
{
    void Update()
    {
        float step = 50 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, BattleManager.targetUnit.gameObject.transform.position, step);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == BattleManager.targetUnit.gameObject)
        {
            GameObject.Find("MainCamera").GetComponent<BattleAnimation>().HitStart();
            BattleManager.currentUnit.gameObject.GetComponent<Personage>().Attack();
            Destroy(gameObject);
        }        
    }
}
