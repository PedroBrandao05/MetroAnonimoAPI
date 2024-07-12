namespace ReportsService.Entities;

public class Location
{
  public string Station { get; set; } = String.Empty;
  
  public string Line { get; set; } = String.Empty;
  
  public static Location Create()
  {
    return new Location();
  }

  public Location OnStation(string station)
  {
    Station = station;

    return this;
  }

  public Location OnLine(string line)
  {
    Line = line;

    return this;
  }
}