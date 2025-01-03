using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FİNALODEV.MODEL
{
    public class Yapilacaklar
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsCompleted { get; set; }

        public Yapilacaklar()
        {
            Id = Guid.NewGuid().ToString();
            IsCompleted = false;
        }
    }
}