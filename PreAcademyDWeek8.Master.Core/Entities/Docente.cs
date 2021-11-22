using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreAcademyDWeek8.Master.Core.Entities
{
    public class Docente: Persona
    {
        public string Telefono { get; set; }
        public List<Lezione> Lezioni { get; set; } = new List<Lezione>();

        public override string ToString()
        {
            return $"Id: {ID}\t{Nome}\t{Cognome}\t Altre info: {Email} - {Telefono}";
        }
    }
}
