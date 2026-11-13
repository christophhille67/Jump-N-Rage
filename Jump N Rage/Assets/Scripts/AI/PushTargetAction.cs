using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

// An action node for the behavior graph: push the target away from the agent
[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Push Target",
                 story: "[Agent] pushes [Target] with [Force]",
                 category: "Action/Combat",
                 id: "3e8d1c6a9b2f4a7e8c5d0f1b2a3e4d5c")]
public partial class PushTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> Force;

    // Runs once when the graph reaches this node
    protected override Status OnStart()
    {
        if (Agent.Value == null || Target.Value == null)
            return Status.Failure;

        // Only works if the target is our player
        if (!Target.Value.TryGetComponent(out PlayerController player))
            return Status.Failure;

        // Direction from the guard to the player, flat on the ground
        Vector3 away = Target.Value.transform.position - Agent.Value.transform.position;
        away.y = 0f;
        away.Normalize();

        // Push away and a bit upwards, so the player flies off the platform
        player.ApplyKnockback(away * Force.Value + Vector3.up * Force.Value * 0.5f);

        return Status.Success;
    }
}