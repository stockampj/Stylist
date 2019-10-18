using System;
using System.Collections.Generic;


namespace Style.Models
{
    public class Style
    {
        public Style ()
        {
            this.Clients = new HashSet<Client>();
        }

        public string Name { get; set; }
        public string Specialty { get; set; }
        public int StyleId { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
   }
}