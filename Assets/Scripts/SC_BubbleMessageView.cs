using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_BubbleMessageView : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_Text userNameText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private Image avatar;
    [SerializeField] private RectTransform BubbleTransform;
    private int messageId;
    private float minWindowWidth = 600f;

    public void SetData(Message _message)
    {
        messageText.text = _message.text;
        userNameText.text = _message.user.name;
        DateTime messageTime = Convert.ToDateTime(_message.time);
        timeText.text = messageTime.ToString("HH:mm:ss");
        messageId = _message.messageId;
        avatar.sprite = _message.user.avatar;
        gameObject.name = messageId.ToString();
    }
}
