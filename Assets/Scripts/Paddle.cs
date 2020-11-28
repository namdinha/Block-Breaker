using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minXPos = 1f;
    [SerializeField] float maxXPos = 15f;

    Ball ball;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start() {
        ball = FindObjectOfType<Ball>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update() {
        UpdatePosition();
    }

    private void UpdatePosition() {
        Vector2 paddlePosition = new Vector2(transform.position.x, transform.position.y);
        paddlePosition.x = Mathf.Clamp(GetXPos(), minXPos, maxXPos);
        transform.position = paddlePosition;
    }

    private float GetXPos() {
        if(gameSession.IsAutoPlayEnabled()) {
            return ball.transform.position.x;
        }
        else {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
