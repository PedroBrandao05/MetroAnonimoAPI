using AnonymityService.Middleware.Security.Models;

namespace ReportsService.Commands.Core;

public class Command<TPayload>
{
  public TPayload Payload { get; set; }
  
  public SessionJwtPayload Session { get; set; }
  
  public Command (TPayload payload, SessionJwtPayload session)
  {
    Payload = payload;

    Session = session;
  }
}

public class Command<TPayload, TEntityId>
{
  public TPayload Payload { get; set; }
  
  public SessionJwtPayload Session { get; set; }

  public TEntityId EntityId { get; set; } = default;
  
  public Command (TPayload payload, SessionJwtPayload session, TEntityId entityId)
  {
    Payload = payload;

    Session = session;

    EntityId = entityId;
  }
}