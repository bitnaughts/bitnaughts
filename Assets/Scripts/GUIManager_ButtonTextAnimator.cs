using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager_ButtonTextAnimator : MonoBehaviour
{
    public string message_top;
    public string message_bottom;
    GameObject[] tiles_top = new GameObject[11];
    GameObject[] tiles_bottom = new GameObject[11];
    void Start()
    {

        for (int i = 0; i < 10; i++)
        {
            tiles_top[i] = this.transform.GetChild(i).gameObject;
            tiles_bottom[i] = this.transform.GetChild(i + 10).gameObject;
        }
    }

    public void updateTopMessage(string input)
    {
        message_top = input;
        for (int i = 0; i < 10 && i < message_top.Length; i++)
        {
            if (message_top.ToCharArray()[i] != ' ')
            {
                tiles_top[i].GetComponent<Image>().sprite = (Resources.Load("alphabet/" + message_top.ToCharArray()[i].ToString()) as GameObject).GetComponent<SpriteRenderer>().sprite;
            }
            else tiles_top[i].GetComponent<Image>().sprite = (Resources.Load("alphabet/Empty") as GameObject).GetComponent<SpriteRenderer>().sprite;
        }
        for (int i = message_top.Length; i < 10; i++)
        {
            tiles_top[i].GetComponent<Image>().sprite = (Resources.Load("alphabet/Empty") as GameObject).GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void updateBottomMessage(string input)
    {
        message_bottom = input;
        for (int i = 0; i < 10 && i < message_bottom.Length; i++)
        {
            if (message_bottom.ToCharArray()[i] != ' ')
            {
                tiles_bottom[i].GetComponent<Image>().sprite = (Resources.Load("alphabet/" + message_bottom.ToCharArray()[i].ToString()) as GameObject).GetComponent<SpriteRenderer>().sprite;
            }
            else tiles_bottom[i].GetComponent<Image>().sprite = (Resources.Load("alphabet/Empty") as GameObject).GetComponent<SpriteRenderer>().sprite;
        }
        for (int i = message_bottom.Length; i < 10; i++)
        {
            tiles_bottom[i].GetComponent<Image>().sprite = (Resources.Load("alphabet/Empty") as GameObject).GetComponent<SpriteRenderer>().sprite;
        }
    }

}
    /* FOR SCROLLING TEXT CAPABILITIES, WHICH I DONT NEED...?
    public string message_top;
    public string message_bottom;
    string originalMessage = "";
    int delay_top = 0;
    int delay_bottom = 0;
    GameObject[] tiles_top = new GameObject[11];
    GameObject[] tiles_bottom = new GameObject[11];

    // Use this for initialization
    void Start()
    {

        for (int i = 0; i < 11; i++)
        {
            tiles_top[i] = this.transform.GetChild(i).gameObject;
            tiles_bottom[i] = this.transform.GetChild(i + 11).gameObject;
        }


        updateTopMessage(message_top);//)"GALACTIC CONQUEST");
        updateBottomMessage(message_bottom);

    }

    public void updateTopMessage(string input)
    {
        message_top = input;
        for (int i = 0; i < 11 && i < message_top.Length; i++)
        {
            if (message_top.ToCharArray()[i] != ' ')
            {
                tiles_top[i].GetComponent<Image>().sprite = (Resources.Load("alphabet/" + message_top.ToCharArray()[i].ToString()) as GameObject).GetComponent<SpriteRenderer>().sprite;
            }
            else tiles_top[i].GetComponent<Image>().sprite = (Resources.Load("alphabet/Empty") as GameObject).GetComponent<SpriteRenderer>().sprite;
        }
        for (int i = message_top.Length; i < 11; i++)
        {
            tiles_top[i].GetComponent<Image>().sprite = (Resources.Load("alphabet/Empty") as GameObject).GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void updateBottomMessage (string input)
    {
        message_bottom = input;
        for (int i = 0; i < 11 && i < message_bottom.Length; i++)
        {
            if (message_bottom.ToCharArray()[i] != ' ')
            {
                tiles_bottom[i].GetComponent<Image>().sprite = (Resources.Load("alphabet/" + message_bottom.ToCharArray()[i].ToString()) as GameObject).GetComponent<SpriteRenderer>().sprite;
            }
            else tiles_bottom[i].GetComponent<Image>().sprite = (Resources.Load("alphabet/Empty") as GameObject).GetComponent<SpriteRenderer>().sprite;
        }
        for (int i = message_bottom.Length; i < 11; i++)
        {
            tiles_bottom[i].GetComponent<Image>().sprite = (Resources.Load("alphabet/Empty") as GameObject).GetComponent<SpriteRenderer>().sprite;
        }
    }

    public string shift(string input)
    {
        if (originalMessage == "")
        {
            originalMessage = input;
        }


        char firstLetter = input.ToCharArray()[0];
        char[] newMessage = new char[input.Length];
        for (int i = 0; i < input.Length - 1; i++)
        {
            newMessage[i] = input.ToCharArray()[i + 1];

        }
        newMessage[newMessage.Length - 1] = firstLetter;
        //message_top = new string(newMessage);

        //print (message);
        if (originalMessage == message_top) delay_top = -500;
        if (originalMessage == message_bottom) delay_bottom = -500;
        //{
        //    delay = -200;
        //print ("swag");
        //}
        return new string(newMessage);
    }

    // Update is called once per frame
    void Update()
    {
        if (delay_top++ > 40)
        {
            delay_top = 0;
            if (message_top.Length > 11) updateTopMessage(shift(message_top));
        }
        if (delay_bottom++ > 40)
        {
            delay_bottom = 0;
            if (message_bottom.Length > 11) updateBottomMessage(shift(message_bottom));
        }
    }
}
*/