using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SC_ChatManager : MonoBehaviour
{
    [SerializeField]private SO_ChatRoom _chatRoom;
    [SerializeField] private TMP_InputField _inputField;

    [SerializeField] private Button _sendButton;
    [SerializeField] private Button _clearButton;
    [SerializeField] private Button _doneButton;

    [SerializeField] private GameObject ownMessageGameObject;
    [SerializeField] private GameObject anotherMessageGameObject;
    
    [SerializeField] private Transform chatContainer;

    [SerializeField] private GameObject MessagePanelGameObject;
    [SerializeField] private GameObject RemovePanelGameObject;

    private List<Message> _currentMessages;
    
    private void Awake()
    {
        _currentMessages = new List<Message>();
        _inputField.onSubmit.AddListener(AddMessage);
        _sendButton.onClick.AddListener(() => AddMessage(_inputField.text));
        _clearButton.onClick.AddListener(() => OpenRemovePanel(true));
        _doneButton.onClick.AddListener(() => OpenRemovePanel(false));
        LoadHistory();
    }
    
    /// <summary>
    /// Добавление сообщения в Canvas
    /// </summary>
    /// <param name="messageText"></param>
    
    private void AddMessage(string messageText)
    {  
        if (messageText == string.Empty) return;
        
        _inputField.text = string.Empty;
        
        
        //Добавление сообщения в хранилище основанное на ScriptableObject
        Message _messageData = _chatRoom.AddMessage(messageText, _chatRoom.GetRandomUser());
        //Добавление сообщений в интерфейс
        SC_BubbleMessageView _bubbleMessage = Instantiate(ownMessageGameObject, chatContainer)
            .GetComponent<SC_BubbleMessageView>();


        _currentMessages.Add(_messageData);
        DataToMessageBubble(_messageData);
        //Проверка на автора предыдущего с предыдущения
        CheckPreviousMessage(true);
        chatContainer.transform.GetComponent<RectTransform>().position = new Vector3(0, 234, 0);
    }

    private void CheckPreviousMessage(bool isOwnMessage = false)
    {    
        if (_currentMessages.Count <= 1) return;
        GameObject previousMessage = chatContainer.Find(_currentMessages[_currentMessages.Count - 2]?.messageId.ToString())?.gameObject;
        Message previousMessageData = _currentMessages[_currentMessages.Count - 2];

        if (previousMessage == null) return;
        
        if (_currentMessages.LastOrDefault()?.user.userId ==
            previousMessageData.user.userId)
        {
            previousMessage.GetComponent<SC_BubbleMessageView>().SetData(previousMessageData, true, isOwnMessage: isOwnMessage, _chatRoom);
        }
       
    }
    
    public void LoadHistory()
    {
        //Загрузка сообщений из истории
        List<Message> _chatHistory = _chatRoom.LoadHistory();
        foreach (var _message in _chatHistory)
        {
            _currentMessages.Add(_message);
            DataToMessageBubble(_message);

        }
    }

    public void DataToMessageBubble(Message _message)
    {
        if (_message.user.userId == _chatRoom.hostId)
        {
            Instantiate(ownMessageGameObject, chatContainer)
                .GetComponent<SC_BubbleMessageView>()
                .SetData(_message,  _chatRoom: _chatRoom);
            CheckPreviousMessage(true);
        }
        else
        {
            Instantiate(anotherMessageGameObject, chatContainer)
                .GetComponent<SC_BubbleMessageView>()
                .SetData(_message,  _chatRoom: _chatRoom);
            CheckPreviousMessage();
        }
    } 
    
    /// <summary>
    /// Открытие и закрытие панели с удалением сообщений
    /// </summary>
    /// <param name="isOpen"></param>

    private void OpenRemovePanel(bool isOpen)
    {
        RemovePanelGameObject.SetActive(isOpen);
        MessagePanelGameObject.SetActive(!isOpen);
        
        //Workaround для ререндеринга Layout
        var _verticalLayoutGroup = chatContainer.GetComponent<VerticalLayoutGroup>();
        _verticalLayoutGroup.spacing = 55;
        _verticalLayoutGroup.spacing = 54;
        

        if (isOpen)
        {
            foreach (var _message in _chatRoom.GetOwnMessages())
            {
               var messageGameObject = chatContainer.Find(_message.messageId.ToString())?.gameObject;
               messageGameObject.GetComponent<SC_BubbleMessageView>().ShowRemoveButton(true);
            }
        }
        else
        {
            foreach (var _message in _chatRoom.GetOwnMessages())
            {
                var messageGameObject = chatContainer.Find(_message.messageId.ToString())?.gameObject;
                messageGameObject.GetComponent<SC_BubbleMessageView>().ShowRemoveButton(false);
            }
        }
    }

}
