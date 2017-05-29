using System;
using System.Collections.Generic;
using System.Text;

namespace ElectricCarSearcher
{
  class Make
  {
    public string id { get; set; }
    public string name { get; set; }
    public string niceName { get; set; }
    public List<Model> models { get; set; }

    public Make(string id, string name, string nicename, List<Model> models) {
      this.id = id;
      this.name = name;
      this.niceName = nicename;
      this.models = models;
    }    
  }
}
