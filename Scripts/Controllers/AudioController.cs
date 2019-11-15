using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (AudioSource))]
public class AudioController : MonoBehaviour {

    AudioSource audio_obj;

    GameObject[] audio_points;
    GameObject[] audio_lines;

    GameObject point_prefab;
    GameObject lines_prefab;

    int fidelity;
    int usable_range;
    float[] spectrum_data;
    PointF[] spectrum_points;

    void Start () {

        audio_obj = this.GetComponent<AudioSource> ();

        fidelity = (int) Math.Pow (2, Audio.COARSENESS);
        usable_range = fidelity / 8;

        /* Spectrum data */
        spectrum_data = new float[fidelity];
        spectrum_points = new PointF[fidelity];

        /* Visualization objects */
        audio_points = new GameObject[fidelity];
        audio_lines = new GameObject[fidelity];

        point_prefab = Resources.Load<GameObject> ("Prefabs/UI/prefab");
        lines_prefab = Resources.Load<GameObject> ("Prefabs/UI/prefab");

        for (int i = 0; i < usable_range; i++) {

            audio_points[i] = Instantiate (
                point_prefab,
                this.transform.position,
                this.transform.rotation
            ) as GameObject;
            audio_points[i].transform.SetParent(this.transform);

            audio_lines[i] = Instantiate (
                lines_prefab,
                this.transform.position,
                this.transform.rotation
            ) as GameObject;
            audio_lines[i].transform.SetParent(this.transform);
        }
    }

    void Update () {

        AudioListener.GetSpectrumData (spectrum_data, Audio.CHANNEL, Audio.WINDOW);
        for (int i = 0; i < usable_range; i++) {
            if (spectrum_data[i] < .1f) spectrum_data[i] = .1f;
            spectrum_points[i] = new PointF (Mathf.Log (i) * 100f, spectrum_data[i] * 500f);

            /* Creates a new line between two systems, pointed between the two systems */
            audio_points[i].GetComponent<RectTransform> ().anchoredPosition = ConversionHandler.ToVector2 (spectrum_points[i]);

            if (i == 0) continue;

            audio_lines[i].GetComponent<RectTransform> ().anchoredPosition = ConversionHandler.ToVector2 (
                PointHandler.GetMidpoint (spectrum_points[i - 1], spectrum_points[i])
            );
            audio_lines[i].GetComponent<RectTransform> ().rotation = ConversionHandler.ToQuaternion (
                PointHandler.GetAngle (spectrum_points[i - 1], spectrum_points[i])
            );
            audio_lines[i].GetComponent<RectTransform> ().localScale = new Vector3 (
                PointHandler.GetDistance (spectrum_points[i - 1], spectrum_points[i]),
                .5f,
                .5f
            );
        }
    }
}