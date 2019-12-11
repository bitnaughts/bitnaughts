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
    public string buffer_text = "";

    int delay_removal = 50;

    void Start () {
        // debugger = //GameObject.Find ("DEBUGGER").GetComponent<Text> ();
        print (debugger.text);
        client = new HttpClient ();
    }
    void FixedUpdate () {

        // buffer_text = buffer_text.Substring (buffer_text.IndexOf ('\n') + 1);
        if (buffer_text != "") {
            delay_removal = 50;
            if (buffer_text.Contains ("\n")) {
                debugger.text += buffer_text.Substring (0, buffer_text.IndexOf ('\n')) + "\n";
                buffer_text = buffer_text.Substring (buffer_text.IndexOf ('\n') + 1);
            } else {
                debugger.text += buffer_text;
                buffer_text = "";
            }
        } else if (delay_removal <= 0) {
            if (RandomHandler.NewPercentChance (25)) {
                if (debugger.text.Contains ("\n")) debugger.text = debugger.text.Substring (debugger.text.IndexOf ('\n') + 1);
                else debugger.text = "";
            }
        }
        delay_removal--;
        debugger.text = (debugger.text.Length > 10000) ? debugger.text.Substring (0, 10000) : debugger.text;

        // debugger.text = (debug_text.Length > 10000) ? debug_text.Substring (0, 10000) : debug_text;
        // // int deletion_speed = new System.Random ().Next (-50, 5);
        // // while (deletion_speed > 0) {
        // if (debug_text.Length > 10000 && debug_text.IndexOf ('\n') != -1) {
        //     if (RandomHandler.NextBool ())  if (RandomHandler.NextBool ()) debug_text = debug_text.Substring (debug_text.IndexOf ('\n') + 1); 
        // }
        // deletion_speed--;
        // }
    }

    public async Task<string> Mine (Asteroid asteroid, Ship ship, double amount) {
        return await Mine (asteroid.id, ship.id, amount);
    }
    public async Task<string> Mine (int asteroid_id, int ship_id, double amount) {
        string result = await Post (
            HTTP.Endpoints.MINE,
            new Dictionary<string, string> { { HTTP.Endpoints.Parameters.ASTEROID, asteroid_id.ToString () },
                { HTTP.Endpoints.Parameters.SHIP, ship_id.ToString () },
                { HTTP.Endpoints.Parameters.AMOUNT, amount.ToString ("F") }
            }
        );
        buffer_text += result.Replace (",", ", ");
        return result;
    }

    public async Task<string> Reset () {
        return await Get (
            HTTP.Endpoints.RESET
        );
    }

    public async Task<string> Get<T> (int id) {
        string result = await Get (
            HTTP.Endpoints.GET,
            new Dictionary<string, string> { { HTTP.Endpoints.Parameters.TYPE, GetTableForType (typeof (T).ToString ()) },
                { HTTP.Endpoints.Parameters.ID, id.ToString () }
            }
        );
        buffer_text += result.Replace (",", ", ");
        return result;
    }
    public async Task<string> Set<T> (T obj) {
        string result = await Post (
            HTTP.Endpoints.SET,
            new Dictionary<string, string> { { HTTP.Endpoints.Parameters.FLAG, HTTP.Endpoints.Parameters.Values.RESET } },
            obj.ToString ()
        );
        buffer_text += result.Replace (",", ", ");
        System.IO.File.WriteAllText (@"C:\test.txt", buffer_text);
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
        buffer_text += result.Replace (",", ", ");
        System.IO.File.WriteAllText (@"C:\test.txt", buffer_text);
        return result + GetTableForType (typeof (T).ToString ()) + typeof (T).ToString ();
    }
    public string GetTableForType (string type) {
        switch (type) {
            case "Ship":
                return "dbo.Ships";
            case "Galaxy":
                return "dbo.Galaxies";
            case "SolarSystem":
                return "dbo.Systems";
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
        buffer_text += String.Format ("{0}: Posting value(s) to Endpoint({1})\n",
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
        buffer_text += String.Format ("{0}: Getting value(s) from Endpoint({1})\n",
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
            return "Errored: " + ex.ToString ();
        }
    }

    public string GetRecepitDate () {
        return "3" + DateTime.Now.ToString ("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK").Substring (1);
    }
}