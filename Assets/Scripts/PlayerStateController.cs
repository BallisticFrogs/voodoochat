using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStateController : MonoBehaviour
{

    private const string keyword_ice = "ice";
    private const string keyword_ghost = "ghost";
    private const string keyword_metal = "metal";

    public float updateInterval = 30;
    public Text countdownText;
    public Text iceVotesText;
    public Text ghostVotesText;
    public Text metalVotesText;

    private TwitchChatBot twitchChatBot;
    private float currentTime;
    private bool votesDirty;
    private int iceVotes;
    private int ghostVotes;
    private int metalVotes;


    private readonly object ircClientlockObj = new object();

    void Awake()
    {
        GameObject twitchChatBotObj = GameObject.FindGameObjectWithTag(Tags.TwitchChatBot);
        if (twitchChatBotObj != null)
        {
            twitchChatBot = twitchChatBotObj.GetComponent<TwitchChatBot>();
        }
        ResetVotes();
    }

    void OnEnable()
    {
        if (twitchChatBot != null)
        {
            twitchChatBot.OnMessage += HandleNewTwitchMessage;
        }
    }

    void OnDisable()
    {
        if (twitchChatBot != null)
        {
            twitchChatBot.OnMessage -= HandleNewTwitchMessage;
        }
    }

    void Start()
    {

    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > updateInterval)
        {
            UpdatePlayerState();
            currentTime = 0;
        }

        if (votesDirty)
        {
            votesDirty = false;
            iceVotesText.text = iceVotes.ToString();
            ghostVotesText.text = ghostVotes.ToString();
            metalVotesText.text = metalVotes.ToString();
        }

        countdownText.text = "Change in " + Mathf.CeilToInt(updateInterval - currentTime);
    }

    private void HandleNewTwitchMessage(string username, string msg)
    {
        lock (ircClientlockObj)
        {
            string normalizedMsg = msg.Trim().ToLower();
            Debug.LogWarning(normalizedMsg);
            if (normalizedMsg.Equals(keyword_ice))
            {
                iceVotes++;
                votesDirty = true;
            }
            else if (normalizedMsg.Equals(keyword_ghost))
            {
                ghostVotes++;
                votesDirty = true;
            }
            else if (normalizedMsg.Equals(keyword_metal))
            {
                metalVotes++;
                votesDirty = true;
            }
        }
    }

    private void UpdatePlayerState()
    {
        lock (ircClientlockObj)
        {
            if (twitchChatBot != null)
            {
                // TODO compare votes
            }
            else
            {
                // TODO random for test
            }
        }

        ResetVotes();
    }

    private void ResetVotes()
    {
        iceVotes = 0;
        ghostVotes = 0;
        metalVotes = 0;
        votesDirty = true;
    }

}
