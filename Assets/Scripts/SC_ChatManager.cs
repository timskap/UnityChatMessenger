using System;
using System.Collections;
using System.Collections.Generic;
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
        //Добавление сообщения в хранилище основанное на ScriptableObject
        Message _messageData = _chatRoom.AddMessage(messageText, _chatRoom.GetUser(_chatRoom.hostId));
        //Добавление сообщений в интерфейс
        Instantiate(ownMessageGameObject, chatContainer)
            .GetComponent<SC_BubbleMessageView>()
            .SetData(_messageData);
        _inputField.text = string.Empty;
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
            }
            else
            {
                Instantiate(anotherMessageGameObject, chatContainer)
                    .GetComponent<SC_BubbleMessageView>()
                    .SetData(_message);
            }
            
            
        }
    }

    public void RemoveMessage(string text)
    {
        
    }

}
