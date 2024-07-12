namespace ReportsService.Infrastructure.Models;

public class Location : Model<Entities.Location>
{
  public string Station { get; set; } = String.Empty;

  public string Line { get; set; } = String.Empty;
  
  public override Entities.Location ToEntity()
  {
    return Entities.Location.Create()
      .OnStation(Station)
      .OnLine(Line);
  }

  public override Location FromEntity(Entities.Location entity)
  {
    var location = new Location();

    location.Line = entity.Line;
    location.Station = entity.Station;

    return location;
  }
}