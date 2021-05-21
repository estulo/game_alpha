public interface IEnemy {
    float currentHealth {get; set;}
    float maxHealth {get;}
    void receiveDamage(float damageReceived);
}