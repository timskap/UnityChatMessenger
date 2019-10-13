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
    [SerializeField] private HorizontalLayoutGroup containerHorizontalLayoutGroup;
    [SerializeField] private VerticalLayoutGroup containerVerticalLayoutGroup;
    [SerializeField] private Button removeButton;
    
    private int messageId;
    private float minWindowWidth = 600f;

    private bool Stacking;
    private bool OwnMessage;
    

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void SetData(Message _message, bool _isStack = false, bool isOwnMessage = false, SO_ChatRoom _chatRoom = null)
    {
        ShowRemoveButton(false);
        messageText.text = _message.text;
        userNameText.text = _message.user.name;
        DateTime messageTime = Convert.ToDateTime(_message.time);
        timeText.text = messageTime.ToString("HH:mm:ss");
        messageId = _message.messageId;
        avatar.sprite = _message.user.avatar;
        gameObject.name = messageId.ToString();
        
        removeButton?.onClick.AddListener(() =>
        {
            _chatRoom.RemoveMessage(_message);
            DeleteMessage();
        });

        Stacking = _isStack;
        OwnMessage = isOwnMessage;

        if (Stacking) HideAvatar(isOwnMessage);
        
        gameObject.SetActive(true);
        


    }

    public void ShowRemoveButton(bool isActive)
    {
        removeButton?.gameObject.SetActive(isActive);

        if (OwnMessage && Stacking)
        {
            containerHorizontalLayoutGroup.padding.right = 0;
            containerHorizontalLayoutGroup.spacing = 190f;
        }
        
    }

    private void DeleteMessage()
    {
        
        Destroy(gameObject);
    }

    private void HideAvatar(bool isOwnMessage)
    {
        avatar.gameObject.SetActive(false);
        backgroundBubbleImage.sprite = backgroundBubbleWithoutAvatarSprite;
        
        if (isOwnMessage)
        {
            containerHorizontalLayoutGroup.padding.right = 180;
        }
        else
        {
            containerHorizontalLayoutGroup.padding.left = 154;
            containerVerticalLayoutGroup.padding.left = 70;
        }

        containerHorizontalLayoutGroup.padding.bottom = -44;
    }
}
