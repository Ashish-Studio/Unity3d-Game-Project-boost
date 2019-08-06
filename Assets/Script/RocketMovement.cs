using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketMovement : MonoBehaviour

{
    Rigidbody Rocketrigidbody;                          //rigidbody which is attached to the our main player (The rocket).
    public float RocketThrust=100f;                     // rocket thrust for smooth movement
    [SerializeField] float mainthrust=100f;
    AudioSource audioSource;
    enum State {  alive,transcending, dead}
    State state = State.alive;
    [SerializeField] AudioClip mainengine;
    [SerializeField] AudioClip Finish;
    [SerializeField] AudioClip Death;
    public int levelno;

    [SerializeField] ParticleSystem RocketThrustParticle;
    [SerializeField] ParticleSystem RocketDeathParticle;
    [SerializeField] ParticleSystem RocketFinishParticle;

    // Start is called before the first frame update
    void Start()
    {
        levelno = SceneManager.GetActiveScene().buildIndex;
        print(levelno);
        Rocketrigidbody =GetComponent<Rigidbody>();//getting rigidbody component for unity 
        audioSource = GetComponent<AudioSource>();
           
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.alive)
        {
            Rocketmovement();                                   //movement method right and left movement
            thrust();
        }// for upward movement
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.alive)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "friend": ;
                break;
            case "Finish":
                FinishSequence();
                break;
            case "Enemy":
                deathSequence();
                break;
            default:
                break;
        }
    }

    private void deathSequence()
    {
        state = State.dead;
        audioSource.Stop();
        audioSource.PlayOneShot(Death);
        RocketDeathParticle.Play();
        Invoke("loadlevel", 1f);
    }

    private void FinishSequence()
    {
        state = State.transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(Finish);
        RocketFinishParticle.Play();
        Invoke("loadnextlevel", 1f);
    }

    private  void loadlevel()
    {
        
            SceneManager.LoadScene(levelno);
    }

    private  void loadnextlevel()
    {
        SceneManager.LoadScene(levelno+1);
    }

    private void thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Rocketrigidbody.AddRelativeForce(Vector3.up*mainthrust);   //adding relative force which will help our rocket to takeoff
           
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(mainengine);
                }
            RocketThrustParticle.Play();
            
        }
        else
        {
            audioSource.Stop();
            RocketThrustParticle.Stop();
        }
    }

    private void Rocketmovement()
    {
        Rocketrigidbody.freezeRotation = true;                          //freeze the roctation for better  manunal control
        float rotationspeed = RocketThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward*rotationspeed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-Vector3.forward*rotationspeed);
        }
        Rocketrigidbody.freezeRotation = false;
    }
}
