using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turn2.Models;

namespace Turn2.Classes
{
    public class SkillsCollection : Skills
    { 
        public Dictionary<int, Skills> SkillList()
        {
            
            Skills Data = new Skills();
            Actor Actor = new Actor();

            /*Data = new Skills { Id = 1, Name = "Tackle", Damage = 20 };
            Data = new Skills { Id = 2, Name = "Kick", Damage = 15 };*/
            Dictionary<int, Skills> list = new Dictionary<int, Skills>()
            {
                { 1, new Skills { Id = 1, Name = "Attack", Damage = 5, Description = "A basic nimble attack that does 5 dmg." } },
                { 2, new Skills { Id = 2, Name = "Tackle", Damage = 20, Description = "A Bash attack." } },
                { 3, new Skills { Id = 3, Name = "Kick", Damage = 15, Description = "An amazing kick right in the face."} }
            };


            return list;
        }

        public SkillsCollection()
        {
            SkillList();
        }       
    }
}

