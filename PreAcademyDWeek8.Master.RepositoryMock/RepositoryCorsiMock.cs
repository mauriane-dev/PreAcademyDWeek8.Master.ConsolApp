using PreAcademyDWeek8.Master.Core.Entities;
using PreAcademyDWeek8.Master.Core.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreAcademyDWeek8.Master.RepositoryMock
{
    public class RepositoryCorsiMock : IRepositoryCorsi
    {
        public static List<Corso> Corsi = new List<Corso>()
        {
            new Corso{CorsoCodice="C-01", Nome="Pre-Academy D", Descrizione= "C# corso base"},
            new Corso{CorsoCodice="C-02", Nome="Academy D", Descrizione= "C# corso avanzato"},
        };

        public Corso Add(Corso item)
        {
            Corsi.Add(item);
            return item;
        }

        public bool Delete(Corso item)
        {
            Corsi.Remove(item);
            return true;
        }

        public List<Corso> GetAll()
        {
            return Corsi;
        }

        public Corso GetByCode(string codice)
        {
            foreach (var item in Corsi)
            {
                if (item.CorsoCodice == codice)
                {
                    return item;
                }
            }
            return null;
        }

        public Corso Update(Corso item)
        {
            foreach (var c in Corsi)
            {
                if (c.CorsoCodice == item.CorsoCodice)
                {
                    c.Nome = item.Nome;
                    c.Descrizione = item.Descrizione;
                    return c;
                }
            }
            return null;
        }
    }
}
