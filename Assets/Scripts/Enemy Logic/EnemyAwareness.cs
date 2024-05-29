using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAwareness : MonoBehaviour
{
    public bool AwareofPlayer { get; private set; }
    public Vector3 DirectionOfPlayer { get; private set; }

    public Vector3 DesiredDir { get; private set; }

    [SerializeField] float awarenessDistance;
    private Transform player;

    [Header("Avoid Each Other")]
    public float enemyRadius = 10;
    public float avoidPower = 4;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 avoidDir = new();
        Transform[] nearbyEnemies = NearbyEnemyCheck();
        foreach (var enemy in nearbyEnemies)
        {
            Vector3 enemyToThis = enemy.position - transform.position;
            Vector3 dir = enemyToThis.normalized;
            avoidDir += dir * (1 - enemyToThis.magnitude / enemyRadius % 1);
        }

        Vector3 enemyToPlayerVector = player.position - transform.position;
        DirectionOfPlayer = enemyToPlayerVector.normalized;

        DesiredDir = Vector3.Normalize(DirectionOfPlayer + avoidPower * avoidDir.normalized);
        

        AwareofPlayer = (enemyToPlayerVector.magnitude <= awarenessDistance);

        // Old convoluted way of as above - redundant to say it's true/false when it's true
        //if (enemyToPlayerVector.magnitude <= awarenessDistance)
        //{
        //    AwareofPlayer = true;
        //}
        //
        //else
        //{
        //    AwareofPlayer = false;
        //}

    }

    Transform[] NearbyEnemyCheck()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemyRadius);
        List<Transform> transforms = new List<Transform>();
        foreach (var item in colliders)
        {
            // Any component can just getcomponent on without having to gameobk or whatever ty Ethan
            if (item.TryGetComponent<EnemyAwareness>(out EnemyAwareness component))
            {
                if (component == this)
                    continue;
                transforms.Add(item.transform);
            }
        }
        return transforms.ToArray();
    }

}