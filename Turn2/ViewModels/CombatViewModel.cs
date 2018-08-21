using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turn2.Models;

namespace Turn2.ViewModels
{
    public class CombatViewModel
    {
        //public Player player { get; set; }
        //public Enemy enemy { get; set; }
        public Actor actor { get; set; }
        public Dictionary<int, Actor> ActorList { get; set; }
        public string Skill { get; set; }
        public string SelectedEnemy { get; set; }
        public Dictionary<int, Skills> SkillList { get; set; }
        public List<string> Messages { get; set; }

    }
}
