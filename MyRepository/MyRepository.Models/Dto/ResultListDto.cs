using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRepository.Models.Dto
{
    public class ResultListDto
    {
       
          public  string sEcho { get; set; }
          public  int iTotalRecords { get; set; }
          public   int iTotalDisplayRecords { get; set; }
          public List<UserDto> aaData { get; set; }
        
    }
}
