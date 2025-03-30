using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    public Coin coin;

    public float crouchSpeed = 6f;
    public float walkSpeed = 12f;
    public float sprintSpeed = 24f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 4f;
    public float playerHeight = 5f;
    private float crouchingHeight = 1f;
    private float speed;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;
    bool isMoving;
    bool isCrouching = false;
    bool isTyping = false;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NewGame();
        ChatSystem.Instance.sendMessage("System", "Press 'R' to restart");
        coin.ResetScore();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isTyping)
        {
            PlayerMove();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            isTyping = true;
            ChatSystem.Instance.ActivateChatPanel();
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ChatSystem.Instance.DeactivateChatPanel();
            isTyping=false;
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Level1");
            coin.ResetScore();
        }

    }

    private void PlayerMove()
    {
        // ground check
        isGrounded = controller.isGrounded;

        // resetting to default velocity
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // creating moving vector
        Vector3 move = transform.right * x + transform.forward * z;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = crouchSpeed;
            controller.height = crouchingHeight;
            controller.center = new Vector3(0f, crouchingHeight / 2f, 0f);
            isCrouching = true;
        }
        else
        {
            speed = walkSpeed;
            controller.height = playerHeight;
            controller.center = new Vector3(0f, playerHeight / 2f, 0f);
            isCrouching = false;
        }

        // move player
        controller.Move(move * speed * Time.deltaTime);
        Debug.Log("Current Speed: " + speed);

        // check if player can jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("Jump");
        }

        // fall down
        velocity.y += gravity * Time.deltaTime;

        // executing jump
        controller.Move(velocity * Time.deltaTime);

        if (lastPosition != gameObject.transform.position && isGrounded)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        lastPosition = gameObject.transform.position;

        animator.SetBool("Moving", move.sqrMagnitude > 0);
        animator.SetBool("Crouching", isCrouching);
        animator.SetBool("Sprinting", speed == sprintSpeed);
        animator.SetBool("Grounded", isGrounded);
    }

    private void NewGame()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }
}
