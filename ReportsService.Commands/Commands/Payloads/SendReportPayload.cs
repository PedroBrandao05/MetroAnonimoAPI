namespace ReportsService.Commands.Commands.Payloads;

public record SendReportPayload
(
  string Title,
  
  string Description,
  
  LocationPayload Location
);