namespace Northis.BattleRoostersOnline.Service.DataStorages
{
	/// <summary>
	/// Предоставляет контракт сервиса хранения данных.
	/// </summary>
	public interface IDataStorageService : IUserDataStorageService, IRoosterStorageService, IMapperService
	{
	}
}
