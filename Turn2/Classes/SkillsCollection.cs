using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turn2.Models;

namespace Turn2.Classes
{
    public class SkillsCollection : Skills
    { 
        public List<Skills> SkillList()
        {
            //Coder notes: This was a Dictonary before
            
            //Skills Data = new Skills();

            /*Data = new Skills { Id = 1, Name = "Tackle", Damage = 20 };
            Data = new Skills { Id = 2, Name = "Kick", Damage = 15 };*/
            List<Skills> list = new List<Skills>()
            {
                { new Skills { Id = 1, Name = "Attack", Damage = 5, Accuracy = 90, Description = "A basic nimble attack." } },
                { new Skills { Id = 2, Name = "Tackle", Damage = 20, Accuracy = 70, Description = "A Bash attack." } },
                { new Skills { Id = 3, Name = "Kick", Damage = 15, Accuracy = 45, Description = "An amazing kick right in the face."} }
            };


            return list;
        }

        public SkillsCollection()
        {
            SkillList();
        }       
    }
}

