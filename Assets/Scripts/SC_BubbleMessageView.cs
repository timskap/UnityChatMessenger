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
    [SerializeField] private Image backgroundBubbleImage;
    [SerializeField] private Sprite backgroundBubbleWithoutAvatarSprite;
    [SerializeField] private Sprite backgroundBubbleSprite;
    [SerializeField] private HorizontalLayoutGroup containerHorizontalLayoutGroup;
    [SerializeField] private VerticalLayoutGroup containerVerticalLayoutGroup;
    private int messageId;
    private float minWindowWidth = 600f;

    public void SetData(Message _message, bool isStack = false, bool isOwnMessage = false)
    {
        messageText.text = _message.text;
        userNameText.text = _message.user.name;
        DateTime messageTime = Convert.ToDateTime(_message.time);
        timeText.text = messageTime.ToString("HH:mm:ss");
        messageId = _message.messageId;
        avatar.sprite = _message.user.avatar;
        gameObject.name = messageId.ToString();

        if (isStack) HideAvatar(isOwnMessage);

    }

    private void HideAvatar(bool isOwnMessage)
    {
        avatar.gameObject.SetActive(false);
        backgroundBubbleImage.sprite = backgroundBubbleWithoutAvatarSprite;
        
        if (isOwnMessage)
        {
            containerHorizontalLayoutGroup.padding.right = 154;
        }
        else
        {
            containerHorizontalLayoutGroup.padding.left = 154;
            containerVerticalLayoutGroup.padding.left = 70;
        }

        containerHorizontalLayoutGroup.padding.bottom = -44;
    }
}
