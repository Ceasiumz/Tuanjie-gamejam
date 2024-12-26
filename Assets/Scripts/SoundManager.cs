using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource ButtonaudioSource;

    public void ButtonClickSound()
    {
        ButtonaudioSource.PlayOneShot(ButtonaudioSource.clip);
    }
}