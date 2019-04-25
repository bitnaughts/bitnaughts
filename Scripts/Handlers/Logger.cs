using System;
using System.IO;

class Logger {
    public static void Log (string logMessage) {
        using (StreamWriter w = File.AppendText ("C:\\Users\\Mutilar\\Documents\\GitHub\\BitNaughtsUnity\\Assets\\bitnaughts\\Assets\\Scripts\\Handlers\\log.txt")) {
            w.Write ("\r\nLog Entry : ");
            w.WriteLine ($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine ("  :");
            w.WriteLine ($"  :{logMessage}");
            w.WriteLine ("-------------------------------");
        }
    }
}