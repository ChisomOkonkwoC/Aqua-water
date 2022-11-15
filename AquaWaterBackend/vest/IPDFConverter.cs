using System.Collections.Generic;
using VestEngine.Application.Dtos.Response;

namespace VestEngine.Application.Services.Interfaces
{
	public interface IPDFConverterService
	{
		void ConverHTMLToPDF(List<TransactionResponseDto> transactions);
	}
}