using Common.Errors;
using Infrastructure.FileManager.Contracts;
using MediatR;
using ReportsService.Entities;
using ReportsService.Infrastructure.Repositories.Contracts;

namespace ReportsService.Commands.Commands;

public class ReportsCommandHandler : 
  IRequestHandler<SendReport, string>,
  IRequestHandler<UpdateReportPictures>
{
  private IReportRepository _repository { get; }
  
  private IFileManager _fileManager { get; }

  public ReportsCommandHandler(IReportRepository repository, IFileManager fileManager)
  {
    _repository = repository;

    _fileManager = fileManager;
  }
  
  public async Task<string> Handle(SendReport command, CancellationToken cancellationToken)
  {
    var report = new Report()
      .WithLocation(
        Location.Create()
          .OnLine(command.Payload.Location.Line)
          .OnStation(command.Payload.Location.Station))
      .WithDescription(command.Payload.Description)
      .Entitled(command.Payload.Title)
      .ReportedBy(command.Session.UserCode);

    await _repository.Save(report);

    return report.Id.ToString();
  }

  public async Task Handle(UpdateReportPictures command, CancellationToken cancellationToken)
  {
    var report = await _repository.FindOne(command.EntityId);

    if (report is null)
    {
      throw new NotFoundError();
    }

    var pictures = new List<string>();

    foreach (var base64 in command.Payload.Pictures)
    {
      var url = await _fileManager.Save(base64, $"{report.Id}-{command.Payload.Pictures.FindIndex(p => p == base64)}");
      
      pictures.Add(url);
    }

    report.WithPictures(pictures);

    await _repository.Update(report);
  }
}