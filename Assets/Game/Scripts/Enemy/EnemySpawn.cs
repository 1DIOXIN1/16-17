using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private EnemyIdleTypes _idleType;
    [SerializeField] private EnemyReactionTypes _reactionType;

    [SerializeField] private Transform _playerTransform;

    [SerializeField] private List<Transform> _targetsForPatrol;

    private Enemy _enemy;

    [SerializeField]private ParticleSystem _particleAfterDie;

    private void Start() 
    {
        _enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        _enemy.Initialization(ChooseIdleType(), ChooseReactionType(), _playerTransform);
    }

    private IIdleBehavior ChooseIdleType()
    {
        switch (_idleType)
        {
            case EnemyIdleTypes.Stand:
                return new StandIdle();

            case EnemyIdleTypes.Patrol:
                return new PatrolIdle(_enemy.transform, _targetsForPatrol);

            case EnemyIdleTypes.RandomWalk:
                return new RandomWalkIdle(_enemy.transform);

            default:
                return new StandIdle();
        }
    }

    private IReactionBehavior ChooseReactionType()
    {
        switch (_reactionType)
        {
            case EnemyReactionTypes.RunTo:
                return new RunToReaction(_enemy.transform, _playerTransform);

            case EnemyReactionTypes.RunOut:
                return new RunOutReaction(_enemy.transform, _playerTransform);

            case EnemyReactionTypes.ScaredAndDie:
                return new ScaredAnDieReaction(_enemy.GetComponent<Collider>(), _enemy.GetComponent<MeshRenderer>(), _particleAfterDie);

            default:
                return new RunOutReaction(_enemy.transform, _playerTransform);
        }

    }

}
