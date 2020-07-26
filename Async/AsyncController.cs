using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Netology.Test.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AsyncController : ControllerBase
	{

		[HttpGet("ex1")]
		public async Task<IActionResult> AsyncExample1()
		{
			var firstResult = await CalcCircumferenceAsync(3);

			var secondResult = CalcCircumferenceAsync(7).Result;

			return Ok(new
			{
				First = firstResult,
				Second = secondResult
			});
		}

		private async Task<double> CalcCircumferenceAsync(double radius)
		{
			var result = 2 * Math.PI * radius;

			await Task.Delay(1000);

			return await Task.FromResult(result);
		}


		[HttpGet("ex2")]
		public async Task<IActionResult> AsyncExample2()
		{
			await DoAsyncWork(1000);

			DoAsyncWork(2000).Wait();

			return Ok();
		}

		private async Task DoAsyncWork(int delay)
		{
			await Task.Delay(delay);
		}


		[HttpGet("ex3")]
		public async Task<IActionResult> AsyncExample3()
		{
			var timer = new Stopwatch();
			timer.Start();
			await DoAsyncWork(1000);
			await DoAsyncWork(2000);
			await DoAsyncWork(3000);
			var nonParallelTime = timer.Elapsed;

			timer.Restart();
			var task1 = DoAsyncWork(2000);
			var task2 = DoAsyncWork(1000);
			var task3 = DoAsyncWork(3000);
			await Task.WhenAll(task1, task2, task3);
			var parallelTime = timer.Elapsed;
			timer.Stop();


			return Ok(new
			{
				NonParallelTime = nonParallelTime.ToString(),
				ParallelTime = parallelTime.ToString()
			});
		}
	}
}
