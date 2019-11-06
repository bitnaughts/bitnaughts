/* C# Dependencies */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/* Unity, Async Dependencies */
using UnityAsync;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using WaitForSeconds = UnityAsync.WaitForSeconds;
using WaitForSecondsRealtime = UnityAsync.WaitForSecondsRealtime;
using WaitUntil = UnityAsync.WaitUntil;
using WaitWhile = UnityAsync.WaitWhile;

public class DatabaseController : MonoBehaviour {

    public const bool LOAD_FROM_SERVER = true;

    HttpClient client;
    Text debugger;
    
    void Start () {
        debugger = GameObject.Find ("DEBUGGER").GetComponent<Text> ();
        client = new HttpClient ();
    }

    public async Task<string> Reset () {
        return await Get (
            HTTP.Endpoints.RESET
        );
    }

    public async Task<string> Get<T> (int id) {
        return await Get (
            HTTP.Endpoints.GET,
            new Dictionary<string, string> { { HTTP.Endpoints.Parameters.TYPE, typeof (T).ToString () },
                { HTTP.Endpoints.Parameters.ID, id.ToString () }
            }
        );
    }
    public async void Set<T> (T obj) {
        debugger.text += await Post (
            HTTP.Endpoints.SET,
            new Dictionary<string, string> { { HTTP.Endpoints.Parameters.TYPE, typeof (T).ToString () } },
            obj.ToString ()
        );
    }

    /* HTTP Post Logic with System.Net.Http */
    private async Task<string> Post (string endpoint, Dictionary<string, string> parameters_dict, string json) {
        return await Post (
            endpoint + JSONHandler.ToParameters (parameters_dict),
            json
        );
    }
    private async Task<string> Post (string endpoint, string json) {
        try {
            HttpResponseMessage response = await client.PostAsync (
                HTTP.API_ENDPOINT + endpoint,
                new StringContent (json)
            );
            response.EnsureSuccessStatusCode ();
            return await response.Content.ReadAsStringAsync ();
        } catch (Exception ex) {
            return ex.ToString ();
        }
    }

    /* HTTP Get Logic with System.Net.Http */
    private async Task<string> Get (string endpoint, Dictionary<string, string> parameters_dict) {
        return await Get (
            endpoint + JSONHandler.ToParameters (parameters_dict)
        );
    }
    private async Task<string> Get (string endpoint) {
        try {
            HttpResponseMessage response = await client.GetAsync (
                HTTP.API_ENDPOINT + endpoint
            );
            response.EnsureSuccessStatusCode ();
            return await response.Content.ReadAsStringAsync ();
        } catch (Exception ex) {
            return ex.ToString ();
        }
    }
}