namespace Scripts.Interfaces
{
    public enum MovementType
    {
        Walking,
        Flying
    }

    public interface IActorSettings
    {
        float MoveSpeed { get; set; }
        int Health { get; set; }
        float TurningSpeed { get; set; }
        MovementType MovementType { get; set; }
    }
}