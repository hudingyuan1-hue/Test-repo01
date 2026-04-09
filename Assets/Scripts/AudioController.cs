using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource BgmAudio;
    [SerializeField] AudioSource SfxAudio;

    public AudioClip bgm;

    
    // Start is called before the first frame update
    void Start()
    {
        BgmAudio.clip = bgm;
        BgmAudio.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySfx(AudioClip clip)
    {
     SfxAudio.PlayOneShot(clip);
    }
}
