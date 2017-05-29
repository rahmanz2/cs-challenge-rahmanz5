namespace ElectricCarSearcher
{
  public class Year
  {
    public string id { get; set; }
    public string year { get; set; }

    public Year(string id, string year)
    {
      this.id = id;
      this.year = year;
    }
  }
}