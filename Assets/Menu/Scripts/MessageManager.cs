using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/*
 * Manages messages passed from one level/scene to another.  Use the Send method to send a message to another scene.  A scene
 *  calls the Receive method to get the next message intended for it, or null if no new messages.
 */
public class MessageManager : MonoBehaviour
{

    public class Message
    {
        Scene _from;
        Scene _to;
        object _message;

        public Message(Scene from, Scene to, object message)
        {
            this._from = from;
            this._to = to;
            this._message = message;
        }

        public Scene from
        {
            get
            {
                return _from;
            }
        }

        public Scene to
        {
            get
            {
                return _to;
            }
        }

        public object message
        {
            get
            {
                return _message;
            }
        }

        override public string ToString()
        {
            return "Message(from=" + from.name + "to=" + to.name + "message=" + message + ")";
        }
    }

    private HashSet<Message> messages = new HashSet<Message>();


    public void Send(Scene me, Scene to, object message)
    {
        Debug.Log("Scene " + me.name + " sending message \"" + message + "\" to scene " + to);
        messages.Add(new Message(me, to, message));
    }

    public Message Receive(Scene me)
    {
        foreach (Message message in messages)
        {
            if (message.to == me)
            {
                Debug.Log("Scene " + me.name + " receiving message \"" + message.message + "\" from scene " + message.from);
                messages.Remove(message);
                return message;
            }
        }
        Debug.Log("Scene " + me.name + " has no new messages");
        return null;
    }


    #region Singleton
    private static MessageManager _instance;

    public static MessageManager instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Neither ~Bootstrap nor ~Init has been loaded");

            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Singleton " + this.GetType() + " instantiated");
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            throw new System.Exception("Duplicate singleton instantiated");
        }
    }
    #endregion
}
