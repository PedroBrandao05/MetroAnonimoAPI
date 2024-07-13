using Common.Core;

namespace ReportsService.Entities;

public class Report : Entity
{
  public string UserCode { get; set; } = String.Empty;
  
  public string Title { get; set; } = String.Empty;
  
  public string Description { get; set; } = String.Empty;

  public Location Location { get; set; } = Location.Create();

  public List<string> Pictures { get; set; } = new List<string>();

  public static Report Create(Guid id)
  {
    var report =  new Report();

    report.Id = id;

    return report;
  }

  public Report ReportedBy(string userCode)
  {
    UserCode = userCode;

    return this;
  }

  public Report Entitled(string title)
  {
    Title = title;

    return this;
  }

  public Report WithDescription(string description)
  {
    Description = description;

    return this;
  }

  public Report WithLocation(Location location)
  {
    Location = location;

    return this;
  }

  public Report WithPictures(List<string> pictures)
  {
    Pictures = pictures;

    return this;
  }
}