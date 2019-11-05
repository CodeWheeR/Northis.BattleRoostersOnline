using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Northis.BattleRoostersOnline.Dto;
using Northis.BattleRoostersOnline.Service.Models;

namespace Northis.BattleRoostersOnline.Service.DataStorages
{
    /// <summary>
    /// Предоставляет контракт сервиса хранения данных.
    /// </summary>
    public interface IDataStorageService : IUserDataStorageService, IRoosterStorageService, IMapperService
	{

    }
}
