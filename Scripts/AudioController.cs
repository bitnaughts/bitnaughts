// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// [RequireComponent (typeof (AudioSource))]
// public class AudioController : MonoBehaviour {

//     AudioSource audio_obj;

//     GameObject[] audio_points;
//     GameObject[] audio_lines;
//     GameObject[] audio_points_1;
//     GameObject[] audio_lines_1;

//     GameObject point_prefab;
//     GameObject lines_prefab;

//     int fidelity;
//     int usable_range;
//     float[] spectrum_data;
//     float[] wma_spectrum_data;
//     float[] wma_1_spectrum_data;
//     PointF[] spectrum_points;

//     void Start () {

//         audio_obj = this.GetComponent<AudioSource> ();

//         fidelity = (int) Math.Pow (2, Audio.COARSENESS);
//         usable_range = fidelity;

//         /* Spectrum data */
//         spectrum_data = new float[fidelity];
//         wma_spectrum_data = new float[fidelity];
//         wma_1_spectrum_data = new float[fidelity];
//         spectrum_points = new PointF[fidelity];

//         /* Visualization objects */
//         audio_points = new GameObject[fidelity];
//         audio_lines = new GameObject[fidelity];

//         audio_points_1 = new GameObject[fidelity];
//         audio_lines_1 = new GameObject[fidelity];

//         point_prefab = Resources.Load<GameObject> ("Prefabs/UI/prefab");
//         lines_prefab = Resources.Load<GameObject> ("Prefabs/UI/prefab");

//         for (int i = 1; i < usable_range; i++) {
//             audio_lines_1[i] = Instantiate (
//                 lines_prefab,
//                 this.transform.position,
//                 this.transform.rotation
//             ) as GameObject;
//             audio_lines_1[i].transform.SetParent (this.transform);
//             audio_lines_1[i].GetComponent<Image> ().color = new Color (77 / 255f, 60 / 255f, 38 / 255f, 1f);
//         }
//         for (int i = 0; i < usable_range; i++) {
//             audio_points_1[i] = Instantiate (
//                 point_prefab,
//                 this.transform.position,
//                 this.transform.rotation
//             ) as GameObject;
//             audio_points_1[i].transform.SetParent (this.transform);
//             audio_points_1[i].GetComponent<Image> ().color = new Color (99 / 255f, 77 / 255f, 50 / 255f, 1f);
//         }
//         for (int i = 1; i < usable_range; i++) {
//             audio_lines[i] = Instantiate (
//                 lines_prefab,
//                 this.transform.position,
//                 this.transform.rotation
//             ) as GameObject;
//             audio_lines[i].transform.SetParent (this.transform);
//             audio_lines[i].GetComponent<Image> ().color = new Color (125 / 255f, 104 / 255f, 79 / 255f, 1f);
//         }
//         for (int i = 0; i < usable_range; i++) {
//             audio_points[i] = Instantiate (
//                 point_prefab,
//                 this.transform.position,
//                 this.transform.rotation
//             ) as GameObject;
//             audio_points[i].transform.SetParent (this.transform);
//             audio_points[i].GetComponent<Image> ().color = new Color (159 / 255f, 145 / 255f, 102 / 255f, 1f);
//         }

//     }

//     void Update () {

//         AudioListener.GetSpectrumData (spectrum_data, Audio.CHANNEL, Audio.WINDOW);

//         for (int i = 0; i < usable_range; i++) {

//             wma_spectrum_data[i] = Mathf.Sqrt (spectrum_data[i]) + .75f * wma_spectrum_data[i];

//             spectrum_points[i] = new PointF (Mathf.Log (i + 1) * 88f, wma_spectrum_data[i] * 50f);

//             audio_points[i].GetComponent<RectTransform> ().anchoredPosition = ConversionHandler.ToVector2 (spectrum_points[i]);

//             if (i == 0) continue;

//             audio_lines[i].GetComponent<RectTransform> ().anchoredPosition = ConversionHandler.ToVector2 (
//                 PointF.GetMidpoint (spectrum_points[i - 1], spectrum_points[i])
//             );
//             audio_lines[i].GetComponent<RectTransform> ().rotation = ConversionHandler.ToQuaternion (
//                 PointF.GetAngle (spectrum_points[i], spectrum_points[i - 1])
//             );
//             audio_lines[i].GetComponent<RectTransform> ().eulerAngles = new Vector3 (audio_lines[i].GetComponent<RectTransform> ().eulerAngles.x, audio_lines[i].GetComponent<RectTransform> ().eulerAngles.y, -audio_lines[i].GetComponent<RectTransform> ().eulerAngles.z);
//             audio_lines[i].GetComponent<RectTransform> ().localScale = new Vector3 (
//                 .5f,
//                 PointF.GetDistance (spectrum_points[i - 1], spectrum_points[i]) / 10f,
//                 .5f
//             );
//         }
//         for (int i = 0; i < usable_range; i++) {

//             wma_1_spectrum_data[i] = Mathf.Sqrt (spectrum_data[i]) + .5f * wma_1_spectrum_data[i];

//             spectrum_points[i] = new PointF ( /* 400f - (i / (float) usable_range) * 400f */ Mathf.Log (i + 1) * 88f, wma_1_spectrum_data[i] * 50f);

//             audio_points_1[i].GetComponent<RectTransform> ().anchoredPosition = ConversionHandler.ToVector2 (spectrum_points[i]);

//             if (i == 0) continue;

//             audio_lines_1[i].GetComponent<RectTransform> ().anchoredPosition = ConversionHandler.ToVector2 (
//                 PointF.GetMidpoint (spectrum_points[i - 1], spectrum_points[i])
//             );
//             audio_lines_1[i].GetComponent<RectTransform> ().rotation = ConversionHandler.ToQuaternion (
//                 PointF.GetAngle (spectrum_points[i - 1], spectrum_points[i])
//             );
//             audio_lines_1[i].GetComponent<RectTransform> ().eulerAngles = new Vector3 (audio_lines_1[i].GetComponent<RectTransform> ().eulerAngles.x, audio_lines_1[i].GetComponent<RectTransform> ().eulerAngles.y, -audio_lines_1[i].GetComponent<RectTransform> ().eulerAngles.z);

//             audio_lines_1[i].GetComponent<RectTransform> ().localScale = new Vector3 (
//                 .5f,
//                 PointF.GetDistance (spectrum_points[i - 1], spectrum_points[i]) / 10f,
//                 .5f
//             );
//         }
//     }
// }