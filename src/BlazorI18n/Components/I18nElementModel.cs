﻿using BlazorI18n.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Html;
using System;
using System.Threading.Tasks;

namespace BlazorI18n
{
    public class I18nElementModel : ComponentBase, IDisposable
    {
        [Inject]
        protected II18n I18n { get; set; }

        [Parameter]
        private string Key { get; set; }

        public HtmlString ConvertedValue { get; private set; }

        public I18nElementModel()
        {

        }

        protected override async Task OnInitAsync()
        {
            I18n.OnLocalUpdateOrChange += async () =>
            {
                if (!string.IsNullOrEmpty(Key))
                {
                    ConvertedValue = new HtmlString(await I18n.GetValue(Key));
                    await Invoke(StateHasChanged);
                }
            };

            await base.OnInitAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
        }

        protected override async Task OnAfterRenderAsync()
        {
            if (string.IsNullOrEmpty(Key))
            {
                throw new ArgumentNullException("Key cannot be empty or null.");
            }

            ConvertedValue = new HtmlString(await I18n.GetValue(Key));

            await base.OnAfterRenderAsync();
        }

        public void Dispose()
        {
            I18n.OnLocalUpdateOrChange -= StateHasChanged;
        }
    }
}
