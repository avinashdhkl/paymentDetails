using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class image
    {
        [Key]
        public int imgID { get; set; }
        public string imgcaption { get; set; }
        public string imgName { get; set; }


    }
}
