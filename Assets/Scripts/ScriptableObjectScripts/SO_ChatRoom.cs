using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_ChatRoom", menuName = "ChatData/SO_ChatRoom", order = 1)]
public class SO_ChatRoom : ScriptableObject
{
    [SerializeField] private int currentMessageId;
    public int hostId;
    
    
    [SerializeField] private List<Message> _messages;
    [SerializeField] private List<SO_UserData> UsersData;
    public Message AddMessage(string textMessage, User _user)
    {
        Message _tempMessageData = new Message
        {
           text = textMessage,
           messageId = currentMessageId,
           user = _user,
           time =  DateTime.Now.ToString()
        };
        _messages.Add(_tempMessageData);
        currentMessageId += 1;
        return _tempMessageData;
    }
    

    public User GetUser(int _id)
    {
        var user = UsersData.Find(x => x.UserInfo.userId == _id);
        return user.UserInfo;
    }

    public void RemoveMessage(int _messageId)
    {
        
    }

    public List<Message> LoadHistory()
    {
        return _messages;
    }
}