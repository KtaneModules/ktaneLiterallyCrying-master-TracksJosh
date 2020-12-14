using UnityEngine;
using System.Linq;
using System.Collections;
using KModkit;
using System;

public class literallyCryingScript : MonoBehaviour
{
    public KMSelectable PlayButton;
    public Material[] Emoji;
    public Renderer EmojiShow;
    public KMAudio audio;
    public KMNeedyModule Needy;

    private static string[] _emojis = new[] { "sleep", "stare", "cry"};

    int emojiSprite = 0;

    static int moduleIdCounter = 1;
    int moduleId;
    int Activated;
    private bool _isSolved;

    private void Start()
    {
        audio = GetComponent<KMAudio>();
    }

    void Awake()
    {
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
            GetComponent<KMSelectable>().AddInteractionPunch();
            audio.PlaySoundAtTransform("insult", transform);
            Activated = 54;
            Invoke("Play", 1.7f);
            Needy.HandlePass();
            _isSolved = true;
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
        audio.PlaySoundAtTransform("crybaby", transform);
        Invoke("OnNeedyDeactivation", 0.7f);
    }

    protected void OnNeedyActivation()
    {
        _isSolved = false;
        emojiSprite = 1;
        EmojiShow.GetComponent<MeshRenderer>().material = Emoji[emojiSprite];
        Activated = 0;
    }

    protected void OnNeedyDeactivation()
    {
        emojiSprite = 0;
        Activated = 54;
        EmojiShow.GetComponent<MeshRenderer>().material = Emoji[emojiSprite];
    }

    protected void OnTimerExpired()
    {
        GetComponent<KMNeedyModule>().OnStrike();
        emojiSprite = 0;
        EmojiShow.GetComponent<MeshRenderer>().material = Emoji[emojiSprite];
        Needy.HandlePass();
        _isSolved = true;

    }
#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use !{0} play to press the insult button";
#pragma warning disable 414
    IEnumerator ProcessTwitchCommand(string command)
    {
        string[] Tears = command.Trim().ToLowerInvariant().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if (Tears[0] == "play" && Tears[0] != "claim")
        {
            PlayButton.OnInteract();
            yield return string.Format("sendtochaterror WAAAAAAAAAAAAAAAH :(");
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
                yield return string.Format("sendtochaterror HAH! LOL! I can't cry :D");
                yield return null;
            }
        }
    }
}