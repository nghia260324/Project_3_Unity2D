public class Character
{
    public string id;
    public string name;
    public int damage;
    public int speed;
    public int health;
    public int level;
    public int currentExp;
    public string idInventory;

    public Character() { }
    public Character(string id,string name,int damage,int speed,
        int health,int level,int currentExp,string idInventory) { 
        this.id = id;
        this.name = name;
        this.damage = damage;
        this.speed = speed;
        this.health = health;
        this.level = level;
        this.currentExp = currentExp;
        this.idInventory = idInventory;
    }
}
