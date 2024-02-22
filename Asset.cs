// Asset.cs
[System.Serializable]
public class Asset
{
    public string name;
    public int quantity;

    public Asset(string _name, int _quantity)
    {
        name = _name;
        quantity = _quantity;
    }
}
