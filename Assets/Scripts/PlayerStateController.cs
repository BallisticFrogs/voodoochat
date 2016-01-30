using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

    private PlayerController playerController;

    private readonly object ircClientlockObj = new object();

    private PlayerState[] playerStateValues;

    void Awake()
    {
       playerController = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerController>();

        GameObject twitchChatBotObj = GameObject.FindGameObjectWithTag(Tags.TwitchChatBot);
        if (twitchChatBotObj != null)
        {
            twitchChatBot = twitchChatBotObj.GetComponent<TwitchChatBot>();
        }

        playerStateValues = Enum.GetValues(typeof(PlayerState)).OfType<PlayerState>().ToArray();

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

        // if not connected to Twitch, allow to switch state using keyboard for test
        if (twitchChatBot == null)
        {
            if (Input.GetKeyDown(KeyCode.U)) playerController.SetPlayerState(PlayerState.NORMAL);
            if (Input.GetKeyDown(KeyCode.I)) playerController.SetPlayerState(PlayerState.TRANCE);
            if (Input.GetKeyDown(KeyCode.O)) playerController.SetPlayerState(PlayerState.GHOST);
            if (Input.GetKeyDown(KeyCode.P)) playerController.SetPlayerState(PlayerState.EXORCISM);
        }
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
                // no connection to Twitch => random for test
                int stateIndex = Random.Range(0, playerStateValues.Length - 1);
                PlayerState newState = playerStateValues[stateIndex];
                playerController.SetPlayerState(newState);
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
