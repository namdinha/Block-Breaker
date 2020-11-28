using UnityEngine;

public class Ball : MonoBehaviour {
    // config
    [SerializeField] Paddle paddle1;
    [SerializeField] float velX = 2f;
    [SerializeField] float velY = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    // state
    Vector2 paddleToBallVector;
    bool hasStarted;

    // cached component refs
    AudioSource myAudioSource;
    Rigidbody2D myRigidbody2D;

    void Start() {
        paddleToBallVector = transform.position - paddle1.transform.position;
        hasStarted = false;
        myAudioSource = GetComponent<AudioSource>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if(!hasStarted) {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick() {
        if(Input.GetMouseButtonDown(0)) {
            myRigidbody2D.velocity = new Vector2(velX, velY);
            hasStarted = true;
        }
    }

    private void LockBallToPaddle() {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Vector2 velocityTweak = new Vector2(
                                    Random.Range(0f, randomFactor), 
                                    Random.Range(0f, randomFactor));
        if(hasStarted) {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidbody2D.velocity += velocityTweak;
        }
    }
}
