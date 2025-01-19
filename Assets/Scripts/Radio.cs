using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{

    public int clickObjectID;

    public Animator animator;

    public AudioSource music;

    public bool playMusic;
    public bool mToggle;

    public bool btnPressed;

    public List<AudioClip> radioQueue = new List<AudioClip>();

    private int trackIndex;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playMusic = true;

        music = GetComponent<AudioSource>();

        trackIndex = 0;

        music.clip = radioQueue[trackIndex];
        music.Play();

    }

    // Update is called once per frame
    void Update()
    {

        if(!music.isPlaying && playMusic && !mToggle) {
            playNextSong();
        }

        if(playMusic && mToggle) {
            music.Play();
            mToggle = false;
        }
        if(!playMusic && mToggle) {
            music.Pause();
            mToggle = false;
        }

    }

    public void toggleMusic() {

        mToggle = true;
        playMusic = !playMusic;

    }

    public void playNextSong() {

        trackIndex += 1;
        if(trackIndex > radioQueue.Count - 1) {
            trackIndex = 0;
        }
        music.clip = radioQueue[trackIndex];
        music.Play();

    }


    public void playLastSong() {

        if(trackIndex == 0) {
            trackIndex = radioQueue.Count - 1;
        } else {
            trackIndex -= 1;
        }
        music.clip = radioQueue[trackIndex];
        music.Play();

    }
    
    public void changeVolume(float volume) {

        music.volume = volume;

    }

}
