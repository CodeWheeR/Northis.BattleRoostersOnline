using System.ServiceModel;

namespace Northis.BattleRoostersOnline.Contracts
{
	/// <summary>
	/// Контракт сервиса, ответственного за оповещения.
	/// </summary>
	internal interface INotifyCallbackContract
	{
		/// <summary>
		/// Контракт операции, ответственной за оповещение о начале матча.
		/// </summary>
		[OperationContract]
		void MatchFinded();
	}
}
