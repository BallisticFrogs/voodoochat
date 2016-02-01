using System;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Net.Sockets;

public class IrcClient
{

    private TcpClient tcpClient;
    private StreamReader input;
    private StreamWriter output;

    private string username;
    private string channel;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ip">irc.twitch.tv</param>
    /// <param name="port">6667</param>
    /// <param name="username">twitch username</param>
    /// <param name="password">must include the "oauth:" part</param>
    public IrcClient(string ip, int port, string username, string password)
    {
        this.username = username;
        tcpClient = new TcpClient(ip, port);
        input = new StreamReader(tcpClient.GetStream());
        output = new StreamWriter(tcpClient.GetStream()) { NewLine = "\r\n" };

        if (!password.StartsWith("oauth:"))
        {
            password = "oauth:" + password;
        }

        output.WriteLine("PASS " + password);
        output.WriteLine("NICK " + username);
        output.WriteLine("USER " + username + " 8 * :" + username);
        output.Flush();

        string message = ReadChatMessage();
        if (message.Contains("Error"))
        {
            int indexOfSep = message.LastIndexOf(':');
            if (indexOfSep >= 0)
            {
                message = message.Substring(indexOfSep + 1);
            }
            throw new Exception(message);
        }
    }

    public void Disconnect()
    {
        input.Close();
        output.Close();
        tcpClient.Close();
    }

    public void JoinRoom(string channel)
    {
        this.channel = channel;
        SendIrcMessage("JOIN #" + channel);
    }

    public void SendChatMessage(string msg)
    {
        SendIrcMessage(":" + username + "!" + username + "@" + username + ".tmi.twitch.tv PRIVMSG #" + channel + " :" + msg);
    }

    public string ReadChatMessage()
    {
        return input.ReadLine();
    }

    public void SendIrcMessage(string msg)
    {
        output.WriteLine(msg);
        output.Flush();
    }

}
