using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerStateController : MonoBehaviour
{

    private const string keyword_trance = "trance";
    private const string keyword_ghost = "ghost";
    private const string keyword_exorcism = "exorcism";

    public float updateInterval = 30;
    public Text countdownText;
    public Text tranceVotesText;
    public Text ghostVotesText;
    public Text exorcismVotesText;

    private TwitchChatBot twitchChatBot;
    private float currentTime;
    private bool votesDirty;
    private int tranceVotes;
    private int ghostVotes;
    private int exorcismVotes;

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
            tranceVotesText.text = tranceVotes.ToString();
            ghostVotesText.text = ghostVotes.ToString();
            exorcismVotesText.text = exorcismVotes.ToString();
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
            if (normalizedMsg.Equals(keyword_trance))
            {
                tranceVotes++;
                votesDirty = true;
            }
            else if (normalizedMsg.Equals(keyword_ghost))
            {
                ghostVotes++;
                votesDirty = true;
            }
            else if (normalizedMsg.Equals(keyword_exorcism))
            {
                exorcismVotes++;
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
                // find majority and change player state accordingly
                // if tie or no votes => normal state
                if (tranceVotes > ghostVotes && tranceVotes > exorcismVotes)
                {
                    playerController.SetPlayerState(PlayerState.TRANCE);
                }
                else if (ghostVotes > tranceVotes && ghostVotes > exorcismVotes)
                {
                    playerController.SetPlayerState(PlayerState.GHOST);
                }
                else if (exorcismVotes > ghostVotes && exorcismVotes > tranceVotes)
                {
                    playerController.SetPlayerState(PlayerState.EXORCISM);
                }
                else
                {
                    playerController.SetPlayerState(PlayerState.NORMAL);
                }
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
        tranceVotes = 0;
        ghostVotes = 0;
        exorcismVotes = 0;
        votesDirty = true;
    }

}
