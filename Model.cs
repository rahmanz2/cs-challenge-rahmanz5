using System.Collections.Generic;

namespace ElectricCarSearcher
{
  public class Model
  {
    public string id { get; set; }
    public string name { get; set; }
    public string niceName { get; set; }
    public List<Year> years { get; set; }

    public Model(string id, string name, string niceName, List<Year> years)
    {
      this.id = id;
      this.name = name;
      this.niceName = niceName;
      this.years = years;
    }
  }
}