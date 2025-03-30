using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ChatSystem : MonoBehaviour
{
    public GameObject chatBubble;
    public TMP_InputField chatInputField;
    public Transform content;
    public GameObject chatPanel;
    public static ChatSystem Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
        DeactivateChatPanel();
    }

    public void sendMessage(string sender, string msg)
    {
        ActivateChatPanel();
        GameObject message = Instantiate(chatBubble, content);
        message.GetComponent<ChatBubble>().init(sender, msg);
    }

    public void OnSendButtonHit()
    {
        sendMessage("You", chatInputField.text);
        chatInputField.text = "";
    }

    public void ActivateChatPanel()
    {
        chatPanel.SetActive(true);
    }

    public void DeactivateChatPanel()
    {
        chatPanel.SetActive(false);
    }

}
