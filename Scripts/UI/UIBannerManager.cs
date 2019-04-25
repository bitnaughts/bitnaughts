using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIBannerManager : MonoBehaviour {
	public string message;
    public int length;
    GameObject[] tiles;

    string originalMessage = "";
	int delay = 0;



	// Use this for initialization
	void Start ()
    {
        tiles = new GameObject[length];
        for (int i = 0; i < length; i++)
        {
            tiles[i] = Instantiate(Resources.Load("alphabet/Holder") as GameObject, new Vector2(0, 0), Quaternion.identity) as GameObject;
            tiles[i].transform.parent = this.transform;
            tiles[i].GetComponent<RectTransform>().localPosition = new Vector2(12.5f + i * 8, -11.5f);
        }

        updateMessage (message);//)"GALACTIC CONQUEST");
      

    }

	public void updateMessage(string input)
	{
		message = input;
		for (int i = 0; i < 17 && i < message.Length ; i++) 
		{

			//print (GameObject.Find (""+(i+1)).GetComponent<Image>().sprite);
			if ( message.ToCharArray()[i] != ' ')
			{
                tiles[i].GetComponent<Image>().sprite = (Resources.Load ("alphabet/"+message.ToCharArray()[i].ToString()) as GameObject).GetComponent<SpriteRenderer>().sprite;
			}
			else tiles[i].GetComponent<Image>().sprite = (Resources.Load ("alphabet/Empty") as GameObject).GetComponent<SpriteRenderer>().sprite;



		}
		for (int i = message.Length; i < 17; i++) {
            tiles[i].GetComponent<Image>().sprite = (Resources.Load ("alphabet/Empty") as GameObject).GetComponent<SpriteRenderer>().sprite;
		}



	}

	public string shift (string input)
	{
		if (originalMessage == "") {
			originalMessage = input;
			//print ("swagfres");
		}


		char firstLetter = input.ToCharArray()[0];
		char[] newMessage = new char[input.Length];
		for (int i = 0; i < input.Length-1; i++)
		{
			newMessage[i] = input.ToCharArray()[i+1];

		}
		newMessage [newMessage.Length - 1] = firstLetter;
		message = new string(newMessage);

		//print (message);
		if (originalMessage == message) {
			delay = -200;
			//print ("swag");
		}
		return message;
	}

	// Update is called once per frame
	void Update () 
	{
		if (delay++ > 40)
		{
			delay = 0;
			if (message.Length > 17) updateMessage (shift (message));

		}
	}
}
