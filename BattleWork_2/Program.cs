using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BattleWork_2
{

    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            Console.WriteLine("请分配你的30点属性，苏克蓝\n");
            Console.WriteLine("体力，力量，气运");
            Character c1 = new Character("苏克蓝", double.Parse(Console.ReadLine()) * 50, int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()));
            c1.SetSkill("求求你", 30, 0.3, "沉默"); //提醒：该概率可和角色的命中属性有关。
            c1.SetSkill("弄死你", 50, 0.1, "暴击");
            c1.SetSkill("变身蓝毒兽", 0, 1, "变身");
            c1.SetSkill("切你屌", 10000, 0.05, "秒杀");
            Console.WriteLine("请分配你的30点属性，寒星剑\n");
            Console.WriteLine("体力，力量，气运");
            Character c2 = new Character("寒星剑", double.Parse(Console.ReadLine()) * 50, int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()));
            c2.SetSkill("饶了我吧", 30, 0.3, "沉默");
            c2.SetSkill("零封苏克蓝", 50, 0.1, "暴击");
            c2.SetSkill("天元之力", 0, 1, "变身");
            c2.SetSkill("切你屌", 10000, 0.05, "秒杀");
            int step_of_c1 = 0;
            int step_of_c2 = 0;
            Regex regex = new Regex(@"^[1-9]$");

            while (!c1.IsDead() && !c2.IsDead())
            {
                int Choose = 0;
                if (c1.status == "正常")
                {
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine($"现在轮到{c1.name}了，请选择使用的技能");
                    c1.GetSkill();
                    while (true)
                    {
                        string input = Console.ReadLine();
                        if (regex.IsMatch(input))
                        {
                            Choose = int.Parse(input) - 1;
                            break;
                        }
                    }
                    double AttackofSkill = c1.GetInitAttack()
                        + Skill.SkillAttackTable[c1.skilltemp[Choose].name];
                    if (c1.IsSkillMiss(Choose))
                    {
                        Console.WriteLine("\n由于学艺不精，该技能没有发挥任何效果\n");
                    }
                    else
                    {
                        switch (Skill.SkillTypeTable[c1.skilltemp[Choose].name])
                        {
                            case "暴击":
                                c2.CostHp(2 * AttackofSkill);
                                Console.WriteLine($"\n技能暴击！！对{c2.name}造成了{2 * AttackofSkill}点伤害\n");
                                break;
                            case "变身":
                                c1.CostHp(c1.GetHp() / 2);
                                c1.SetInitAttack(c1.GetInitAttack() * 2);
                                double de_accu = random.Next(50, 80) * 0.01;
                                c2.debuf_accurate = c2.debuf_accurate * de_accu;
                                Console.WriteLine($"\n苏克蓝觉醒血脉之力，变身成为蓝毒兽，血量降低一半，攻击增大一倍,目标的命中被降低了{100 - de_accu * 100}%\n");
                                break;
                            case "沉默":
                                c2.CostHp(AttackofSkill);
                                c2.status = Skill.SkillTypeTable[c1.skilltemp[Choose].name];
                                step_of_c2 = 1;
                                Console.WriteLine($"\n技能成功命中目标，对{c2.name}造成了{AttackofSkill}点伤害\n");
                                break;
                            case "秒杀":
                                c2.CostHp(c2.GetHp());
                                Console.WriteLine($"\n苏克蓝一刀砍断了寒星剑的屌，{c2.name}当场被其抹杀\n");
                                break;
                        }
                    }
                    Console.WriteLine($"{c1}");
                    Console.WriteLine($"{c2}");
                    if (c2.IsDead())
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine($"\n{c1.name}处于{c1.status}状态\n");
                    step_of_c1 -= 1;
                    if (step_of_c1 <= 0)
                    {
                        c1.status = "正常";
                    }
                }

                if (c2.status == "正常")
                {
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine($"现在轮到{c2.name}了，请选择使用的技能");
                    c2.GetSkill();
                    while (true)
                    {
                        string input = Console.ReadLine();
                        if (regex.IsMatch(input))
                        {
                            Choose = int.Parse(input) - 1;
                            break;
                        }
                    }
                    double AttackofSkill = c2.GetInitAttack()
                        + Skill.SkillAttackTable[c2.skilltemp[Choose].name];
                    if (c2.IsSkillMiss(Choose))
                    {
                        Console.WriteLine("\n由于学艺不精，该技能没有发挥任何效果\n");

                    }
                    else
                    {
                        switch (Skill.SkillTypeTable[c2.skilltemp[Choose].name])
                        {
                            case "暴击":
                                c1.CostHp(2 * AttackofSkill);
                                Console.WriteLine($"\n技能暴击！！对{c1.name}造成了{2 * AttackofSkill}点伤害\n");
                                break;
                            case "变身":

                                c2.SetInitAttack(c2.GetInitAttack() * 2);
                                double accu = random.Next(140, 160) * 0.01;
                                c2.buf_accurate = c2.buf_accurate * accu;
                                Console.WriteLine($"\n小心！！寒星剑顿悟了天地规则，片刻间天地皆为他的棋子, 攻击力翻倍，命中率提高了{accu * 100 - 100}%\n");
                                break;
                            case "沉默":
                                c1.CostHp(AttackofSkill);
                                c1.status = Skill.SkillTypeTable[c2.skilltemp[Choose].name];
                                step_of_c1 = 1;
                                Console.WriteLine($"\n技能成功命中目标，对{c1.name}造成了{AttackofSkill}点伤害\n");
                                break;
                            case "秒杀":
                                c1.CostHp(c1.GetHp());
                                Console.WriteLine($"\n寒星剑一刀砍断了苏克蓝的屌，{c1.name}当场被其抹杀\n");
                                break;
                        }
                    }
                    Console.WriteLine($"{c1}");
                    Console.WriteLine($"{c2}");
                    if (c1.IsDead())
                    {
                        break;
                    }
                }

                else
                {
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine($"\n{c2.name}处于{c2.status}状态\n");
                    step_of_c2 -= 1;
                    if (step_of_c2 <= 0)
                    {
                        c2.status = "正常";
                    }
                }
            }
            if (c1.IsDead())
            {
                Console.WriteLine($"\n----{c2.name}获得了胜利！----");
            }
            else
            {
                Console.WriteLine($"\n----{c1.name}获得了胜利！----");
            }

            Console.ReadKey();
        }
    }
}
