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