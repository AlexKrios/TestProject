using UnityEngine;

public class BattleAnimation : MonoBehaviour
{
    private Animator animator;
    private UnitStatus _currentUnit;
    private UnitStatus _targetUnit;

    void Start() { }

    public void CameraStart()
    {
        var animator = GameObject.Find("MainCamera").GetComponent<Animator>();
        animator.SetBool("isStart", true);
    }

    public void TurnStart()
    {
        animator = BattleManager.currentUnit.gameObject.GetComponent<Animator>();
        animator.SetBool("isTurn", true);
    }

    public void TurnEnd()
    {
        animator = BattleManager.currentUnit.gameObject.GetComponent<Animator>();
        animator.SetBool("isTurn", false);
    }

    public void Turn()
    {
        _currentUnit = BattleManager.currentUnit;
        _targetUnit = BattleManager.targetUnit;

        float speed = BattleManager.globalSpeed * 6;

        Vector3 currentPos = _currentUnit.gameObject.transform.position;
        Quaternion currentRot = _currentUnit.gameObject.transform.rotation;
        Vector3 targetPos = _targetUnit.gameObject.transform.position;

        _currentUnit.gameObject.transform.rotation = Quaternion.Slerp(
            currentRot,
            Quaternion.LookRotation(targetPos - currentPos), 
            Time.deltaTime * speed
        );

        animator = BattleManager.currentUnit.gameObject.GetComponent<Animator>();
        animator.SetBool("isAttack", true);
    }
}
