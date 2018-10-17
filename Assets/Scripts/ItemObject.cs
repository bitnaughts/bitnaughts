
public class ItemObject
{
    //	public string name;
    //	string baseName;
    //	string shortName;
    //    string description;
    //	string size;
    //ItemObject[] attachments;

    public string[] parts = new string[7];

    public float[] coefficients = new float[6];


    //velocity, fire rate, rotation speed, accuracy, clip size

    public ItemObject(string[] items)
    {
        init(items, "small");
    }

    public ItemObject(string[] items, string size)
    {
        init(items, size);
    }

    void init(string[] items, string size)
    {
        parts = items;
        coefficients = Values.modifyCoefficients(coefficients, parts[parts.Length - 1], parts.Length - 1);
        for (int i = 0; i < parts.Length - 1; i++)
        {
            coefficients = Values.modifyCoefficients(coefficients, parts[i], i); 
        }
        //this.size = size;
	}


   


}
