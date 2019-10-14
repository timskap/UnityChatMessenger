using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "SO_ChatRoom", menuName = "ChatData/SO_ChatRoom", order = 1)]
public class SO_ChatRoom : ScriptableObject
{
    [SerializeField] private int currentMessageId;
    public int hostId;
    [SerializeField] private List<Message> _messages;
    [SerializeField] private List<SO_UserData> UsersData;
    
    /// <summary>
    /// Добавление сообщения в хранилище
    /// </summary>
    /// <param name="textMessage">Сообщение пользователя</param>
    /// <param name="_user">Пользователь</param>
    /// <returns></returns>
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
    
    /// <summary>
    /// Получение пользователя по ID
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public User GetUser(int _id)
    {
        SO_UserData user = UsersData.Find(x => x.UserInfo.userId == _id);
        return user.UserInfo;
    }
    
    /// <summary>
    /// Удаление сообщения
    /// </summary>
    /// <param name="_message"></param>

    public void RemoveMessage(Message _message)
    {
      _messages.Remove(_message);
    }
    
    /// <summary>
    /// Получить все свои сообщения
    /// </summary>
    /// <returns></returns>
    
    public List<Message> GetOwnMessages()
    {
        List<Message> ownMessages =  _messages.FindAll(x => x.user.userId == hostId);

        return ownMessages;
    }
    
    /// <summary>
    /// Загрузить историю всех сообщений из этого чата
    /// </summary>
    /// <returns></returns>

    public List<Message> LoadHistory()
    {
        return _messages;
    }

    public User GetRandomUser()
    {
        User _randomUser = UsersData[Random.Range(0, UsersData.Count - 1)].UserInfo;
        return _randomUser;
    }
}