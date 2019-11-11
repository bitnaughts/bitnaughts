using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

public static class JSONHandler {
    public static string ToJSON (Dictionary<string, string> dict) {
        return "{" + String.Join (
            ",\n",
            dict.Select (x => ToJSONPair (x))
        ) + "}";
    }
    public static string ToJSONPair (KeyValuePair<string, string> x) {
        if (x.Value == null) return "\"" + x.Key + "\":\"NULL\"";
        if (x.Value[0] == '[') return "\"" + x.Key + "\":" + x.Value;
        return "\"" + x.Key + "\":\"" + x.Value + "\"";
    }

    public static string ToParameters (Dictionary<string, string> dict) {
        return "?" + String.Join (
            "&",
            dict.Select (x => x.Key + "=" + x.Value)
        );
    }
    public static string ToJSONArray<T> (List<T> list) {
        return "[" + String.Join (",", list.Select (x => x.ToString ())) + "]";
    }
}