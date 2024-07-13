namespace Common.Errors;

public class ApplicationError : Exception
{
  public string Code { get; set; }
  
  public int HttpCode { get; set; }

  public ApplicationError (int httpCode, string code, string? message) : base(message)
  {
    HttpCode = httpCode;
    Code = code;
  }
}

public class InternalServerError : ApplicationError
{
  public InternalServerError (string? message = "Internal server error") : base(500, "INTERNAL_SERVER_ERROR", message)
  {
  }
}

public class NotFoundError : ApplicationError
{
  public NotFoundError (string? message = "Not found") : base(404, "NOT_FOUND", message)
  {
  }
}

public class MandatoryAttributesError : ApplicationError
{
  public MandatoryAttributesError (string? message = "Mandatory attributes not provided") : base(400, "MANDATORY_ATTRIBUTES", message)
  {
  }
}

public class ForbiddenError : ApplicationError
{
  public ForbiddenError (string? message = "Forbidden Error") : base(403, "FORBIDDEN_ERROR", message)
  {
  }
}