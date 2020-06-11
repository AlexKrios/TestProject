using UnityEngine;

public class BattleAnimation : MonoBehaviour
{
    private Animator _currentAnimator;
    private Animator _targetAnimator;

    private UnitStatus _currentUnit;
    private UnitStatus _targetUnit;

    void Start() { }

    public void TurnStart()
    {        
        _currentAnimator = BattleManager.currentUnit.gameObject.GetComponent<Animator>();
        _currentAnimator.SetBool("isTurn", true);
    }

    public void TurnEnd()
    {
        _currentAnimator = BattleManager.currentUnit.gameObject.GetComponent<Animator>();
        _currentAnimator.SetBool("isTurn", false);
    }

    public void TurnAttackStart()
    {
        _currentUnit = BattleManager.currentUnit;
        _targetUnit = BattleManager.targetUnit;

        float speed = BattleManager.globalSpeed * 6;

        _currentAnimator = _currentUnit.gameObject.GetComponent<Animator>();
        _currentAnimator.SetBool("isAttack", true);        

        Vector3 currentPos = _currentUnit.parent.transform.position;
        Quaternion currentRot = _currentUnit.parent.transform.rotation;
        Vector3 targetPos = _targetUnit.parent.transform.position;

        if (_currentUnit.gameObject == _targetUnit.gameObject)
        {
            return;
        }

        _currentUnit.parent.transform.rotation = Quaternion.Slerp(
            currentRot,
            Quaternion.LookRotation(targetPos - currentPos), 
            Time.deltaTime * speed
        );
    }

    public void TurnAttackEnd()
    {        
        _currentAnimator = BattleManager.currentUnit.gameObject.GetComponent<Animator>();
        _currentAnimator.SetBool("isAttack", false);
    }

    public void HitStart()
    {
        _targetAnimator = BattleManager.targetUnit.gameObject.GetComponent<Animator>();
        _targetAnimator.SetBool("isHit", true);
    }

    public void HitEnd()
    {
        _targetAnimator = BattleManager.targetUnit.gameObject.GetComponent<Animator>();
        _targetAnimator.SetBool("isHit", false);
    }

    public void Dead()
    {
        _targetAnimator = BattleManager.targetUnit.gameObject.GetComponent<Animator>();
        _targetAnimator.SetBool("isHit", false);

        if (BattleManager.targetUnit.status == "Dead")
        {
            _targetAnimator.SetBool("isDead", true);
        }        
    }

    public void DeadEnd()
    {
        _targetAnimator.SetBool("isTurn", false);
    }

    public void ResetTrigger()
    {
        _currentAnimator = BattleManager.currentUnit.gameObject.GetComponent<Animator>();
        _targetAnimator = BattleManager.targetUnit.gameObject.GetComponent<Animator>();

        _currentAnimator.SetBool("isAttack", false);
        _targetAnimator.SetBool("isHit", false);
    }
}
