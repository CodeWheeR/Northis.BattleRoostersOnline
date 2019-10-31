using System.ServiceModel;
using System.Threading.Tasks;

namespace Northis.BattleRoostersOnline.Service.Contracts
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
		[OperationContract(IsInitiating = true, IsOneWay = true)]
		void FindMatchAsync(string token, string rooster);

		/// <summary>
		/// Контракт операции, отвечающий за отмену поиска матча.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <returns>true - в случае успешной отмены поиска, иначе - false.</returns>
		[OperationContract(IsTerminating = true)]
		Task<bool> CancelFinding(string token);

		/// <summary>
		/// Контракт операции, отвечающий за старт поединка петухов.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="matchToken">Токен матча.</param>
		/// <returns>Task.</returns>
		[OperationContract(IsInitiating = true, IsOneWay = true)]
		void StartBattleAsync(string token, string matchToken);

		/// <summary>
		/// Контракт операции, отвечающий за сдачу боя.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="matchToken">Токен матча.</param>
		/// <returns>Task.</returns>
		[OperationContract(IsTerminating = true, IsOneWay = true)]
		void GiveUpAsync(string token, string matchToken);
	}
}
