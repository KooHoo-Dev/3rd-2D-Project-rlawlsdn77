namespace SHIFTER
{
    public interface ITakeDamage
    {
        float Hp { get; set; }
        void TakeDamage(float damage);
    }
}