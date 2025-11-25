using UnityEngine;

public class ScaredAnDieReaction : IBehavior
{
    private Collider _characterCollider;
    private MeshRenderer _characterMesh;

    private ParticleSystem _particleAfterDie;

    private bool _isDie = false;

    public ScaredAnDieReaction(Collider characterCollider, MeshRenderer characterMesh, ParticleSystem particleAfterDie) 
    {
        _particleAfterDie = particleAfterDie;
        _characterCollider = characterCollider;
        _characterMesh = characterMesh;
    }

    public void Execute()
    {
        if(_isDie)
            return;

        _isDie = true;
        
        _particleAfterDie.transform.position = _characterCollider.transform.position;
        _particleAfterDie.Play();
        
        TurnOfObject();
    }

    private void TurnOfObject()
    {
        _characterCollider.enabled = false;
        _characterMesh.enabled = false;
    }
}
