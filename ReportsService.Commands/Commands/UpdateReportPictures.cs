using AnonymityService.Middleware.Security.Models;
using MediatR;
using ReportsService.Commands.Commands.Payloads;
using ReportsService.Commands.Core;

namespace ReportsService.Commands.Commands;

public class UpdateReportPictures : Command<PicturesPayload, Guid>, IRequest
{
  public UpdateReportPictures(PicturesPayload payload, SessionJwtPayload session, Guid entityId) : base(payload, session, entityId)
  {
  }
}