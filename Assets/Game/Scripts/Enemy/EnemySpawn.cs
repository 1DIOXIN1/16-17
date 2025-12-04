using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private EnemyBehaviorTypes _idleBehaviorType;
    [SerializeField] private EnemyBehaviorTypes _ReactionBehaviorType;
    [SerializeField] private EnemyLostTargetBehaviorTypes _LostTargetBehaviorType;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private List<Transform> _targetsForPatrol;
    [SerializeField]private ParticleSystem _particleAfterDie;

    private Enemy _enemy;

    private void Start() 
    {
        _enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        _enemy.Initialization(ChooseBehaviorType(_idleBehaviorType), ChooseBehaviorType(_ReactionBehaviorType), ChooseLastTargetBehaviorType(_LostTargetBehaviorType), _playerTransform);
    }

    private IBehavior ChooseBehaviorType(EnemyBehaviorTypes behaviorType)
    {
        switch (behaviorType)
        {
            case EnemyBehaviorTypes.Stand:
                return new StandIdle();

            case EnemyBehaviorTypes.Patrol:
                return new PatrolIdle(_enemy.transform, _targetsForPatrol);
            
            case EnemyBehaviorTypes.PatrolNavMesh:
                return new PatrolIdleNavMesh(_enemy.GetComponent<NavMeshAgent>(), _targetsForPatrol);

            case EnemyBehaviorTypes.RandomWalk:
                return new RandomWalkIdle(_enemy.transform);

            case EnemyBehaviorTypes.RunTo:
                return new RunToReaction(_enemy.transform, _playerTransform);

            case EnemyBehaviorTypes.RunOut:
                return new RunOutReaction(_enemy.transform, _playerTransform);

            case EnemyBehaviorTypes.ScaredAndDie:
                return new ScaredAnDieReaction(_enemy.GetComponent<Collider>(), _enemy.GetComponent<MeshRenderer>(), _particleAfterDie);
            
            case EnemyBehaviorTypes.LostTarget:
                return new RunToNavMeshAgentLostTarget(_enemy.GetComponent<NavMeshAgent>());
            
            case EnemyBehaviorTypes.RunToNavMesh:
                return new RunToNavMeshAgentReaction(_enemy.GetComponent<NavMeshAgent>(), _enemy.transform, _playerTransform);

            default:
                return new StandIdle();
        }
    }

    private ILostTarget ChooseLastTargetBehaviorType(EnemyLostTargetBehaviorTypes behaviorType)
    {
        switch (behaviorType)
        {
            case EnemyLostTargetBehaviorTypes.RunToNavMesh:
                return new RunToNavMeshAgentLostTarget(_enemy.GetComponent<NavMeshAgent>());

            default:
                return new RunToNavMeshAgentLostTarget(_enemy.GetComponent<NavMeshAgent>());
        }
    }
}
