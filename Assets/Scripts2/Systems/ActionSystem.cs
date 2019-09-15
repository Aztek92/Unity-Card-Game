using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/Systems/Action System")]
public class ActionSystem : ScriptableObject
{
    public GameEvent BeforeAttackActionEvent;
    public GameEvent AfterAttackActionEvent;

    static bool isExecuting = false;
    static Queue<Action> ActionQueue = new Queue<Action>();

    public void ExecuteAction(Action action)
    {
        AddToQueue(action);
    }

    public void AddToQueue(Action action)
    {
        ActionQueue.Enqueue(action);
        if (!isExecuting)
            ExecuteNextAction();
    }

    public void ExecuteNextAction()
    {
        isExecuting = true;
        Action action = ActionQueue.Dequeue();
        RaiseBeforeEvent(action);
        action.Execute();
        RaiseAfterEvent(action);
        CompleteExecution();
    }

    public void CompleteExecution()
    {
        if(ActionQueue.Count > 0)
        {
            ExecuteNextAction();
        }
        else
        {
            isExecuting = false;
        }
    }

    public void RaiseBeforeEvent(Action action)
    {
        if(action is AttackAction)
        {
            BeforeAttackActionEvent.RaiseActionEvent(action);
        }
    }

    public void RaiseAfterEvent(Action action)
    {
        if(action is AttackAction)
        {
            AfterAttackActionEvent.RaiseActionEvent(action);
        }
    }
}