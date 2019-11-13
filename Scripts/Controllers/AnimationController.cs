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

    public string[] timing;

    Queue<string> timing_queue;

    List<string> timing_manual = new List<string> ();

    Text debug;

    float timer = 0f;
    float delta_time = 0f;
    float next_time = 0f;
    float delay_start = .5532f;
    float beat_length = .8000f;
    int beat_count = 0;

    void Start () {
        timing_queue = new Queue<string> (timing);
        next_time = GetNextTime ();
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
        if (timer >= next_time) {
            next_time = GetNextTime ();
        }
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
            case 0:
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.SMOOTH_ANIMATION + "Initializing connection");
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.TYPING_ANIMATION + "...\n\n");
                break;
            case 2:
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.SMOOTH_ANIMATION + "Fetching data from server");
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.TYPING_ANIMATION + "...\n\n");
                break;
            case 4:
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.SMOOTH_ANIMATION + "Decrypting packets");
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.TYPING_ANIMATION + "...\n\n");
                break;
            case 6:
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.SMOOTH_ANIMATION + "Parsing Huffman Codes");
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.TYPING_ANIMATION + "...\n\n");
                break;
            case 8:
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.SMOOTH_ANIMATION + "Enter passkey:\t");
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.TYPING_ANIMATION + "*******\n\n");
                break;
            case 12:
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.SMOOTH_ANIMATION + "Populating content");
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.TYPING_ANIMATION + "...\n\n");
                break;
            case 14:
                GameObject.Find ("IntroText").GetComponent<TextController> ().AddText (TextAnimations.SMOOTH_ANIMATION + "Welcome, PLAYER.");
                break;
            case 25:
                break;
            case 30:
                break;
            case 35:
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

    float GetNextTime () {
        if (timing_queue.Peek () != "") {
            // print (timing_queue.Peek ().Split (' ') [1]);
            timing_queue.Dequeue ();
            return float.Parse (timing_queue.Peek ().Split (' ') [0]);
        }
        return 1000f;
    }

}