using UnityEngine;

public class SoundPlayer
{
    public void PlaySoundOnce(AudioSource source, AudioClip clip)
    {
        source.Stop();
        source.pitch = Random.Range(1f, 1.5f);
        source.volume = Random.Range(.3f, .6f);
        source.PlayOneShot(clip);
    }
}
