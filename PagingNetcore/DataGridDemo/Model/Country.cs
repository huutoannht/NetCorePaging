using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataGridDemo.Model
{
  public class Country
  {
    [Key()]
    public Int32 Code { get; set; }    

    
    [StringLength(2, MinimumLength =2)]
    [Display(Name="2 Digit ISO")]
    public String ISO2{ get; set; }

    [StringLength(3, MinimumLength = 3)]
    [Display(Name = "3 Digit ISO")]
    public String ISO3 { get; set; }

    public String Name { get; set; }
  }
}
