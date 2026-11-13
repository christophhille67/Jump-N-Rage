using Unity.Behavior;
using UnityEngine;

// Finds the player in the scene and hands it to the guard's behavior graph.
// Needed because a prefab can't store a reference to an object in a scene.
[RequireComponent(typeof(BehaviorGraphAgent))]
public class GuardPlayerFinder : MonoBehaviour
{
    [SerializeField] private string playerVariableName = "Player";  // name in the blackboard

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("GuardPlayerFinder: no object tagged 'Player' in the scene.", this);
            return;
        }

        GetComponent<BehaviorGraphAgent>().SetVariableValue(playerVariableName, player);
    }
}