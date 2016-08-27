using System;
using UnityEngine;

/*
 * Represents level music.  Holds the clip, whether it loops or not, or whether or not it restarts at beginning of level 
 * even if already playing.  Used by the MusicManager singleton.
 */

[Serializable]
public class Music
{
    [Tooltip("Audioclip for the music")]
    public AudioClip clip;
    [Tooltip("Keep looping if true")]
    public bool loops = true;
    [Tooltip("If true, restart music when level begins, even if this clip is already playing")]
    public bool restartWithLevel = false;

    public override string ToString()
    {
        if (!clip)
        {
            return "null";
        }
        string result = "Music(clip=" + clip.name;
        if (loops)
        {
            result += ", loops";
        }
        if (restartWithLevel)
        {
            result += ", restartWithLevel";
        }
        result += ")";
        return result;
    }

    public override bool Equals(object obj)
    {
        Music other = (Music)obj;
        return clip == other.clip && loops == other.loops && restartWithLevel == other.restartWithLevel;
    }

    public override int GetHashCode()
    {
        return clip.GetHashCode() + 7 * loops.GetHashCode() + 11 * restartWithLevel.GetHashCode();
    }
}