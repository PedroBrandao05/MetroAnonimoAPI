namespace Infrastructure.FileManager.Contracts;

public interface IFileManager
{
  public Task<string> Save(string base64, string filename);
}