using BBUnity.Conditions;
using Pada1.BBCore;
using UnityEngine;

[Condition("Conditions/IsCloseToEnemy")]
public class IsCloseToEnemy : GOCondition
{
    public override bool Check()
    {
        GameObject enemy = GameObject.FindWithTag("Enemy");
        return (gameObject.transform.position - enemy.transform.position).sqrMagnitude < 5.0f * 5.0f;
    }
}