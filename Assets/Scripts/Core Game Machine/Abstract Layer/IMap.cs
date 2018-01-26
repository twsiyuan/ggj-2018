using System.Collections;
using System.Collections.Generic;

public interface IMap
{	

    /// <summary>
    /// 新增車站
    /// </summary>
    void AddStation(IStation station);

    /// <summary>
    /// 取得所有車站
    /// </summary>
    List<IStation> GetAllStations();

}