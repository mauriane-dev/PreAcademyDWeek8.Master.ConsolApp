using PreAcademyDWeek8.Master.Core.Entities;
using PreAcademyDWeek8.Master.Core.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreAcademyDWeek8.Master.RepositoryMock
{
    public class RepositoryStudentiMock : IRepositoryStudenti
    {
        public static List<Studente> Studenti = new List<Studente>();


        public Studente Add(Studente item)
        {
            if (Studenti.Count == 0)
            {
                item.ID = 1;
            }
            else //se la lista è piena trova l'id più alto e, dopo aver incrementato di 1, lo assegna ad item
            {
                int maxId = 1;
                foreach (var s in Studenti)
                {
                    if (s.ID > maxId)
                    {
                        maxId = s.ID;
                    }
                }
                item.ID = maxId + 1;
            }
            Studenti.Add(item);
            return item;
        }

        public bool Delete(Studente item)
        {
            Studenti.Remove(item);
            return true;
        }

        public List<Studente> GetAll()
        {
            return Studenti;
        }

        public Studente GetById(int id)
        {
            foreach (var item in Studenti)
            {
                if (item.ID == id)
                {
                    return item;
                }
            }
            return null;
        }

        public Studente Update(Studente item)
        {
            foreach (var s in Studenti)
            {
                if (s.ID == item.ID)
                {
                    s.Email = item.Email;                    
                    return s;
                }
            }
            return null;
        }
    }
}
