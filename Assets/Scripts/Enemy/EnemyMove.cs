using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Transform))]
public class EnemyMove : MonoBehaviour
{
    public float Speed;
    public float MoveDownSpeed;

    private Rigidbody2D _rigidbody2D;
    private Transform _transform;

    private void AddSpeed(float speed)
    {
        Speed += speed;
    }

    // Use this for initialization
    void Start ()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();

        MoveEnemy(DIRECTION.Right);
	}

    public void Turn(DIRECTION direction)
    {
        MoveEnemy(direction);
        MoveDown();
    }

    private void MoveEnemy(DIRECTION direction)
    {
        float2 velocity = _rigidbody2D.velocity;
        float force = Speed * Time.deltaTime * 50;
        velocity.x = (int) direction * force;
        _rigidbody2D.velocity = velocity;
    }

    private void MoveDown()
    {
        float3 newPosition = new float3(_transform.position.x, _transform.position.y - MoveDownSpeed, _transform.position.z);
        _transform.position = newPosition;
    }
}
