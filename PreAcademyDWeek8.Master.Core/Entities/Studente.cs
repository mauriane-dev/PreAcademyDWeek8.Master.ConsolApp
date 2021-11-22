using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreAcademyDWeek8.Master.Core.Entities
{
    public class Studente: Persona
    {
        public string TitoloStudio { get; set; }
        public DateTime DataNascita { get; set; }

        //Fk verso Corso
        public string CorsoCodice { get; set; }
        public Corso Corso { get; set; }

        public override string ToString()
        {
            return $"Id: {ID}\t{Nome}\t{Cognome}\tnato il:{DataNascita.ToShortDateString()}\t Altre info: {Email} - {TitoloStudio}";
        }
    }
}
