using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class TrophyInteraction : MonoBehaviour
{
    public Transform player;
    public float interactionRange = 10f;
    public GameObject tooltipText;
    public Animator animator;
    public GameObject handTrophy;

    void Start()
    {
        if(PlayerPrefs.GetInt("won", 0) == 1)
        {
            handTrophy.SetActive(true);
            animator.SetBool("Trophy", true);
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= interactionRange)
        {
            tooltipText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetBool("Trophy", true);
                handTrophy.SetActive(true);
                PlayerPrefs.SetInt("won", 1);
                tooltipText.SetActive(false);
                ChatSystem.Instance.sendMessage("System", "You won yayayay!!");
                Destroy(gameObject);
            }
        }
        else
        {
            tooltipText.SetActive(false);
        }
    }
}