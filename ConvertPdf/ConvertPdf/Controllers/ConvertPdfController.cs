using ConvertPdf.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

#pragma warning disable CS0168
namespace ConvertPdf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvertPdfController : ControllerBase
    {
        private readonly IConvertPdf _convertPdf;

        public ConvertPdfController(IConvertPdf convertPdf)
        {
            _convertPdf = convertPdf;
        }

        [HttpGet("convert")]
        public void ConvertPdf()
        {
            _convertPdf.Convert();
        }

        [HttpGet("convert2")]
        public void Convert2()
        {
            _convertPdf.Convert2();
        }

        [HttpGet("convert3")]
        public void Convert3()
        {
            _convertPdf.Convert3();
        }

        [HttpGet("convert4")]
        public void Convert4()
        {
            _convertPdf.Convert4();
        }
    }
}
