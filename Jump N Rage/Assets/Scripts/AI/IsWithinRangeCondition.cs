using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;

// A condition node for the behavior graph: "Is the target within X metres of the agent?"
[Serializable, GeneratePropertyBag]
[Condition(name: "Is Within Range",
           story: "[Agent] is within [Range] m of [Target]",
           category: "Conditions",
           id: "7c3a9f2e1b4d4e6a8f0c2d5b9e1a3c47")]
public partial class IsWithinRangeCondition : Condition
{
    // These appear as fields on the node; you fill them with blackboard variables
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> Range;
    [SerializeReference] public BlackboardVariable<float> MaxHeightDifference = new BlackboardVariable<float>(2.5f);

    public override bool IsTrue()
    {
        if (Agent.Value == null || Target.Value == null)
            return false;

        Vector3 agentPos = Agent.Value.transform.position;
        Vector3 targetPos = Target.Value.transform.position;

        // Ignore targets on a different floor (above or below the guard's platform)
        if (Mathf.Abs(targetPos.y - agentPos.y) > MaxHeightDifference.Value)
            return false;

        // Measure distance on the ground only
        agentPos.y = 0f;
        targetPos.y = 0f;
        return Vector3.Distance(agentPos, targetPos) <= Range.Value;
    }   
}