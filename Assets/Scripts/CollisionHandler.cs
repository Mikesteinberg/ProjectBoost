using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip failure;
    [SerializeField] ParticleSystem particleSuccess;
    [SerializeField] ParticleSystem particleFailure; 

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (isTransitioning) {return;}

        switch (collisionInfo.gameObject.tag) 
        {
            case "Friendly":
                Debug.Log("Collided With Friendly Object");
                break;
            case "Finish":
                Debug.Log("Collided With Finish Object");
                StartSuccessSequence();
                break;
            default:
                Debug.Log("Collided With Other Object");
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence() 
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);

        audioSource.Stop();
        audioSource.PlayOneShot(failure);

        particleFailure.Play();
    }

    void StartSuccessSequence() 
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", levelLoadDelay);
        audioSource.Stop();
        audioSource.PlayOneShot(success);

        particleSuccess.Play();
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        isTransitioning = false;
    }

    void NextLevel() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentSceneIndex + 1;
        if (nextLevelIndex >= SceneManager.sceneCountInBuildSettings) 
        {
            nextLevelIndex = 0;
        }
        SceneManager.LoadScene(nextLevelIndex);
        isTransitioning = false;
    }

}
