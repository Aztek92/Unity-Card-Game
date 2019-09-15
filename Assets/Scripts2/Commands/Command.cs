using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Command
{
    public static Queue<Command> CommandQueue = new Queue<Command>();
    public static bool isPlaying = false;

    public virtual void AddToQueue()
    {
        CommandQueue.Enqueue(this);
        if (!isPlaying)
            PlayNextCommand();
    }

    public virtual void Execute() {
        Complete();
    }

    public static void Complete()
    {
        if(CommandQueue.Count > 0)
        {
            PlayNextCommand();
        }
        else
        {
            isPlaying = false;
        }
    }

    public static void PlayNextCommand()
    {
        isPlaying = true;
        CommandQueue.Dequeue().Execute();
    }
}