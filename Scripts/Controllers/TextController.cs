using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Text))]
public class TextController : MonoBehaviour {

    string text = "";

    Text text_obj;

    float timer = 0f;

    Queue<TextChunk> chunks = new Queue<TextChunk> (); /* Parts of text to be added at a given rate */

    void Start () {
        text_obj = this.GetComponent<Text> ();
    }

    void Update () {
        if (chunks.Count != 0) {
            Tuple<string, bool> result = chunks.Peek ().Step (Time.deltaTime);
            if (result.Item2) {
                text += result.Item1;
                text_obj.text = text;
                chunks.Dequeue ();
            } else {
                text_obj.text = text + result.Item1;
            }
        }
    }

    public void AddText (List<string> inputs) {
        foreach (var input in inputs) AddText (input);
    }
    public void AddText (string input) {
        string animation = input.Substring (0, 2);
        string input_text = input.Substring (2);

        switch (animation) {
            case TextAnimations.TYPING_ANIMATION:
                chunks.Enqueue (new TextChunk (input_text, .1f, false));
                break;
            case TextAnimations.SMOOTH_ANIMATION:
                chunks.Enqueue (new TextChunk (input_text));
                break;
            case TextAnimations.NO_ANIMATION:
                text += input_text;
                text_obj.text = text;
                break;
        }
    }
}

public class TextChunk {
    /* Keeps track of input to output conversion */
    string input;
    StringBuilder output = new StringBuilder ();
    int position;

    /* Controls how fast text is revealed */
    float timer = 0f;
    float speed = .0125f;
    float random_variation;

    /* Controls garbled text leading output */
    string noise = TextAnimations.NOISE;
    int noise_distance = 15;

    /* Generates unmodified TextChunk with default speed and noise  */
    public TextChunk (string input) {
        this.input = input;
        this.position = -noise_distance;
    }

    /* Generates TextChunk with custom speed */
    public TextChunk (string input, float speed) {
        this.input = input;
        this.speed = speed;
        this.position = -noise_distance;
    }

    /* Generates TextChunk with custom speed */
    public TextChunk (string input, float speed, bool noisy) {
        this.input = input;
        this.speed = speed;
        this.noise_distance = noisy ? 5 : 0;
        this.position = -noise_distance;
    }

    /* Progresses the string conversion process, returning true when completed */
    public Tuple<string, bool> Step (float delta_time) {
        timer += delta_time;
        while (timer > speed * random_variation) {
            timer -= speed * random_variation;
            random_variation = RandomHandler.NewFloat (0, 2);
            if (position > -1) {
                if (noise_distance > 0) output[position] = input[position];
                else output.Append (input[position]);
            }
            if (output.Length == input.Length) {
                if (output.ToString () == input) {
                    return new Tuple<string, bool> (output.ToString (), true);
                }
            } else if (noise_distance > 0) {
                output.Append (
                    RandomHandler.NewItem<char> (noise.ToCharArray())
                );
            }
            if (position < input.Length) position++;
        }
        return new Tuple<string, bool> (output.ToString (), false);
    }
}