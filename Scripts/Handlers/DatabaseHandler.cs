using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public static class DatabaseHandler {

    public const bool LOAD_FROM_DATABASE = true;

    public const string URL = "https://bitnaughts.azurewebsites.net/api/";

    /* Endpoints */
    public const string GET = "get",
        SET = "set";

    /* Parameter Flags */
    public const string FLAG = "flag",
        RESET = "reset",
        TYPE = "type",
        ID = "id";

    public static HttpClient client = new HttpClient ();

    public static async Task<string> Get<T> (int id) {
        return await Get (
            GET,
            new Dictionary<string, string> { { TYPE, typeof (T).ToString () },
                { ID, id.ToString () }
            }
        );
    }
    public static async Task<string> Set<T> (T obj) {
        return await Post (
            SET,
            new Dictionary<string, string> { { TYPE, typeof (T).ToString () } },
            obj.ToString ()
        );
    }

    /* HTTP Post Logic with System.Net.Http */
    public static async Task<string> Post (string endpoint, Dictionary<string, string> parameters_dict, string json) {
        return await Post (
            endpoint + JSONHandler.ToParameters (parameters_dict),
            json
        );
    }
    public static async Task<string> Post (string endpoint, string json) {
        try {
            HttpResponseMessage response = await client.PostAsync (
                URL + endpoint,
                new StringContent (json)
            );
            response.EnsureSuccessStatusCode ();
            return await response.Content.ReadAsStringAsync ();
        } catch (Exception ex) {
            return ex.ToString ();
        }
    }

    /* HTTP Get Logic with System.Net.Http */
    public static async Task<string> Get (string endpoint, Dictionary<string, string> parameters_dict) {
        return await Get (
            endpoint + JSONHandler.ToParameters (parameters_dict)
        );
    }
    public static async Task<string> Get (string endpoint) {
        try {
            HttpResponseMessage response = await client.GetAsync (
                URL + endpoint
            );
            response.EnsureSuccessStatusCode ();
            return await response.Content.ReadAsStringAsync ();
        } catch (Exception ex) {
            return ex.ToString ();
        }
    }

}