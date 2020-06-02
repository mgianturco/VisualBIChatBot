using VisualBIChatBot.Common.Models;
using VisualBIChatBot.Infraestructure.Luis;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace VisualBIChatBot.Dialogs
{
    public class CreateAlertDialog: ComponentDialog
    {
        static string userText;
        private readonly ILuisService _luisService;

        public CreateAlertDialog( ILuisService luisService)
        {
            _luisService = luisService;
            var waterfallSteps = new WaterfallStep[]
            {
                Process
            };
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
            AddDialog(new TextPrompt(nameof(TextPrompt)));
        }

        private async Task<DialogTurnResult> Process(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            userText = stepContext.Context.Activity.Text;

            var newStepContext = stepContext;
            newStepContext.Context.Activity.Text = userText;

            var luisResult = await _luisService._luisRecognizer.RecognizeAsync(newStepContext.Context, cancellationToken);
            var Entities = luisResult.Entities.ToObject<EntityLuis>();

            if (Entities.Instance.Gestion?.Count > 0)
            {
                // Capturar la entidad
                var entityCapture = Entities.Instance.Gestion.FirstOrDefault();
                await stepContext.Context.SendActivityAsync(entityCapture.Text, cancellationToken: cancellationToken);
            }

            return await stepContext.ContinueDialogAsync(cancellationToken: cancellationToken);
        }
    }
}
