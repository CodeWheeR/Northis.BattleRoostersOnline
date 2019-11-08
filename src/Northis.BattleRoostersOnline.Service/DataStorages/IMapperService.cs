using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Northis.BattleRoostersOnline.Service.DataStorages
{
	/// <summary>
	/// Предоставляет доступ к объекту AutoMapper.
	/// </summary>
	public interface IMapperService
	{
		/// <summary>
		/// Возвращает или задает данные об игровых сессиях.
		/// </summary>
		/// <value>
		/// Игровые сессии.
		/// </value>
		IMapper Mapper { get; }
	}
}
