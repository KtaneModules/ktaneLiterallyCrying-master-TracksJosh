﻿using UnityEngine;
using System.Linq;
using System.Collections;
using KModkit;
using System;

public class literallyLaughingScript : MonoBehaviour
{
    public GameObject Patrick;
    public KMSelectable PlayButton;
    public Material[] Emoji;
    public Renderer EmojiShow;
    public KMAudio audio;
    public KMNeedyModule Needy;

    private static string[] _emojis = new[] { "sleep", "stare", "cry" };

    int emojiSprite = 0;

    static int moduleIdCounter = 1;
    int moduleId;
    int Activated;
    private bool _isSolved;

    private void Start()
    {
        Patrick.SetActive(false);
        audio = GetComponent<KMAudio>();
        emojiSprite = 0;
    }

    void Awake()
    {
        Patrick.SetActive(false);
        audio = GetComponent<KMAudio>();
        emojiSprite = 0;
        moduleId = moduleIdCounter++;
        Needy = GetComponent<KMNeedyModule>();
        Needy.OnNeedyActivation += OnNeedyActivation;
        Needy.OnNeedyDeactivation += OnNeedyDeactivation;
        Needy.OnTimerExpired += OnTimerExpired;
        emojiSprite = 0;
        Activated = 54;
        EmojiShow.GetComponent<MeshRenderer>().material = Emoji[emojiSprite];
        if (Activated <= 1)
        {
            PlayButton.OnInteract += delegate () { PressPlay(); return false; };
        }
        else
        {
            PlayButton.OnInteract += delegate () { PressPlay(); return true; };
        }
    }

    private void PressPlay()
    {
        if (Activated <= 1)
        {
            Patrick.SetActive(true);
            Needy.HandlePass();
            _isSolved = true;
            GetComponent<KMSelectable>().AddInteractionPunch();
            audio.PlaySoundAtTransform("coconutlol", transform);
            Activated = 54;
            Play();
        }
        else
        {
            GetComponent<KMSelectable>().AddInteractionPunch();
        }
    }

    private void Play()
    {
        emojiSprite = 2;
        EmojiShow.GetComponent<MeshRenderer>().material = Emoji[emojiSprite];
        Invoke("OnNeedyDeactivation", 5.2f);
    }

    protected void OnNeedyActivation()
    {
        _isSolved = false;
        emojiSprite = 1;
        audio.PlaySoundAtTransform("laughactivate", transform);
        EmojiShow.GetComponent<MeshRenderer>().material = Emoji[emojiSprite];
        Activated = 0;
    }

    protected void OnNeedyDeactivation()
    {
        emojiSprite = 0;
        Activated = 54;
        Patrick.SetActive(false);
        EmojiShow.GetComponent<MeshRenderer>().material = Emoji[emojiSprite];
    }

    protected void OnTimerExpired()
    {
        GetComponent<KMNeedyModule>().OnStrike();
        emojiSprite = 2;
        EmojiShow.GetComponent<MeshRenderer>().material = Emoji[emojiSprite];
        Needy.HandlePass();
        _isSolved = true;

    }
#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use !{0} coconut to press the coconut";
#pragma warning disable 414
    IEnumerator ProcessTwitchCommand(string command)
    {
        string[] Tears = command.Trim().ToLowerInvariant().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if (Tears[0] == "coconut" && Tears[0] != "claim")
        {
            PlayButton.OnInteract();
            yield return string.Format("sendtochaterror HAHAHAHA :D");
            yield return null;
        }
        else
        {
            if (Tears[0] == "claim")
            {
                yield return null;
            }
            else
            {
                yield return string.Format("sendtochaterror WEEEEEEEEEEE :D");
                yield return null;
            }
        }
    }
}