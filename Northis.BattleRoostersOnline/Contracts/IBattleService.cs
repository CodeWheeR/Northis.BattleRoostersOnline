using System.ServiceModel;
using System.Threading.Tasks;
using DataTransferObjects;

namespace Northis.BattleRoostersOnline.Contracts
{
	/// <summary>
	/// Контракт сервиса, отвечающего за проведение битвы петухов.
	/// </summary>
	[ServiceContract(CallbackContract = typeof(IBattleServiceCallback), SessionMode = SessionMode.Required)]
	public interface IBattleService
	{
		/// <summary>
		/// Контракт операции, отвечающий за поиск матча.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="rooster">Петух.</param>
		/// <returns>Task.</returns>
		[OperationContract(IsInitiating = true)]
		Task FindMatchAsync(string token, RoosterDto rooster);

		/// <summary>
		/// Контракт операции, отвечающий за отмену поиска матча.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <returns>true - в случае успешной отмены поиска, иначе - false.</returns>
		[OperationContract(IsTerminating = true)]
		bool CancelFinding(string token);

		/// <summary>
		/// Контракт операции, отвечающий за старт поединка петухов.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="matchToken">Токен матча.</param>
		/// <returns>Task.</returns>
		[OperationContract(IsInitiating = true)]
		Task StartBattleAsync(string token, string matchToken);

		/// <summary>
		/// Контракт операции, отвечающий за сдачу боя.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="matchToken">Токен матча.</param>
		/// <returns>Task.</returns>
		[OperationContract(IsTerminating = true)]
		Task GiveUpAsync(string token, string matchToken);
	}
}
