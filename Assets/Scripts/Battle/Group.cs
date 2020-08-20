using System.Collections.Generic;

public class Group
{
    public List<UnitTeamData> allyTeam = new List<UnitTeamData>();
    public List<UnitTeamData> enemyTeam = new List<UnitTeamData>();

    public Group()
    {
        allyTeam.Add(new UnitTeamData() { team = "Ally", place = "1", type = "Recruit" });
        allyTeam.Add(new UnitTeamData() { team = "Ally", place = "2", type = "Hero" });
        allyTeam.Add(new UnitTeamData() { team = "Ally", place = "3", type = "Recruit" });
        allyTeam.Add(new UnitTeamData() { team = "Ally", place = "4", type = "Technician" });
        allyTeam.Add(new UnitTeamData() { team = "Ally", place = "5", type = "Gunner" });

        enemyTeam.Add(new UnitTeamData() { team = "Enemy", place = "1", type = "Recruit" });
        enemyTeam.Add(new UnitTeamData() { team = "Enemy", place = "2", type = "Hero" });
        enemyTeam.Add(new UnitTeamData() { team = "Enemy", place = "3", type = "Recruit" });
        enemyTeam.Add(new UnitTeamData() { team = "Enemy", place = "4", type = "Technician" });
        enemyTeam.Add(new UnitTeamData() { team = "Enemy", place = "5", type = "Gunner" });
    }
}

public class UnitTeamData
{
    public string team;
    public string place;
    public string type;

    public string weapon;
    public string armor;
    public string body;
}