using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    private Transform playerTransform;
    public float speed = 1f;

    private float direction = 1f;

    void Start()
    {
        playerTransform = GetComponent<Transform>();
    }

    void Update()
    {
        playerTransform.Translate(direction * Vector3.right * Time.deltaTime * speed);
    }

    public void ReverseDirection()
    {
        direction *= -1f;
    }
}