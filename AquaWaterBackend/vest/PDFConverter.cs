using iText.Html2pdf;
using System;
using System.Collections.Generic;
using System.IO;
using VestEngine.Application.Dtos.Response;
using VestEngine.Application.Services.Interfaces;

namespace VestEngine.Application.Services.Implementation
{
	public class PDFConverterService : IPDFConverterService
	{
		private IMailService _mailService;

		public PDFConverterService(IMailService mailService)
		{
			_mailService = mailService;
		}

		public void ConverHTMLToPDF(List<TransactionResponseDto> transactions)
		{
			var baseDir = Directory.GetCurrentDirectory();
			string folderName = "/StaticFiles/";
			var path = Path.Combine(baseDir + folderName, string.Concat("GeneratedReport",Guid.NewGuid(),".Pdf"));
			var data = "<tr><th scope=\"row\"></th><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>\r";
			var template = _mailService.GetEmailTemplate("GeneratedReport.html");
			string newData = null;
			int count = 1;
			for(int i = 0; i < transactions.Count; i++)
			{
				newData += $"<tr><th scope=\"row\" class=\"h6\">{count}</th><td class=\"h6\">{transactions[i].Reference}</td><td class=\"h6\">{transactions[i].AmountDebit}</td>" +
				$"<td class=\"h6\">{transactions[i].AmountCredit}</td><td class=\"h6\">{transactions[i].Balance}</td><td class=\"h6\">{transactions[i].Narration}</td>" +
				$"<td class=\"h6\">{transactions[i].ValueDate}</td><td class=\"h6\">{transactions[i].AccountName}</td><td class=\"h6\">{transactions[i].AccountName}</td>";
				if (transactions[i].AmountDebit > 0) newData += $"<td class=\"h6\">Withdrawal</td></tr>";
				else newData += $"<td class=\"h6\">Deposit</td></tr>";
				count++;
			}
			var html = template.Replace(data, newData);
			HtmlConverter.ConvertToPdf(html, new FileStream(path, FileMode.Create, FileAccess.Write));
		}
	}
}
