using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 300f;
    [SerializeField] float rotationThrust = 300f; 
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;

    [SerializeField] ParticleSystem mainBooster;

    Rigidbody rb;
    AudioSource audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();

        }
        else
        {
            StopThrusting();
        }

    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainBooster.Stop();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ActivateRightBooster();

        }
        else if (Input.GetKey(KeyCode.D))
        {
            ActivateLeftBooster();
        }
        else
        {
            StopBoosting();
        }
    }

    private void StopBoosting()
    {
        rightBooster.Stop();
        leftBooster.Stop();
    }

    private void StartThrusting()
    {
        Debug.Log("Pressed Space");
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }

    void ActivateLeftBooster()
    {
        ApplyRotation(-rotationThrust);
        if (!leftBooster.isPlaying)
        {
            leftBooster.Play();
        }
        rightBooster.Stop();
    }

    void ActivateRightBooster()
    {
        ApplyRotation(rotationThrust);

        if (!rightBooster.isPlaying)
        {
            rightBooster.Play();
        }

        leftBooster.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
