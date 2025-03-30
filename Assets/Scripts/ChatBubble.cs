using UnityEngine;
using TMPro;

public class ChatBubble : MonoBehaviour
{
    public TMP_Text message;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void init(string sender, string msg)
    {
        message.text = string.Format("{0} : {1}",sender,msg);
    }
}
