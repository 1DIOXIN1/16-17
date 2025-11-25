using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private EnemyBehaviorTypes _idleBehaviorType;
    [SerializeField] private EnemyBehaviorTypes _ReactionBehaviorType;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private List<Transform> _targetsForPatrol;
    [SerializeField]private ParticleSystem _particleAfterDie;

    private Enemy _enemy;

    private void Start() 
    {
        _enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        _enemy.Initialization(ChooseBehaviorType(_idleBehaviorType), ChooseBehaviorType(_ReactionBehaviorType), _playerTransform);
    }

    private IBehavior ChooseBehaviorType(EnemyBehaviorTypes behaviorType)
    {
        switch (behaviorType)
        {
            case EnemyBehaviorTypes.Stand:
                return new StandIdle();

            case EnemyBehaviorTypes.Patrol:
                return new PatrolIdle(_enemy.transform, _targetsForPatrol);

            case EnemyBehaviorTypes.RandomWalk:
                return new RandomWalkIdle(_enemy.transform);

            case EnemyBehaviorTypes.RunTo:
                return new RunToReaction(_enemy.transform, _playerTransform);

            case EnemyBehaviorTypes.RunOut:
                return new RunOutReaction(_enemy.transform, _playerTransform);

            case EnemyBehaviorTypes.ScaredAndDie:
                return new ScaredAnDieReaction(_enemy.GetComponent<Collider>(), _enemy.GetComponent<MeshRenderer>(), _particleAfterDie);

            default:
                return new StandIdle();
        }
    }
}
