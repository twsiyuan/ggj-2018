using UnityEngine;

public interface IRoad
{	
	/// <summary>
	/// 取得 [0-1] 的實際位置
	/// </summary>
	/// <returns>The position.</returns>
	/// <param name="t">T.</param>
	Vector3 GetPosition(float t);

}