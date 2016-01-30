using System;
using UnityEngine;
using System.Collections;
using System.Threading;
using UnityEngine.UI;

public class IrcBot : MonoBehaviour
{

    private const string pref_username = "username";
    private const string pref_password = "password";

    // voodoochat
    // oauth:g0jq8rzhs4a6epo4tygnpgv5w8yit2

    public InputField usernameField;
    public InputField passwordField;
    public Text feedbackText;

    private IrcClient client;
    //    private ThreadController threadController;
    private Thread ircClientThread;

    private readonly object ircClientlockObj = new object();

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
            //ircClientThread.Abort();
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

            // connect to Twitch
            client = new IrcClient("irc.twitch.tv", 6667, username, password);
            client.JoinRoom(username);

            // save prefs
            PlayerPrefs.SetString(pref_username, username);
            PlayerPrefs.SetString(pref_password, password);

            // start reading chat
            ircClientThread = new Thread(ReadChat);
            ircClientThread.Start();

            // load main scene
            GlobalUI.LoadLvl();
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

            lock (ircClientlockObj)
            {
                Debug.LogWarning(message);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //            lock (ircClientlockObj)
        //            {
        //            }
    }

}
