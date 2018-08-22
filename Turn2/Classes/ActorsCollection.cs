using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turn2.Models;

namespace Turn2.Classes
{
    public class ActorsCollection : Actor
    {
        public List<Actor> ActorList()
        {
            Actor actors = new Actor();

            List<Actor> ActorList = new List<Actor>()
            {
                { new Actor { Id = 0, Name = "Player", IsPlayeble = true, Hp = 100, Atk = 30, Def = 20, Agi = 15 + new Random().Next(0,30) } },
                { new Actor { Id = 1, Name = "Troll", IsPlayeble = false, Hp = 150, Atk = 8, Def = 6, Agi = 20 } },
                { new Actor { Id = 2, Name = "Goblin", IsPlayeble = false, Hp = 50, Atk = 15, Def = 10, Agi = 15 } }

            };

            return ActorList;
        }

        public ActorsCollection()
        {
            ActorList();
        }
    }
}
