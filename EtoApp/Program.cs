
using Eto.Forms;
using Eto.Drawing;
using System;
using System.Collections.Generic;

class Program
{
    static Character player = new("Sigma", 100, 25, 7);

    static List<Character> enemies = new()
    {
        new Character("Goblin Scout", 80, 15, 20),
        new Character("Goblin Brute", 120, 20, 23),
        new Character("Goblin King", 150, 25, 27),
    };

    static int currentEnemyIndex = 0;

    static Label lblStatus;
    static TextArea txtLog;
    static Button btnAttack, btnDefend, btnDodge, btnRun;
    static Random rnd = new();
    static int i = 0;

    [STAThread]
    static void Main()
    {
        var app = new Application(Eto.Platform.Detect);
        btnAttack = new Button { Text = "Útok" };
        btnDefend = new Button { Text = "Krýt se" };
        btnDodge = new Button { Text = "Uhnout" };
        btnRun = new Button { Text = "Konec" };

        lblStatus = new Label { Text = GetStatus(), Height = 60, Wrap = WrapMode.Word };
        txtLog = new TextArea { ReadOnly = true, Height = 200, Wrap = true, Text = "" };

        // Intro story
        AddStoryText("Zdá se býti klidný den ve Oakvillu a náš hrdina odpočívá pokorně...");
        AddStoryText("Když vtu je vesnice pod útokem goblinů...!");
        AddStoryText("Jak bráníš vesnici setkáš se tváří v tvář Goblin Scouta...");
        AddStoryText("Ti co se nepodvolí králi ZEMŘOU!!!");
        AddStoryText("Goblin Scout tě vyzval na souboj...");

        btnAttack.Click += (_, _) => { ClearLog(); PlayerAttack(); };
        btnDefend.Click += (_, _) => { ClearLog(); PlayerDefend(); };
        btnDodge.Click += (_, _) => { ClearLog(); PlayerDodge(); };
        btnRun.Click += (_, _) => { ClearLog(); PlayerRun(); };

        var layout = new DynamicLayout { Padding = 10, Spacing = new Size(5, 5) };
        layout.Add(lblStatus);
        layout.Add(txtLog);
        layout.AddSeparateRow(btnAttack, btnDefend, btnDodge, btnRun);

        app.Run(new Form { Title = "Goblin Quest", ClientSize = new Size(400, 350), Content = layout });
    }

    static string GetStatus()
    {
        if (currentEnemyIndex < enemies.Count)
        {
            var enemy = enemies[currentEnemyIndex];
            return $"Hráč: {player.Name} | HP: {player.HP}\nNepřítel: {enemy.Name} | HP: {enemy.HP}";
        }
        else
        {
            return $"Hráč: {player.Name} | HP: {player.HP}\nNepřítel: žádný (všichni poraženi)";
        }
    }

    static void ClearLog() => txtLog.Text = "";

    static void Log(string text) => txtLog.Text += text + "\n";

    static void UpdateStatus() => lblStatus.Text = GetStatus();

    static void AddStoryText(string story) => txtLog.Text += story + "\n\n";

    static void DisableButtons() => btnAttack.Enabled = btnDefend.Enabled = btnDodge.Enabled = false;

    static void PlayerAttack()
    {
        int dmg = Math.Max(0, player.Attack(rnd) - enemies[currentEnemyIndex].Defense);
        enemies[currentEnemyIndex].HP = Math.Max(0, enemies[currentEnemyIndex].HP - dmg);
        Log($"{player.Name} zasáhl {enemies[currentEnemyIndex].Name} za {dmg} HP.");

        if (enemies[currentEnemyIndex].HP == 0)
        {
            Log($"{enemies[currentEnemyIndex].Name} byl poražen!");

            if (currentEnemyIndex == 0)
            {
                AddStoryText("Jak Goblin Scout umírá na zemi, s posledním dechem řekl svá poslední slova...");
                AddStoryText("Však nakonec podlehnete králi...");
                AddStoryText("Vesnice byla zachráněna, ale hrdina cítí, že není úplně konec...");
                AddStoryText("Proto se hrdina výdá do Temné země, aby porazil krále jednou provždy...");
                AddStoryText("Nejprve však musí projít Lesem Utrpení...");
                AddStoryText("Jak náš hrdina prochází lesem utrpení, uslyšel vtu hlasitý řev...");
                AddStoryText("Hrdina se otočil směrem odkud byl slyšet řev s mečem v rukách...");
                AddStoryText("A najednou se zjevil zdroj z kterého ten řev pocházel...");
                AddStoryText("Obrovský Goblin brute...");
                AddStoryText("Najednou dostal náš hrdina mráz po zádech a věděl, že toto nebude lehký boj...");
                AddStoryText("Goblin brute se chrlí do boje...");
            }
            else if (currentEnemyIndex == 1)
            {
                AddStoryText("Jak Goblin brute už byl zbaven sil, spadl na zem a bylo slyšet jeho obrovské váhy...");
                AddStoryText("Hrdina využil situace a zapíchl svůj meč do hlavy Goblina...");
                AddStoryText("Hrdina tedy opět zvítězil...");
                AddStoryText("Jak hrdina odcházel z lesa, najednou pocítil silné větry Temné Země...");
                AddStoryText("A konečně spatřil hrad Krále...");
                AddStoryText("Teď už není cesty zpět...");
                AddStoryText("Hrdina se ocitl před dveřmi hlavního sálu..");
                AddStoryText("Hrad se zdál být bez obrany, protože král všechny skřety vyslal do války...");
                AddStoryText("Jenom pár služebníků zbylo, kteří nebyli ochotni hrdinu napadnout...");
                AddStoryText("Jak hrdina otvíral dveře, pomyslel na svoji cestu...");
                AddStoryText("A konečně ho zahlédl... KRÁLE GOBLINŮ...");
                AddStoryText("Nikdy hrdina neviděl něco tak hnusného jako byl on...");
                AddStoryText("Král promluvil svým hlubokým tónem...");
                AddStoryText("Konečně... Hrdina Oakvillu... ten, který se mě rozhodl zastavit...");
                AddStoryText("Jsi ještě menší než jsem čekal... ahahahahahahhahahahahahahaaa...");
                AddStoryText("Jak se Král zvedal ze svého trůnu z ryzího zlata, začaly mu pod nohama praskat kachličky...");
                AddStoryText("Popadl své kladivo a zřekl...");
                AddStoryText("JÁ JSEM KRÁL GOBLINŮ A ZDE ZHEBNEŠ...");
                AddStoryText("Finální boj začal...");
            }
            else if (currentEnemyIndex == 2) 
            {
                ClearLog();
                AddStoryText("Jak Goblin King je velmi raněný, ztratil sílu, aby se bránil...");
                AddStoryText("Hrdina tedy s poslednímy zbytky sil vzal svůj meč...");
                AddStoryText("a propíchl Králi srdce...");
                AddStoryText("Z Králových úst vyšel vřískot a padl na zem mrtev...");
                AddStoryText("Bez velení se zbytky goblinů nemohli poradit a jednotlivé armády padly...");
                AddStoryText("Hrdina si vzal Královu korunu jako trofej a vydal se zpět domů...");
                AddStoryText("Jak pomalu přicházel hrdina domů, z dáli už ho vítali vesničané...");
                AddStoryText("Pro jeho úspěch svolali velkou hostinu a všichni byli šťastni...");
                AddStoryText("The End");

                DisableButtons();
                UpdateStatus();
                return; 
            }

            currentEnemyIndex++;
            if (currentEnemyIndex < enemies.Count)
            {
                Log($"Objevuje se nový nepřítel: {enemies[currentEnemyIndex].Name}!");
            }

            player.HP += 30;
            Log($"{player.Name} získal 20 HP za poražení nepřítele!");
        }
        else
        {
            EnemyTurn();
        }

        UpdateStatus();
    }

    static void PlayerDefend()
    {
        Log($"{player.Name} se brání a sníží poškození příštího útoku.");
        player.IsDefending = true;
        EnemyTurn();
        UpdateStatus();
    }

    static void PlayerDodge()
    {
        Log($"{player.Name} se snaží uhnout s 50% šancí.");
        player.IsDodging = true;
        EnemyTurn();
        UpdateStatus();
    }

    static void PlayerRun()
    {
        Log($"{player.Name} ukončil.");
        DisableButtons();
        UpdateStatus();
    }

    static void EnemyTurn()
    {
        i++;
        if (i % 2 != 0) return;

        if (currentEnemyIndex >= enemies.Count)
            return;

        int dmg = enemies[currentEnemyIndex].Attack(rnd) - player.Defense;

        if (player.IsDefending)
        {
            dmg /= 2;
            player.IsDefending = false;
            Log($"{player.Name} snížil poškození protože se bránil.");
        }

        if (player.IsDodging)
        {
            if (rnd.NextDouble() < 0.5)
            {
                dmg = 0;
                Log($"{player.Name} se vyhnul útoku.");
            }
            else Log($"{player.Name} se nevyhnul útoku.");
            player.IsDodging = false;
        }

        dmg = Math.Max(0, dmg);
        player.HP = Math.Max(0, player.HP - dmg);
        Log($"{enemies[currentEnemyIndex].Name} útočí a způsobuje {dmg} poškození.");

        if (player.HP == 0)
        {
            Log($"{player.Name} byl poražen! Konec hry.");
            DisableButtons();
        }

        UpdateStatus();
    }
}

class Character
{
    public string Name;
    public int HP;
    public int AttackPower;
    public int Defense;

    public bool IsDefending = false;
    public bool IsDodging = false;

    public Character(string name, int hp, int attack, int defense)
    {
        Name = name;
        HP = hp;
        AttackPower = attack;
        Defense = defense;
    }

    public int Attack(Random rnd)
    {
        int variability = rnd.Next(10, 20);
        return AttackPower + variability;
    }
}
