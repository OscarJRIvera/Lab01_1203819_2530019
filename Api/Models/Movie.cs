using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Api.Models
{
    
 
    public class Movie
    {
       
        public int id { get; set; }
        
        public string director { get; set; }
      
        public double imdbrating { get; set; }
       
        public string genre { get; set; }
        
        public DateTime ReleaseDate { get; set; }
      
        public int RottenTomatoesRating { get; set; }
      
        public string Title { get; set; }

        public static int Compare_Title(Movie x, Movie y)
        {
            int r = x.Title.CompareTo(y.Title);
            return r;
        }

    }

  
 

   

}
