using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Turn2.Classes;
using Turn2.Models;
using Turn2.ViewModels;

namespace Turn2.Controllers
{
    public class GameController : Controller
    {
        //readonly Player player = new Player();
        //readonly Enemy enemy = new Enemy();
        //readonly Skills skills = new Skills();
        Actor Actor = new Actor();
        ActorsCollection ActorsCollection = new ActorsCollection();
        readonly SkillsCollection collection = new SkillsCollection();
        Random random = new Random();

        public IActionResult Index()
        {
            //var player = this.Actor;
            //var enemy = this.Actor;
            //var skills = this.skills;
            //player.Hp = 100; player.Atk = 10; player.Def = 6;
            //enemy.Hp = 150; enemy.Atk = 8; enemy.Def = 5;

            Dictionary<int, Skills> collection = new Dictionary<int, Skills>();

            Dictionary<int, Actor> versus = new Dictionary<int, Actor>();

            List<string> empty = new List<string>
            {
                ""
            };

            collection = this.collection.SkillList();

            versus = ActorsCollection.ActorList();

            var combatModel = new CombatViewModel
            {
                ActorList = versus,
                //skills = skills,
                SkillList = collection,
                Messages = empty
            };

            return View(combatModel);
        }

        [HttpPost]
        public IActionResult EngageVersionUno(CombatViewModel combat)
        {
            //Version 4
            //var random = new Random().Next(1,4);
            
            Skill(combat);

            foreach (var enemies in combat.ActorList.Values.Where(x=> !x.IsPlayeble))
            {
                EnemySkill(combat, random.Next(1, 4), enemies.Id);
            }
            
            return Json(combat);
        }

        public CombatViewModel Skill(CombatViewModel combat)
        {
            //Resolucion del combate version 4 

            /*var nameValue = combat.SkillList.Values
                .Where(x => x.Name == combat.skill)
                .Select(x => x.Name)
                .Single();*/
            /*var newDef = combat.ActorList.Values
                .Where(x => !x.IsPlayeble)
                .Select(y => y.Def)
                .Single();*/

            var Value = combat.SkillList.Values
                .Where(x=> x.Name == combat.Skill)
                .Select(x => new { x.Damage, x.Name })
                .Single();

            var Stats = combat.ActorList.Values
                .Where(isPlayeble => isPlayeble.IsPlayeble)
                .Select(x => new { x.Atk, x.Def })
                .Single();

            var newDamage = (Value.Damage + Stats.Atk) - Stats.Def;

            newDamage = (newDamage <= 0) ? 1 : newDamage;

            var enemi = combat.ActorList.Values
                .Where(a => !a.IsPlayeble && a.Name == combat.SelectedEnemy)
                .Select(b => b.Id)
                .Single();


            var hp = combat.ActorList.Values
                .Where(a => !a.IsPlayeble && a.Name == combat.SelectedEnemy)
                .Select(b => b.Hp)
                .Single() - newDamage;

            combat.ActorList[enemi].Hp = hp;

            combat.Messages.Add("Player has used " + Value.Name + " and did " + newDamage + " Damage!");

            return combat;
        }

        public CombatViewModel EnemySkill(CombatViewModel combat, int decision, int Id)
        {
            //Resolucion del combate version 4

            /*var valueDmg = combat.SkillList.Values
                .Where(x => x.Id == decision)
                .Select(a => a.Damage)
                .Single();*/

            /*var hp = combat.ActorList.Values
                .Where(a => a.IsPlayeble)
                .Select(b => b.Hp)
                .Single() - damage;*/


            var SkillStats = combat.SkillList.Values
                .Where(x => x.Id == decision)
                .Select(a => new { a.Name, a.Damage })
                .Single();

            var newDamage = combat.ActorList.Values
               .Where(isPlayeble => !isPlayeble.IsPlayeble && isPlayeble.Id == Id)
               .Select(x => new { x.Atk, x.Name })
               .Single();

            var playerStats = combat.ActorList.Values
                .Where(x => x.IsPlayeble)
                .Select(y => new { y.Def, y.Hp })
                .First();

            var damage = (SkillStats.Damage + newDamage.Atk) - playerStats.Def;

            damage = (damage <= 0) ? 1 : damage;

            combat.ActorList[1].Hp = playerStats.Hp - damage;

            combat.Messages.Add(newDamage.Name + " has used "+ SkillStats.Name + " and did " + damage + " Damage!");

            return combat;
        }

        public IActionResult Victory(CombatViewModel combat)
        {
            if(combat.actor.Hp <= 0 && combat.actor.Hp > 0)
            {
                return Content("<h1 style='color: blue'> YOU WIN! </h1>");
            }
            if (combat.actor.Hp <= 0 && combat.actor.Hp > 0)
            {
                return Content("<h1 style='color: red'> YOU LOSE! </h1>");
            }
            else
            {
                return Content("<h1 style='color: blue'> IT'S A DRAW! </h1>");
            }
        }
    }
}