namespace api.Services;

using api.Models;
public interface IRecordCollectionService
{
    Task<IEnumerable<RecordCollection>> GetAllAsync();
    Task<RecordCollection?> GetByIdAsync(int id);
    Task<RecordCollection> CreateAsync(RecordCollection record);
    Task<bool> UpdateAsync(int id, RecordCollection record);
    Task<bool> DeleteAsync(int id);
}