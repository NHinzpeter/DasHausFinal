using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRWalk : MonoBehaviour
{

    public Transform vrCamera;
    public AudioSource audioS;
    public float speed, gravity;
    public bool moveForward;
    public bool moveUpward;
    public bool fire1Blocked = false;
    public bool stand = true;
    public bool crouch = false;
    public bool crawl = false;
    public bool leiteranimation = false;
    public Vector3 forward;
    private CharacterController cc;
    private Rigidbody rb;
    float x=90f, y=0f;



    // Use this for initialization
    void Start()
    {
        cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Anpassen der Guckrichtung in Windows
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        x += 2*Input.GetAxis("Mouse X");
        y -= 2*Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(y,x,0);
        PlayFootsteps(Random.Range(-0.03f, 0.03f));

        //wenn ein bool gecheckt wurde, wird die entsprechende Funktion gecallt
        if (stand)
        {
            executeStand();
        }

        else if (crouch)
        {
            executeCrouch();
        }

        else if (crawl)
        {
            executeCrawl();
        }

        //Betätigen der Taste startet/stoppt die
        if (Input.GetButtonDown("Fire1"))
        {
            if(!fire1Blocked){
                moveForward = !moveForward;
            }
        }

        //Normale Lauf-Funktion
        if (moveForward)
        {
            if (rb.drag > 0f)
            {
                rb.drag = 0f;
                rb.useGravity = true;
                rb.isKinematic = true;
            }
            forward = vrCamera.TransformDirection(Vector3.forward);
            forward.y = 0;
            forward.y = forward.y - (gravity * Time.deltaTime);
            cc.Move(forward * speed * Time.deltaTime);

            //y-Wert wird immer wieder auf den festen Wert gesetzt, da Kollisionen mit den beweglichen Wänden sonst zu Fehlern führen
            if (GetComponent<GameController>().level == GameController.levelenum.l3)
            {
                transform.position = new Vector3(transform.position.x, 3.55f, transform.position.z);
            }
        }

        //vertikale Bewegung für Leiter usw
        if (moveUpward)
        {
            if (rb.drag < 1f)
            {
                rb.drag = 1f;
                rb.useGravity = false;
                rb.isKinematic = false;
            }
            if (GetComponent<GameController>().level == GameController.levelenum.l5)
            {
                if (transform.position.x < 28 && transform.position.x > 27.9f && transform.position.z < -430)
                {
                    rb.AddForce(Vector3.up * 0.7f);
                    leiteranimation = false;
                }
                else leiteranimation = true;
            }
            else rb.AddForce(Vector3.up * 0.7f);
        }
    }


    void executeStand()
    {
        Debug.Log("Player STANDING");
        cc.height = 3f;
        cc.center = new Vector3(0, -1.5f, 0);
        speed = 4;
        audioS.pitch = 0.8f;
        stand = false;
    }

    void executeCrouch()
    {
        Debug.Log("Player CROUCHED");
        cc.height = 2.2f;
        cc.center = new Vector3(0, -1.1f, 0);
        speed = 2.5f;
        audioS.pitch = 0.7f;
        crouch = false;
    }

    void executeCrawl()
    {
        Debug.Log("Player CRAWLING");
        cc.height = 0.9f;
        cc.center = new Vector3(0, -0.45f, 0);
        speed = 1.5f;
        crawl = false;
    }

    //Footsteps werden leicht unregelmäßig und je nach Geschwindigkeit verschieden laut abgespielt
    void PlayFootsteps(float rng)
    {
        if (moveForward && speed >= 4)
        {
            audioS.enabled = true;
            audioS.loop = true;
            audioS.pitch = 0.8f;
            audioS.volume = 0.10f;
            audioS.pitch += rng;
            audioS.volume += rng;
        }
        else if (moveForward && speed < 4 && speed > 2.4)
        {
            audioS.enabled = true;
            audioS.loop = true;
            audioS.pitch = 0.7f;
            audioS.volume = 0.05f;
            audioS.pitch += rng;
            audioS.volume += rng;
        }
        else if (moveForward && speed <= 2.4)
        {
            audioS.enabled = false;
        }
        else
        {
            if (leiteranimation == false) audioS.enabled = false;
            else audioS.enabled = true;
        }
    }
}