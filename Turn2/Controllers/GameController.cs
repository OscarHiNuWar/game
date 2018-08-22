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
        //readonly Actor Actor = new Actor();
        ActorsCollection ActorsCollection = new ActorsCollection();
        readonly SkillsCollection collections = new SkillsCollection();
        Random random = new Random();

        public IActionResult Index()
        {
            //var player = this.Actor;
            //var enemy = this.Actor;
            //var skills = this.skills;
            //player.Hp = 100; player.Atk = 10; player.Def = 6;
            //enemy.Hp = 150; enemy.Atk = 8; enemy.Def = 5;

            List<Skills> collection = new List<Skills>();

            List<Actor> versus = new List<Actor>();

            List<string> empty = new List<string>{""};

            collection = collections.SkillList();

            versus = ActorsCollection.ActorList();

            var combatModel = new CombatViewModel
            {
                ActorList = versus,
                SkillList = collection,
                Messages = empty
            };

            return View(combatModel);
        }

        [HttpPost]
        public IActionResult EngageVersionUno(CombatViewModel combat)
        {
            //Version 5
            //Added Hit and Miss (Accuracy)
            //var random = new Random().Next(1,4);

            /*var playerAccuracy = combat.ActorList.Where(x => x.IsPlayeble).Select(y=>y.Agi).Single() + new Random().Next(0,20);
            var enemyAccuracy = combat.ActorList.Where(x => !x.IsPlayeble).Select(y=>y.Agi).ToArray();
            var acc = enemyAccuracy[combat.ActorList.Where(x => x.Name == combat.SelectedEnemy).Select(x => x.Id-1).Single()] + new Random().Next(0, 20);

            if (playerAccuracy > acc)*/
            Skill(combat);

            foreach (var enemies in combat.ActorList.Where(x=> !x.IsPlayeble))
            {
                EnemySkill(combat, random.Next(1, 4), enemies.Id);
            }
            
            return Json(combat);
        }

        public CombatViewModel Skill(CombatViewModel combat)
        {
            //Resolucion del combate version 5 

            /*var nameValue = combat.SkillList.Values
                .Where(x => x.Name == combat.skill)
                .Select(x => x.Name)
                .Single();*/
            /*var newDef = combat.ActorList.Values
                .Where(x => !x.IsPlayeble)
                .Select(y => y.Def)
                .Single();*/
            /*var enemi = combat.ActorList
                .Where(a => !a.IsPlayeble && a.Name == combat.SelectedEnemy)
                .Select(b => b.Id)
                .Single();*/

            var EStats = combat.ActorList
                .Where(a => !a.IsPlayeble && a.Name == combat.SelectedEnemy)
                .Select(b => new { b.Hp, b.Def, b.Id })
                .Single();

            var Value = combat.SkillList
                .Where(x=> x.Name == combat.Skill)
                .Select(x => new { x.Damage, x.Name })
                .Single();

            var Stats = combat.ActorList
                .Where(isPlayeble => isPlayeble.IsPlayeble)
                .Select(x => new { x.Atk, x.Agi })
                .Single();

            var newDamage = (Value.Damage + Stats.Atk) - EStats.Def;

            newDamage = (newDamage <= 0) ? 1 : newDamage;

            var playerAccuracy = Stats.Agi + new Random().Next(0, 20);
            var enemyAccuracy = combat.ActorList.Where(x => !x.IsPlayeble).Select(y => y.Agi).ToArray();
            var acc = enemyAccuracy[combat.ActorList.Where(x => x.Name == combat.SelectedEnemy).Select(x => x.Id - 1).Single()] + new Random().Next(0, 20);

            if (playerAccuracy > acc)
            {
                combat.ActorList[EStats.Id].Hp = EStats.Hp - newDamage;
                combat.Messages.Add("Player has used " + Value.Name + " and did " + newDamage + " Damage!");
            }
            else
                combat.Messages.Add("Player has used " + Value.Name + " but missed!!");

            return combat;
        }

        public CombatViewModel EnemySkill(CombatViewModel combat, int decision, int Id)
        {
            //Resolucion del combate version 5

            /*var valueDmg = combat.SkillList.Values
                .Where(x => x.Id == decision)
                .Select(a => a.Damage)
                .Single();*/

            /*var hp = combat.ActorList.Values
                .Where(a => a.IsPlayeble)
                .Select(b => b.Hp)
                .Single() - damage;*/

            var SkillStats = combat.SkillList
                .Where(x => x.Id == decision)
                .Select(a => new { a.Name, a.Damage })
                .Single();

            var newDamage = combat.ActorList
               .Where(isPlayeble => !isPlayeble.IsPlayeble && isPlayeble.Id == Id)
               .Select(x => new { x.Atk, x.Name, x.Agi })
               .Single();

            var playerStats = combat.ActorList
                .Where(x => x.IsPlayeble)
                .Select(y => new { y.Def, y.Hp, y.Id, y.Agi })
                .First();

            var damage = (SkillStats.Damage + newDamage.Atk) - playerStats.Def;

            damage = (damage <= 0) ? 1 : damage;

            var round = newDamage.Agi + new Random().Next(0, 30);

            if ((playerStats.Agi + new Random().Next(0, 20)) < round)
            {
                combat.ActorList[playerStats.Id].Hp = playerStats.Hp - damage;
                combat.Messages.Add(newDamage.Name + " has used " + SkillStats.Name + " and did " + damage + " Damage!");
            } else
            {
                combat.Messages.Add(newDamage.Name + " has used " + SkillStats.Name + " but missed!!");
            }

            return combat;
        }
    }
}