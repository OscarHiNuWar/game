using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turn2.Models;

namespace Turn2.Classes
{
    public class ActorsCollection : Actor
    {
        public Dictionary<int, Actor> ActorList()
        {
            Actor actors = new Actor();

            Dictionary<int, Actor> ActorList = new Dictionary<int, Actor>()
            {
                { 1, new Actor { Id = 1, Name = "Player", IsPlayeble = true, Hp = 100, Atk = 30, Def = 20} },
                { 2, new Actor { Id = 2, Name = "Troll", IsPlayeble = false, Hp = 150, Atk = 8, Def = 6} },
                { 3, new Actor { Id = 3, Name = "Goblin", IsPlayeble = false, Hp = 50, Atk = 15, Def = 10} }

            };

            return ActorList;
        }

        public ActorsCollection()
        {
            ActorList();
        }
    }
}
