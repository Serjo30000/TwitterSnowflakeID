using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication15.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HomeController : Controller
	{
		private uint a = 0b_0000_0000_0000;
		private void UpdateId(Object obj)
		{
			a = 0b_0000_0000_0000;
		}
		/// <summary>
		/// Получить UUID
		/// </summary>
		/// <response code="200">Успешное выполнение</response>
		[HttpGet]
		public IActionResult GetUuid()
		{
			Guid u = Guid.NewGuid();
			return Ok(u);
		}


		/// <summary>
		/// Отправить Twitter snowflake ID
		/// </summary>
		/// <response code="200">Успешное выполнение</response>
		/// <response code="400">Плохой запрос</response>
		[HttpPost]
		public IActionResult PostTwitterSnowflakeId(string idDT, string idComputer)
		{
			bool er = false;
			for (int i = 0; i < 5; i++)
			{
				if (idDT == null || idComputer == null || idDT.Length!=5 || idComputer.Length!=5) break;
				if ((idDT.ToCharArray()[i] == '0' || idDT.ToCharArray()[i] == '1') && (idComputer.ToCharArray()[i] == '0' || idComputer.ToCharArray()[i] == '1'))
				{
					
				}
                else
                {
					er = true;
				}
			}
			if (idDT == null || idComputer == null || er==true)
			{
				return BadRequest(ModelState);
			}
			string strIdTwitterSnowflake = "0";
			strIdTwitterSnowflake += Convert.ToString(((long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalMilliseconds-(long)new DateTime(1970, 1, 1, 0, 0, 0).Subtract(DateTime.UnixEpoch).TotalMilliseconds), 2);
			//01010
			strIdTwitterSnowflake += idDT;
			//01100
			strIdTwitterSnowflake += idComputer;
			for (int i = 0; i < 12 - Convert.ToString(a, toBase: 2).Length; i++)
            {
				strIdTwitterSnowflake += "0";

			}
			strIdTwitterSnowflake += Convert.ToString(a, toBase: 2);
			a = a + 1;
			TimerCallback tm = new TimerCallback(UpdateId);
			Timer timer = new Timer(tm,null, 0, 1);
			return Ok(strIdTwitterSnowflake);
		}
	}
}
