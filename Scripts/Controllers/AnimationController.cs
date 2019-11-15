using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour {

    public GameObject galaxy;

    // Queue<string> timing_queue;

    // List<string> timing_manual = new List<string> ();

    Text debug;

    float timer = 0f;
    float delta_time = 0f;
    float next_time = 0f;
    float delay_start = .5532f;
    float beat_length = .8000f;
    int beat_count = 0;

    void Start () {
        // timing_queue = new Queue<string> (timing);
        // next_time = GetNextTime ();
        debug = GameObject.Find ("DEBUGGER").GetComponent<Text> ();

        // GameObject.Find ("IntroText").GetComponent<TextController> ()
        //     .AddText (new List<string> { { TextAnimations.SMOOTH_ANIMATION + "Initializing connection" },
        //         { TextAnimations.TYPING_ANIMATION + "...\n" },
        //         { TextAnimations.SMOOTH_ANIMATION + "Fetching data from server" },
        //         { TextAnimations.TYPING_ANIMATION + "...\n" },
        //         { TextAnimations.SMOOTH_ANIMATION + "Decrypting packets" },
        //         { TextAnimations.TYPING_ANIMATION + "...\n" },
        //         { TextAnimations.SMOOTH_ANIMATION + "Parsing Huffman Codes" },
        //         { TextAnimations.TYPING_ANIMATION + "...\n" },
        //         { TextAnimations.SMOOTH_ANIMATION + "Enter passkey: " },
        //         { TextAnimations.TYPING_ANIMATION + "********\n" },
        //         { TextAnimations.SMOOTH_ANIMATION + "Populating content:" },
        //         { TextAnimations.TYPING_ANIMATION + "...\n" },
        //         { TextAnimations.SMOOTH_ANIMATION + "Welcome, PLAYER." }
        //     });
    }
    bool first_fire = true;
    void Update () {
        timer += Time.deltaTime;
        if (delay_start > 0) delay_start -= Time.deltaTime;
        if (delay_start <= 0) {
            if (first_fire) Animate (0);
            delta_time += Time.deltaTime;
            debug.text = "Time: " + timer;
            if (delta_time > beat_length) {
                delta_time -= beat_length;
                beat_count++;
                Animate (beat_count);
            }
        }
        // if (timer >= next_time) {
        //     next_time = GetNextTime ();
        // }
    }

    TextController IntroText;

    void Animate (int beat) {
        first_fire = false;
        print ("Beat" + beat_count);
        // GameObject.Find ("IntroText").GetComponent<TextController> ()
        //     .AddText (new List<string> { { TextAnimations.SMOOTH_ANIMATION + "Initializing connection" },
        //         { TextAnimations.TYPING_ANIMATION + "...\n" },
        //         { TextAnimations.SMOOTH_ANIMATION + "Fetching data from server" },
        //         { TextAnimations.TYPING_ANIMATION + "...\n" },
        //         { TextAnimations.SMOOTH_ANIMATION + "Decrypting packets" },
        //         { TextAnimations.TYPING_ANIMATION + "...\n" },
        //         { TextAnimations.SMOOTH_ANIMATION + "Parsing Huffman Codes" },
        //         { TextAnimations.TYPING_ANIMATION + "...\n" },
        //         { TextAnimations.SMOOTH_ANIMATION + "Enter passkey: " },
        //         { TextAnimations.TYPING_ANIMATION + "********\n" },
        //         { TextAnimations.SMOOTH_ANIMATION + "Populating content:" },
        //         { TextAnimations.TYPING_ANIMATION + "...\n" },
        //         { TextAnimations.SMOOTH_ANIMATION + "Welcome, PLAYER." }
        //     });
        switch (beat) {
            case 1:
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.NO_ANIMATION + "Initializing connection");
                break;
            case 2:
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.TYPING_ANIMATION + "...\n");
                break;
            case 3:
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.NO_ANIMATION + "Fetching data from server");
                break;
            case 4:
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.TYPING_ANIMATION + "...\n");
                break;
            case 5:
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.NO_ANIMATION + "Decrypting packet stream");
                break;
            case 6:
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.TYPING_ANIMATION + "...\n");
                break;
            case 7:
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.NO_ANIMATION + "Parsing Huffman Codes");
                break;
            case 8:
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.TYPING_ANIMATION + "...\n");
                break;
            case 9:
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.NO_ANIMATION + "Enter passkey:\t");
                break;
            case 10:
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.TYPING_ANIMATION + "******\n");
                break;
            case 11:
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.NO_ANIMATION + "Populating content");
                break;
            case 12:
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.TYPING_ANIMATION + "...\n");
                break;
            case 13:
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.NO_ANIMATION + "Welcome, PLAYER.");
                break;
            case 15:
                GameObject.Find ("IntroPanel").SetActive (false);
                galaxy.SetActive (true);
                break;
            case 23:
                galaxy.SetActive (false);
                break;
            case 40:
                break;
            case 45:
                break;
            case 50:
                break;
            case 55:
                break;
            case 60:
                break;
            case 65:
                break;
            case 70:
                break;
            case 75:
                break;
        }

    }

}