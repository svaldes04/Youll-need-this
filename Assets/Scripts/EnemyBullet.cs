using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameObject playerTransform;
    public float bulletSpeed;
    public float bulletLife;
    private Rigidbody2D thisRigidBody;
    private Vector3 myTarget;
    // Start is called before the first frame update
    void Start()
    {
        thisRigidBody = GetComponent<Rigidbody2D>();
        StartCoroutine(despawn());

        if(GameObject.FindGameObjectWithTag("Player") == null){
            Destroy(gameObject);
        } else {
            playerTransform = GameObject.FindGameObjectWithTag("Player");
            myTarget = playerTransform.transform.position - this.transform.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        thisRigidBody.MovePosition(transform.position + myTarget.normalized * bulletSpeed * Time.deltaTime);
    }

    IEnumerator despawn()
    {   
        yield return new WaitForSeconds(bulletLife);
        Destroy(gameObject);
    }
}
