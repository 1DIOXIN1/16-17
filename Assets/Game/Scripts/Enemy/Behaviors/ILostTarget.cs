using UnityEngine;

public interface ILostTarget : IBehavior
{
    void SetLastSeenPosition(Vector3 position);
    void End();
    bool IsFinished { get; }
}
