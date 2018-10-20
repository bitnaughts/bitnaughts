using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class textAnimator : MonoBehaviour {

	public static char[] possibleSymbols;
	public string tempText;
	public char[] tempTextArray;
	public string text;
	public int amountText;
	public bool running;
	// Use this for initialization
	void Start () {
        string possibleSymbolsList = "abcdefghijklmnopqrstuvqxyzABCDEFGHIJKLMNOQRSTUVWXYZ11223344556677889900!@#$%^&*";
        possibleSymbols = possibleSymbolsList.ToCharArray(); //new char[] {'a','b','c','d','e','f','g','h','i','j','k','l','m','m','o','p','q','r','s','t','u','v','w','x','y','z','1','2','3','4','5','6','7','8','9','0','!','@','#','$','%','&'};
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (running)// && Random.value < .5f) 
		{
			for (int i = 0; i < amountText-5 && i < text.Length; i++)
			{
				if (i < amountText-10) tempTextArray[i] = text.Substring(i,1).ToCharArray()[0];
				else tempTextArray[i] = possibleSymbols [((int)(Random.value * possibleSymbols.Length))];
				//if (i < amountText-5) tempText = tempText.Substring(0,i) + text.Substring(i,1) + tempText.Substring (i+1,tempText.Length - i);
				//else tempText.Substring(0,i) = possibleSymbols [((int)(Random.value * possibleSymbols.Length))].ToString ();
			}

			amountText++;
			if (amountText > text.Length + 10) running = false;
			//this.GetComponent<Text> () = tempText;
			//if (amountText < text.Length) {
		//		tempText += possibleSymbols [((int)(Random.value * possibleSymbols.Length))].ToString ();

			//} else
			//	running = false;
			tempText = "";
			for (int i = 0; i < tempTextArray.Length;i++)
				tempText += tempTextArray[i].ToString();
			this.GetComponent<Text> ().text = tempText;
		}
	}

	public void changeText(string text)
	{
		tempTextArray = new char[text.Length];
		tempText = "";
		this.text = text;
		amountText = 1;
		running = true;
	}

}
