public interface IPackageData
{
    string Paper { get; set; }
    string Price { get; set; }
    void Initialize(string paper, string price);
}