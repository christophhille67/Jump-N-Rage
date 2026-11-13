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

    // The graph calls this to ask "is the condition true right now?"
    public override bool IsTrue()
    {
        if (Agent.Value == null || Target.Value == null)
            return false;

        float distance = Vector3.Distance(Agent.Value.transform.position,
                                          Target.Value.transform.position);
        return distance <= Range.Value;
    }
}