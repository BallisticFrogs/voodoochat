using System;
using UnityEngine;
using System.Collections;
using System.Threading;
using UnityEngine.UI;

public class TwitchChatBot : MonoBehaviour
{

    public delegate void Messagehandler(string username, string mesg);

    private const string pref_username = "username";
    private const string pref_password = "password";

    public InputField usernameField;
    public InputField passwordField;
    public Text feedbackText;

    public event Messagehandler OnMessage;

    private IrcClient client;
    private Thread ircClientThread;

    void Awake()
    {
        // keep object when changing scene
        DontDestroyOnLoad(gameObject);

        // load prefs
        usernameField.text = PlayerPrefs.GetString(pref_username);
        passwordField.text = PlayerPrefs.GetString(pref_password);
    }

    void OnDisable()
    {
        if (ircClientThread != null)
        {
            Debug.LogWarning("Disconnecting IRC client");
            client.Disconnect();
            ircClientThread = null;
            Debug.LogWarning("IRC client disconnected");
        }
    }

    public void Connect()
    {
        feedbackText.text = null;
        try
        {
            // read ui fields
            string username = usernameField.text;
            string password = passwordField.text;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                // connect to Twitch
                client = new IrcClient("irc.twitch.tv", 6667, username, password);
                client.JoinRoom(username.ToLower());

                // save prefs
                PlayerPrefs.SetString(pref_username, username);
                PlayerPrefs.SetString(pref_password, password);

                // start reading chat
                ircClientThread = new Thread(ReadChat);
                ircClientThread.Start();

                // load main scene
                GlobalUI.LoadLvl();
            }
            else
            {
                feedbackText.text = "Missing username and/or password !";
            }
        }
        catch (Exception e)
        {
            feedbackText.text = e.Message;
        }
    }

    private void ReadChat()
    {
        while (true)
        {
            string message = client.ReadChatMessage(); // blocking

            string[] parts = message.Split(':', '!');
            //            Debug.LogWarning(message);
            //            for (int i = 0; i < parts.Length; i++)
            //            {
            //                Debug.LogWarning(i + " => " + parts[i]);
            //            }

            if (OnMessage != null && parts.Length > 2) OnMessage(parts[1], parts[parts.Length - 1]);
        }
    }

}
