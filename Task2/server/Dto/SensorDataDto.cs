namespace server;

public class SensorDataDto {
    public string Id { get; set; }
    public int SensorId { get; set; }
    public DateTime Created { get; set; }
    public decimal Value { get; set; }
}