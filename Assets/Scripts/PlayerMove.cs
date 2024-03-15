using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{   
    public float Maxhealth;
    private float health;
    public float bulletDamage;
    private bool isAlive;
    public float speed;
    private Rigidbody2D thisRigidBody;
    private Vector3 change;
    public int cooldown; // In FixedUpdate frames
    private int cooldownCounter;
    public GameObject myBullet;

    private Animator animator;
    public static Vector3 lastchange;

    public Slider healthbar;
    private AudioSource audio;
    public AudioClip hitSound;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cooldownCounter = 0;
        isAlive = true;
        healthbar = GameObject.FindGameObjectWithTag("Hp").GetComponent<Slider>();
        healthbar.value = 1;
        health = Maxhealth;
        audio = GetComponent<AudioSource>();
        audio.clip = hitSound;
    }

    // Movement runs smoother in fixed update
    void FixedUpdate()
    {
        if(isAlive){
            change = Vector3.zero;

            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");

            if(change != Vector3.zero){
                lastchange = change;
                thisRigidBody.MovePosition(transform.position + change * speed * Time.deltaTime);

                if(change.x < 0){
                    GetComponent<SpriteRenderer>().flipX = true;
                } else if(change.x > 0){
                    GetComponent<SpriteRenderer>().flipX = false;
                }

                animator.SetBool("Moving", true);
            } else {
                animator.SetBool("Moving", false);
            }

            cooldownCounter--;
            
            if(Input.GetKey(KeyCode.Z) && cooldownCounter <= 0)
            {
                animator.SetBool("Shooting", true);
                cooldownCounter = cooldown;
                
                if(lastchange.x < 0){
                    GameObject.Instantiate(myBullet, transform.position + new Vector3(-0.3f, -0.5f, 0), Quaternion.identity);
                }else{
                    GameObject.Instantiate(myBullet, transform.position + new Vector3(0.4f, -0.57f, 0), Quaternion.identity);
                }
                StartCoroutine(EndShootAnim());
            }

            if(Input.GetKeyDown(KeyCode.Escape)){
                Application.Quit();
            }

        } else {
            animator.SetBool("Dead", true);
        }
    }

    void OnTriggerEnter2D(Collider2D target){
        if(target.gameObject.tag != "PBullet")
        {
            health-=bulletDamage;
            healthbar.value = health/Maxhealth;
            
            audio.Play();
            if(health <= 0){
                healthbar.value = 0;
                isAlive = false;
                StartCoroutine(Death());
            }
        }
    }

    IEnumerator EndShootAnim(){
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Shooting", false);
    }

    IEnumerator Death(){
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
