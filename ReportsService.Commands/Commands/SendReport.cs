using AnonymityService.Middleware.Security.Models;
using MediatR;
using ReportsService.Commands.Commands.Payloads;
using ReportsService.Commands.Core;

namespace ReportsService.Commands.Commands;

public class SendReport : Command<SendReportPayload>, IRequest<string>
{
  public SendReport(SendReportPayload payload, SessionJwtPayload session) : base(payload, session)
  {
  }
}