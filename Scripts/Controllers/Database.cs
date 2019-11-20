/* C# Dependencies */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/* Unity, Async Dependencies */
using UnityEngine;
using UnityEngine.UI;

public class Database : MonoBehaviour {

    public const bool LOAD_FROM_SERVER = false;

    HttpClient client;
    public Text debugger;
    public string debug_text;

    void Start () {
        // debugger = //GameObject.Find ("DEBUGGER").GetComponent<Text> ();
        print (debugger.text);
        client = new HttpClient ();
    }

    void Update () {

        debugger.text = (debug_text.Length > 5000) ? debug_text.Substring (0, 5000) : debug_text;
        // int deletion_speed = new System.Random ().Next (-50, 5);
        // while (deletion_speed > 0) {
        if (debug_text.Length > 5000 && debug_text.IndexOf ('\n') != -1) {
            debug_text = debug_text.Substring (debug_text.IndexOf ('\n') + 1);
        }
        // deletion_speed--;
        // }
    }

    public async Task<string> Mine (Asteroid asteroid, Ship ship, double amount) {
        return await Post (
            HTTP.Endpoints.MINE,
            new Dictionary<string, string> { { HTTP.Endpoints.Parameters.ASTEROID, asteroid.id.ToString () },
                { HTTP.Endpoints.Parameters.SHIP, ship.id.ToString () },
                { HTTP.Endpoints.Parameters.AMOUNT, amount.ToString ("F") }
            }
        );
    }

    public async Task<string> Reset () {
        return await Get (
            HTTP.Endpoints.RESET
        );
    }

    public async Task<string> Get<T> (int id) {
        return await Get (
            HTTP.Endpoints.GET,
            new Dictionary<string, string> { { HTTP.Endpoints.Parameters.TYPE, GetTableForType(typeof (T).ToString ()) },
                { HTTP.Endpoints.Parameters.ID, id.ToString () }
            }
        );
    }
    public async Task<string> Set<T> (T obj) {
        string result = await Post (
            HTTP.Endpoints.SET,
            new Dictionary<string, string> { { HTTP.Endpoints.Parameters.FLAG, HTTP.Endpoints.Parameters.Values.RESET } },
            obj.ToString ()
        );
        debug_text += result.Replace (",", ", ");
        System.IO.File.WriteAllText (@"C:\test.txt", debug_text);
        return result;
    }
    public async Task<string> Add<T> (T obj) {
        string result = await Post (
            HTTP.Endpoints.SET,
            new Dictionary<string, string> { { HTTP.Endpoints.Parameters.FLAG, HTTP.Endpoints.Parameters.Values.ADD },
                { HTTP.Endpoints.Parameters.TABLE, GetTableForType (typeof (T).ToString ()) }
            },
            obj.ToString ()
        );
        debug_text += result.Replace (",", ", ");
        System.IO.File.WriteAllText (@"C:\test.txt", debug_text);
        return result + GetTableForType (typeof (T).ToString ()) + typeof (T).ToString ();
    }
    public string GetTableForType (string type) {
        switch (type) {
            case "Ship":
                return "dbo.Ships";
            case "Galaxy":
                return "dbo.Galaxies";
        }
        return "null";
    }

    /* HTTP Post Logic with System.Net.Http */
    private async Task<string> Post (string endpoint, Dictionary<string, string> parameters_dict) {
        return await Post (
            endpoint + JSONHandler.ToParameters (parameters_dict),
            ""
        );
    }
    private async Task<string> Post (string endpoint, Dictionary<string, string> parameters_dict, string json) {
        return await Post (
            endpoint + JSONHandler.ToParameters (parameters_dict),
            json
        );
    }
    private async Task<string> Post (string endpoint, string json) {
        debug_text += String.Format ("{0}: Posting value(s) to Endpoint({1})\n",
            GetRecepitDate (),
            endpoint
        );
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
        debug_text += String.Format ("{0}: Getting value(s) from Endpoint({1})\n",
            GetRecepitDate (),
            endpoint
        );
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

    public string GetRecepitDate () {
        return "3" + DateTime.Now.ToString ("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK").Substring (1);
    }
}