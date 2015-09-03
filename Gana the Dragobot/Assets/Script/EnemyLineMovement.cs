using UnityEngine;
using System.Collections;

public class EnemyLineMovement : MonoBehaviour {

    private Transform enemyTransform;
    public float speed = 1f;

    private float direction = 1f;
    public bool reverseDirection;

    void Start()
    {
        enemyTransform = GetComponent<Transform>();
        if (reverseDirection)
        {
            direction = -1f;
        }
    }

    void Update()
    {
        enemyTransform.Translate(direction * -Vector3.forward * Time.deltaTime * speed);
    }
}