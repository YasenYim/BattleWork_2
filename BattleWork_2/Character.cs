using System;
using System.Collections.Generic;
using System.Text;

namespace BattleWork_2
{
    class Skill
    {
        public string name;
        private double attack;  // attack or heal;
        private double prob; // 特殊技能发动几率
        static public Dictionary<string, double> SkillAttackTable = new Dictionary<string, double>(); // 攻击数值
        static public Dictionary<string, double> SkillProbTable = new Dictionary<string, double>();// 暴击概率
        static public Dictionary<string, string> SkillTypeTable = new Dictionary<string, string>();// 眩晕概率
        public Skill()
        {

        }
        public Skill(string name, double attack)
        {
            this.name = name;
            this.attack = attack;
            SkillAttackTable[name] = attack;
        }
        public Skill(string name, double attack, double prob, string type)
        {
            this.name = name;
            this.attack = attack;
            SkillAttackTable[name] = attack;
            SkillProbTable[name] = prob;
            SkillTypeTable[name] = type;
        }

        public override string ToString()
        {
            return $"【{name}】.基础伤害{attack}点 ,【{SkillTypeTable[name]}系技能】"; //这里可以加台词{lang}
        }
    }


    // 角色类
    class Character
    {
        public string name;                 // 角色名称
        private double hp;                  // 角色血量
        private double initial_attack;      // 初始血量
        private double initial_prob;
        public string status = "正常";
        public double debuf_accurate = 1;
        public double buf_accurate = 1;

        public Character(string name, double hp, int strength, int attention)
        {
            this.name = name;
            this.hp = hp;
            this.initial_attack = strength * 10;
            this.initial_prob = attention * 0.01;
        }
        public Skill skill = new Skill();
        public List<Skill> skilltemp = new List<Skill>();

        public void GetSkill()
        {
            for (int ind = 0; ind < skilltemp.Count; ind++)
            {
                Console.WriteLine($"{ind + 1} {skilltemp[ind]}");
            }
        }

       
        public void SetSkill(string name, double attack, double prob, string type)
        {
            skill = new Skill(name, attack, initial_prob + prob, type);
            skilltemp.Add(skill);
        }
        public void SetSkill(string name, double attack)
        {
            skill = new Skill(name, attack);
            skilltemp.Add(skill);
        }
        public bool IsDead()
        {
            return hp <= 0;
        }
        public double GetHp()
        {
            return hp;
        }
        public double GetInitAttack()
        {
            return initial_attack;
        }
        public void SetInitAttack(double attack)
        {
            initial_attack = attack;
        }
        public void CostHp(double cost)
        {
            hp -= cost;
        }

        public override string ToString()
        {
            if (hp < 0)
            {
                hp = 0;
            }
            return $"{name}还剩余血量：{hp}点";
        }
        public bool IsSkillMiss(int Choose)
        {
            Random rand = new Random();
            return rand.Next(0, 100) > (100 * Skill.SkillProbTable[skilltemp[Choose].name]) * debuf_accurate * buf_accurate;
        }
    }
}
