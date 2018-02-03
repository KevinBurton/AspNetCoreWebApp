namespace AspNetCoreWebApp.Models
{
  public class Crime<T>
  {
    public T Id {get; set;}
    public string Name { get; set; }
    public string Description { get; set; }
    public string Population { get; set; }
    public double ViolentCrime { get; set; }
    public double Murder { get; set; }
    public double Rape { get; set; }
    public double Robbery { get; set; }
    public double AggravatedAssault { get; set; }
    public double Property { get; set; }
    public double Burglary { get; set; }
    public double Larceny { get; set; }
    public double MotorVehicle { get; set; }
  }
}
