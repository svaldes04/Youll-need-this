using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform attachedTarget;    // object that gets followed by the camera
    public float smoothing;
    private AudioSource audio;
    public AudioClip deathSound;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    //occurs at the end of the update
    void LateUpdate() {
        if(attachedTarget != null){
            if (transform.position != attachedTarget.position) {
                Vector3 targetPosition = new Vector3(attachedTarget.position.x, attachedTarget.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
            }
        } else {
            attachedTarget = GameObject.FindGameObjectWithTag("Title").transform;
            audio.Stop();
            audio.clip = deathSound;
            audio.loop = false;
            audio.Play();
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame(){
        yield return new WaitForSeconds(13f);
        Application.Quit();
        Debug.Log("Quitted");
    }
}