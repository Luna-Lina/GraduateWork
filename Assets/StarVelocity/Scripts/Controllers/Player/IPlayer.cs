namespace StarVelocity.Controllers
{
    public interface IPlayer
    {
        void MakeDamage();
        void IncreaseSpeed(float speedIncreaseAmount);
        void CurrentScore();
    }
}