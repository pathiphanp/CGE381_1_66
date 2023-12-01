using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSoundMusic : MonoBehaviour
{
    [SerializeField] string nameMusic;
    void Start()
    {
        SoundManager.Instance.PlayMusic(nameMusic);
    }
}
