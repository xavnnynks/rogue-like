using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rogue_like
{
    internal class Program
    {
        class Player
        {
            public string name;
            public int hp;
            public int maxhp;
            public int damage;
            public string aid;
            public Weapon weapon;
            public int points;
            // игрок лечится и наносит урон
        }

        class Enemy
        {
            public string name;
            public int hp;
            public int maxhp;
            public string weapon;
            public int damage;
            // урон
        }

        class Aid
        {
            public string name;
            public int hp;
        }

        class Weapon
        {
            public string name;
            public string type; // для удобства
            public int damage;
            public int hp;
        }

        static void generateEnemy(int x, Enemy e)
        {
            switch (x)
            {
                case 0:
                    e.name = "Зомби";
                    e.hp = 35;
                    e.weapon = "Лопата";
                    e.damage = 15;
                    break;
                case 1:
                    e.name = "Лич";
                    e.hp = 70;
                    e.weapon = "Эскалибур";
                    e.damage = 50;
                    break;
                case 2:
                    e.name = "Варвар";
                    e.hp = 30;
                    e.weapon = "Кинжал";
                    e.damage = 10;
                    break;
                case 3:
                    e.name = "Орк";
                    e.hp = 30;
                    e.weapon = "Ржавый топор";
                    e.damage = 20;
                    break;
                case 4:
                    e.name = "Тролль";
                    e.hp = 25;
                    e.weapon = "Булава";
                    e.damage = 25;
                    break;
                //для скелета можно попробовать двойные атаки
                case 5:
                    e.name = "Скелет";
                    e.hp = 10;
                    e.weapon = "Костяной лук";
                    e.damage = 4;
                    break;
            }
        }

        static void generateWeapon(int x, Player y, Weapon w)
        {
            switch (x)
            {
                case 0:
                    w.name = "Эскалибур";
                    w.damage = 20;
                    w.type = "меч";
                    w.hp = 100;
                    y.weapon = w;
                    y.damage = w.damage;
                    break;
                case 1:
                    w.name = "Фламберг";
                    w.damage = 15;
                    w.type = "меч";
                    w.hp = 60;
                    y.weapon = w;
                    y.damage = w.damage;
                    break;
                case 2:
                    w.name = "Бальмунг";
                    w.damage = 10;
                    w.type = "меч";
                    w.hp = 70;
                    y.weapon = w;
                    y.damage = w.damage;
                    break;
                case 3:
                    w.name = "Эльфийский лук";
                    w.damage = 8;
                    w.type = "лук";
                    w.hp = 60;
                    y.weapon = w;
                    y.damage = w.damage;
                    break;
                case 4:
                    w.name = "Кладенец";
                    w.damage = 10;
                    w.type = "меч";
                    w.hp = 80;
                    y.weapon = w;
                    y.damage = w.damage;
                    break;
                case 5:
                    w.name = "Зу-ль-фикар";
                    w.damage = 25;
                    w.type = "меч"; w.hp = 10;
                    y.weapon = w;
                    y.damage = w.damage;
                    break;
            }

        }

        static void attack(Enemy e, Player p)
        {
            
            if (e.hp <= p.damage)
            {
                e.hp = 0;
            }
            else if (e.hp >= p.damage)
            {
                e.hp -= p.damage;
                Console.WriteLine($"Вы наносите {p.damage} урона.");
                Console.WriteLine($"Противник {e.name} ударил вас и нанес {e.damage} урона!");
                if (p.hp > e.damage)
                {
                    p.hp -= e.damage;
                    Console.WriteLine($"У противника {e.hp}hp, у вас {p.hp}hp.");
                }
                else
                {
                    p.hp = 0;
                }
            }
           
        }

        static void flee(Enemy e, Player p)
        {
            Console.WriteLine("Вы пропускаете ход! Очередь противника.");
            Console.WriteLine($"Противник {e.name} ударил вас и нанес {e.damage} урона!");
            p.hp -= e.damage;
            Console.WriteLine($"У противника {e.hp}hp, у вас {p.hp}hp.");
        }

        static void heal(Enemy e, Player p, Aid a)
        {
            if (p.hp == p.maxhp) // если хп максимальное
            {
                Console.WriteLine($"У вас максимальный показатель hp.");
            }
            else if (p.hp + a.hp > p.maxhp) // если хп+аптечка больше максимального
            {
                p.hp = 100;
                Console.WriteLine($"Вы восполнили hp!");
            }
            else
            {
                Console.WriteLine($"Вы используете {a.name}!");
                p.hp += a.hp;
                Console.WriteLine($"Ваше hp восполнилось на {a.hp} единиц.");
            }
            Console.WriteLine($"У вашего противника {e.name} {e.hp}hp. У вас {p.hp}hp.");
        }

        static void game(int x, Enemy e, Player p, Aid a)
        {
            switch(x)
            {
                case 1:
                    attack(e, p);
                    break;
                case 2:
                    flee(e, p);
                    break;
                case 3:
                    heal(e, p, a);
                    break;
            }
        }

        static void Main(string[] args)
        {
            // начало, пользователь вводит имя
            Console.WriteLine("Добро пожаловать, воин! Введи свое имя.");
            Player player = new Player();
            player.name = Console.ReadLine();
            Console.WriteLine($"Рад познакомиться {player.name}!");

            // даем старт
            Random rnd = new Random();
            // хп игрока
            player.maxhp = 100;
            player.hp = 100;
            player.damage = 20;
            player.points = 0;
            // аптечка
            Aid aid = new Aid();
            aid.name = "средняя аптечка";
            aid.hp = 10;
            // меч
            Weapon flamberg = new Weapon();
            int sword = rnd.Next(0, 5);

            generateWeapon(sword, player, flamberg);

            Console.WriteLine($"Вам был ниспослан {flamberg.type} {player.weapon.name}({player.weapon.hp}def), а также {aid.name}({aid.hp}hp). У вас {player.hp}hp.");

            int value = rnd.Next(0, 5);
            Enemy enemy = new Enemy();

            generateEnemy(value, enemy);
            Console.WriteLine($"{player.name} встречает врага {enemy.name} ({enemy.hp}hp), у врага на поясе сияет оружие {enemy.weapon}({enemy.damage}dmg)");
            Console.WriteLine("Что вы будете делать?\n1. Ударить\n2. Пропустить ход\n3. Использовать аптечку");

            do
            {
                int x;
                x = Convert.ToInt32(Console.ReadLine());
                if (x != 1 ^ x != 2 ^ x != 3)
                {
                    Console.WriteLine("Вы ввели неверное значение. Попробуйте еще раз!");
                }
                else
                {
                    game(x, enemy, player, aid);
                }
                if (enemy.hp <= 0)
                {
                    player.points += enemy.damage;
                    Console.WriteLine($"Вы победили противника! У вас {player.points} очков!");
                    value = rnd.Next(0, 5);
                    generateEnemy(value, enemy);
                    Console.WriteLine($"{player.name} встречает врага {enemy.name} ({enemy.hp}hp), у врага на поясе сияет оружие {enemy.weapon}({enemy.damage}dmg)");
                    Console.WriteLine("Что вы будете делать?\n1. Ударить\n2. Пропустить ход\n3. Использовать аптечку");
                }
                if (player.hp == 0)
                {
                    Console.WriteLine($"Игра закончена! Вы были убиты с {player.points} очками!");
                }
            } while (player.hp > 0);

            Console.ReadLine();
        }
    }
}
