using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletLife;
    private Rigidbody2D thisRigidBody;
    private Vector3 myDirection = PlayerMove.lastchange;
    // Start is called before the first frame update
    void Start()
    {
        thisRigidBody = GetComponent<Rigidbody2D>();
        
        //Bullet rotation
        float angle = Vector3.Angle(Vector3.right, myDirection);
        transform.Rotate(0,0,angle* myDirection.y );

        StartCoroutine(despawn());
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        // TODO: Rotate bullet to appropiate direction... :)
        thisRigidBody.MovePosition(transform.position + myDirection * bulletSpeed * Time.deltaTime);
    }

    IEnumerator despawn()
    {   
        yield return new WaitForSeconds(bulletLife);
        Destroy(gameObject);
    }
}
