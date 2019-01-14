
// public class ColorCoder
// {
//     //Color schemes:::
//     public static Color Color_keyword = new Color(88 / 255f, 156 / 255f, 214 / 255f);
//     public static Color Color_object = new Color(80 / 255f, 200 / 255f, 175 / 255f);
//     public static Color Color_comment = new Color(88 / 255f, 166 / 255f, 77 / 255f);
//     public static Color Color_string = new Color(210 / 255f, 150 / 255f, 100 / 255f);
//     public static Color Color_highlight = new Color(1, 0, 0);
    
//     // public static string[] keyword_list = { "boolean", "char", "class", "const", "double", "else", "final", "float", "for", "if", "int", "new", "private", "public", "return", "static", "this", "void", "while" };
//     // public static string[] object_list = { "PLACEHOLDER_FOR_CLASS_NAME", "Color", "Position", "String", "System"};

//     public static string colorize(string line)
//     {
//         List<int> indexes;
//         //STRINGS
//         line = colorizeStrings(line);
//         //KEYWORDS
//         for (int i = 0; i < keyword_list.Length; i++)
//         {
//             indexes = Parser.findWordLocations(line, keyword_list[i]);
//             line = colorizeWords(line, keyword_list[i], indexes, Color_keyword);
//         }
//         //OBJECTS
//         for (int i = 0; i < object_list.Length; i++)
//         {
//             indexes = Parser.findWordLocations(line, object_list[i]);
//             line = colorizeWords(line, object_list[i], indexes, Color_object);
//         }

//         return line;
//     }

//     public static string colorizeStrings(string line)
//     {
//         if (!line.Contains("\""))
//         {
//             return line;
//         }
//         else
//         {
//             string output = "";
//             bool open_string = false;
//             for (int i = 0; i < line.Length; i++)
//             {
//                 if (line[i] == '\"')
//                 {
//                     if (!open_string)
//                     {
//                         output += "<color=#" + getStringColor() + ">";
//                     }
//                     else
//                     {
//                         output += "\""+ "</color>";
//                         i++;
//                     }
//                     open_string = !open_string;
//                 }
//                 output += line[i];
//             }
//             return output;
//         }
//     }

//     public static string colorizeWords(string line, string word, List<int> indexes, Color color_in)
//     {
//         if (indexes.Count == 0)
//         {
//             return line;
//         }
//         else
//         {
//             string output = "";
//             int shifter = 0;
//             for (int i = 0; i < indexes.Count; i++)
//             {
//                 output += line.Substring(shifter, indexes[i]) + "<color=#" + RGBToHex(color_in) + ">" + word + "</color>" + line.Substring(indexes[i] + word.Length);

//                 shifter = indexes[i];
//             }
//             return output;
//         }
//     }

//     public static string highlight(string line)
//     {
//         return "<mark=#" + ColorCoder.getHighlightColor() + "55>" + line + "</mark>";
//     }
//     public static string comment(string line)
//     {
//         return line.Substring(0, line.IndexOf("//")) + "<color=#" + ColorCoder.getCommentColor() + ">" + line.Substring(line.IndexOf("//")) + "</color>";
//     }


    



//     public static string getKeywordColor()
//     {
//         return RGBToHex(Color_keyword);
//     }
//     public static string getCommentColor()
//     {
//         return RGBToHex(Color_comment);
//     }
//     public static string getHighlightColor()
//     {
//         return RGBToHex(Color_highlight);
//     }
//     public static string getColorObject()
//     {
//         return RGBToHex(Color_object);
//     }
//     public static string getStringColor()
//     {
//         return RGBToHex(Color_string);
//     }
//     /*  Code converted from Danny Lawrence's JavaScript implementation
//         http://wiki.unity3d.com/index.php?title=HexConverter        */
//     public static string RGBToHex(Color color_in)
//     {
//         float red = color_in.r * 255f;
//         float green = color_in.g * 255f;
//         float blue = color_in.b * 255f;

//         string a = GetHex(Mathf.Floor(red / 16));
//         string b = GetHex(Mathf.Round(red % 16));
//         string c = GetHex(Mathf.Floor(green / 16));
//         string d = GetHex(Mathf.Round(green % 16));
//         string e = GetHex(Mathf.Floor(blue / 16));
//         string f = GetHex(Mathf.Round(blue % 16));

//         string z = a + b + c + d + e + f;

//         return z;
//     }
//     public static string GetHex(float value)
//     {
//         string hex_values = "0123456789ABCDEF";
//         return hex_values[(int)value] + "";
//     }


// }