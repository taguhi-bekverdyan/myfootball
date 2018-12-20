using System;
using MyFootballMvc.ViewModels;

namespace MyFootballMvc.Models
{
    public class ErrorViewModel : LayoutViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}