using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class SC_ChatManager : MonoBehaviour
{
    [SerializeField]private SO_ChatRoom _chatRoom;
    [SerializeField] private TMP_InputField _inputField;

    [SerializeField] private Button _sendButton;
    [SerializeField] private Button _clearButton;

    [SerializeField] private GameObject ownMessageGameObject;
    [SerializeField] private GameObject anotherMessageGameObject;
    
    [SerializeField] private Transform chatContainer;
    
    private void Awake()
    {
        _inputField.onSubmit.AddListener(AddMessage);
        _sendButton.onClick.AddListener(() => AddMessage(_inputField.text));
        LoadHistory();
    }
    
    private void AddMessage(string messageText)
    {  
        if (messageText == string.Empty) return;
        
        _inputField.text = string.Empty;
        
        //Добавление сообщения в хранилище основанное на ScriptableObject
        Message _messageData = _chatRoom.AddMessage(messageText, _chatRoom.GetUser(_chatRoom.hostId));
        //Добавление сообщений в интерфейс
        SC_BubbleMessageView _bubbleMessage = Instantiate(ownMessageGameObject, chatContainer)
            .GetComponent<SC_BubbleMessageView>();
        //Проверка на повтор с предыдущим сообщением
        _bubbleMessage.SetData(_messageData);
       
        CheckPreviousMessage(true);
        
    }

    private void CheckPreviousMessage(bool isOwnMessage = false)
    {
        GameObject previousMessage = chatContainer.Find(_chatRoom.LoadHistory()[_chatRoom.LoadHistory().Count - 2]?.messageId.ToString())?.gameObject;
        Message previousMessageData = _chatRoom.LoadHistory()[_chatRoom.LoadHistory().Count - 2];

        if (previousMessage == null) return;
        
        if (_chatRoom.LoadHistory()?.LastOrDefault()?.user.userId ==
            previousMessageData.user.userId)
        {
            previousMessage.GetComponent<SC_BubbleMessageView>().SetData(previousMessageData, true, isOwnMessage);
        }
    }

    public void LoadHistory()
    {
        foreach (var _message in _chatRoom.LoadHistory())
        {
            if (_message.user.userId == _chatRoom.hostId)
            {
                Instantiate(ownMessageGameObject, chatContainer)
                    .GetComponent<SC_BubbleMessageView>()
                    .SetData(_message);
                CheckPreviousMessage(true);
            }
            else
            {
                Instantiate(anotherMessageGameObject, chatContainer)
                    .GetComponent<SC_BubbleMessageView>()
                    .SetData(_message);
                CheckPreviousMessage();
            }
        }
    }

    public void RemoveMessage(string text)
    {
        
    }

}
