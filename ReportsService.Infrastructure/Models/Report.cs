namespace ReportsService.Infrastructure.Models;

public class Report : Model<Entities.Report>
{
  public string UserCode { get; set; } = String.Empty;
  
  public string Title { get; set; } = String.Empty;
  
  public string Description { get; set; } = String.Empty;

  public Location Location { get; set; } = new Location();
  
  public List<string> Pictures { get; set; }
  
  public override Entities.Report ToEntity()
  {
    return Entities.Report.Create(Id)
      .Entitled(Title)
      .WithDescription(Description)
      .WithLocation(Location.ToEntity())
      .ReportedBy(UserCode)
      .WithPictures(Pictures);
  }

  public override Report FromEntity(Entities.Report entity)
  {
    var report = new Report
    {
      Location = new Location().FromEntity(entity.Location),
      
      Title = entity.Title,
      
      Description = entity.Description,
      
      UserCode = entity.UserCode,
      
      Pictures = entity.Pictures
    };

    return report;
  }
}