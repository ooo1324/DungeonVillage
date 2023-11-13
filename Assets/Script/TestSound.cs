using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    public AudioClip audioClip;

    private void OnTriggerEnter(Collider other)
    {
        //Managers.Sound.Play("Player/Funky Chill 2 loop", Define.Sound.Bgm);
       // Managers.Sound.Play("Player/DM-CGS-19", Define.Sound.Effect);
        Managers.Sound.Play(audioClip, Define.Sound.Effect);
    }
}
