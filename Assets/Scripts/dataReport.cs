using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class dataReport : MonoBehaviour {
	public bool opened;
	public bool over;

	public GameObject items;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.GetComponent<Image> ().enabled = opened;
	}

	public void open(bool opening, string temp)
	{
		opened = opening;
		items.SetActive(opening);
		if (opening)
		{
			items.GetComponent<dataInformationDisplay>().display(temp);
		}
	}
	public string build()
	{
		return items.GetComponent<dataInformationDisplay> ().text.GetComponent<Text> ().text.Split ('\n') [0];
	}
}
