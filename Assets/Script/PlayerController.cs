using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float jumpForce;
    public bool canJump = true;

    public float walkSpeed = 0;
    private Touch touch;
    public int score = 0;

    public TextMeshProUGUI textScore;
    public TextMeshProUGUI endScore;
    public TextMeshProUGUI hightScore;
    public TextMeshProUGUI newScore;

    public GameObject endGame;

    public string sceneName;

    private Animator animator;
    private enum State { walk , jump }
    private State state = State.walk;

    public Light lightWorld;
    public Light lightPlayer;
    public Light[] lightEvi;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        endGame.SetActive(false);
        TextScore();
        newScore.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.touchCount > 0 )
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved )
            {
                transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * walkSpeed , transform.position.y,transform.position.z);
            }
        }
        textScore.text =score.ToString();
        endScore.text = score.ToString();
        animator.SetInteger("state", (int)state);
        SetLight();
    }
    public void HightScore()
    {
        if(score > PlayerPrefs.GetInt("HightScore" , 0))
        {
            PlayerPrefs.SetInt("HightScore", score);
            newScore.gameObject.SetActive(true);
        }
    }
    void TextScore()
    {
        hightScore.text = "" + PlayerPrefs.GetInt("HightScore", 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ground")
        {
            canJump = true;
            state = State.walk;
        }

        if(other.tag == "Score")
        {
            score++;
            HightScore();
        }

        if(other.tag == "Os")
        {
            endGame.SetActive(true);
            if(Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Ground")
        {
            canJump= false;
        }
    }

    public void Jump()
    {
        if(canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            state = State.jump;
        }
    }

    public void PlayAgain()
    {
        endGame.SetActive(false);

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene(sceneName);
    }

    public void SetLight()
    {
        if(score == 0)
        {
            lightWorld.intensity = 1f;
            lightPlayer.intensity = 0f;

            for(int i = 1; i < lightEvi.Length; i++)
            {
                lightEvi[i].intensity = 0f;
            }
        }
        else if(score == 10)
        {
            lightWorld.intensity = 0.7f;
        }
        else if (score == 20)
        {
            lightWorld.intensity = 0.6f;
        }
        else if (score == 30)
        {
            lightWorld.intensity = 0.6f;
            lightPlayer.intensity = 3f;

            for (int i = 1; i < lightEvi.Length; i++)
            {
                lightEvi[i].intensity = 2f;
            }
        }
        else if (score == 45)
        {
            lightWorld.intensity = 0.4f;
            lightPlayer.intensity = 4f;

            for (int i = 1; i < lightEvi.Length; i++)
            {
                lightEvi[i].intensity = 2.5f;
            }
        }
        else if (score == 60)
        {
            lightWorld.intensity = 0.2f;
            lightPlayer.intensity = 5f;

            for (int i = 1; i < lightEvi.Length; i++)
            {
                lightEvi[i].intensity = 3f;
            }
        }
        else if(score == 100)
        {
            lightWorld.intensity = 0.1f;
            lightPlayer.intensity = 6f;

            for (int i = 1; i < lightEvi.Length; i++)
            {
                lightEvi[i].intensity = 4.5f;
            }
        }

    }
}
