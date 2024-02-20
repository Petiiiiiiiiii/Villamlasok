using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA240212
{
    internal class Nap
    {
        public Dictionary<int,int> OrakEsDbVillam { get; set; }

        public Nap(string sor)
        {
            var atmeneti = sor.Split(';');
            this.OrakEsDbVillam = new();
            for (int i = 0; i < atmeneti.Length; i++)
                this.OrakEsDbVillam.Add(i + 1, Convert.ToInt32(atmeneti[i]));
        }

        public override string ToString()
        {
            return $"{string.Join(" ",this.OrakEsDbVillam.Values)}";
        }
    }
}
