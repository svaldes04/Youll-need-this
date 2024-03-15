using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour{
    private Rigidbody2D thisRigidBody;
    public GameObject myBullet;
    public GameObject nextBat;
    public float speed;
    public float changeTime;
    public float shootTime;
    private bool needsChange;
    private bool needsShoot;
    private Vector3 change = Vector3.zero;
    

    // Start is called before the first frame update
    void Start()
    {
        thisRigidBody = GetComponent<Rigidbody2D>();
        change.y = -1;
        needsChange = true;
        needsShoot = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if(needsChange){
            needsChange = false;
            StartCoroutine(changeDirection());
        }
        thisRigidBody.MovePosition(transform.position + change * speed * Time.deltaTime);

        if(needsShoot){
            needsShoot = false;
            StartCoroutine(shoot());
       }
    }

    void OnTriggerEnter2D(Collider2D target){
         if(target.gameObject.tag == "PBullet")
        {
            GameObject.Instantiate(nextBat, new Vector3(Random.Range(-7,-12), 4f, 0), transform.rotation);
            
            if(Random.Range(1,5) == 4){
                GameObject.Instantiate(nextBat, new Vector3(Random.Range(-7,-12), 4f, 0), transform.rotation);
            }
            
            Destroy(gameObject);
        }
    }

    IEnumerator shoot(){
        yield return new WaitForSeconds(shootTime);
        GameObject.Instantiate(myBullet, transform.position + new Vector3(0.3f, -0.3f, 0), transform.rotation);
        needsShoot = true;
    }

    IEnumerator changeDirection(){
        yield return new WaitForSeconds(changeTime);
        change *= -1;
        needsChange = true;
    }
}
