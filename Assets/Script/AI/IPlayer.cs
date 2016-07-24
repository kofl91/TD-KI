public interface IPlayer
{
    bool SpendMoney(int cost);
    void BuildTower(TowerStructure tower, TileStructure tile);
    bool LooseLife();
    int GetMoney();
    int GetLife();
}