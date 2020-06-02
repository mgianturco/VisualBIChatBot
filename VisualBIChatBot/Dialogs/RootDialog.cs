using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VisualBIChatBot.Common.Cards;
using VisualBIChatBot.Infraestructure.Luis;

namespace VisualBIChatBot.Dialogs
{
    public class RootDialog : ComponentDialog
    {
        private readonly ILuisService _luisService;

        public RootDialog(ILuisService luisService )
        {
            _luisService = luisService;
            var waterfallSteps = new WaterfallStep[]
            {
                InitialProcess,
                FinalProcess
            };
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
            AddDialog(new CreateAlertDialog( _luisService));
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> InitialProcess(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var luisResult = await _luisService._luisRecognizer.RecognizeAsync(stepContext.Context, cancellationToken);
            return await ManageIntentions(stepContext, luisResult, cancellationToken);
        }

        private async Task<DialogTurnResult> FinalProcess(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
        }

        private async Task<DialogTurnResult> ManageIntentions(WaterfallStepContext stepContext, RecognizerResult luisResult, CancellationToken cancellationToken)
        {
            var topIntent = luisResult.GetTopScoringIntent();
            switch (topIntent.intent)
            {

                case "Agradecer":
                    await IntentAgradecer(stepContext, luisResult, cancellationToken);
                    break;
                case "AltaAlerta":
                    return await IntentAltaAlerta(stepContext, luisResult, cancellationToken);
                case "AltaMails":
                    await IntentAltaMails(stepContext, luisResult, cancellationToken);
                    break;
                case "BajaAlerta":
                    await IntentBajaAlerta(stepContext, luisResult, cancellationToken);
                    break;
                case "BajaMails":
                    await IntentBajaMails(stepContext, luisResult, cancellationToken);
                    break;
                case "Despedir":
                    await IntentDespedir(stepContext, luisResult, cancellationToken);
                    break;
                case "Saludar":
                    await IntentSaludar(stepContext, luisResult, cancellationToken);
                    break;
                case "VerAlerta":
                    await IntentVerAlerta(stepContext, luisResult, cancellationToken);
                    break;
                case "VerMails":
                    await IntentVerMails(stepContext, luisResult, cancellationToken);
                    break;
                case "VerOpciones":
                    await IntentVerOpciones(stepContext, luisResult, cancellationToken);
                    break;
                case "None":
                    await IntentNone(stepContext, luisResult, cancellationToken);
                    break;
                default:
                    await IntentNone(stepContext, luisResult, cancellationToken);
                    break;
            }
            return await stepContext.NextAsync(null, cancellationToken);
        }

        private async Task IntentAgradecer(WaterfallStepContext stepContext, RecognizerResult luisResult, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivityAsync($"Gracias, estamos para ayudarte", cancellationToken: cancellationToken);
        }

        private async Task<DialogTurnResult> IntentAltaAlerta(WaterfallStepContext stepContext, RecognizerResult luisResult, CancellationToken cancellationToken)
        {
            return await stepContext.BeginDialogAsync(nameof(CreateAlertDialog), null, cancellationToken);
        }

        private async Task IntentAltaMails(WaterfallStepContext stepContext, RecognizerResult luisResult, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivityAsync($"IntentAltaMails", cancellationToken: cancellationToken);
        }

        private async Task IntentBajaAlerta(WaterfallStepContext stepContext, RecognizerResult luisResult, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivityAsync($"IntentBajaAlerta", cancellationToken: cancellationToken);
        }

        private async Task IntentBajaMails(WaterfallStepContext stepContext, RecognizerResult luisResult, CancellationToken cancellationToken)
        { 
            await stepContext.Context.SendActivityAsync($"IntentBajaMails", cancellationToken: cancellationToken);
        }

        private async Task IntentVerAlerta(WaterfallStepContext stepContext, RecognizerResult luisResult, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivityAsync($"IntentVerAlerta", cancellationToken: cancellationToken);
        }

        private async Task IntentVerMails(WaterfallStepContext stepContext, RecognizerResult luisResult, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivityAsync($"IntentVerMails", cancellationToken: cancellationToken);
        }

        private async Task IntentVerOpciones(WaterfallStepContext stepContext, RecognizerResult luisResult, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivityAsync($"Puedo hacerte llegar alertas o notificaciones", cancellationToken: cancellationToken);
            await Task.Delay(800);
            await stepContext.Context.SendActivityAsync("Estas serian las gestiones posibles", cancellationToken: cancellationToken);
            await AlertOptionsCard.ToShow(stepContext, cancellationToken);
        }

        private async Task IntentSaludar(WaterfallStepContext stepContext, RecognizerResult luisResult, CancellationToken cancellationToken)
        {
            var userName = stepContext.Context.Activity.From.Name;
            await stepContext.Context.SendActivityAsync($"Hola {userName}, ¿cómo te puedo ayudar?", cancellationToken: cancellationToken);
        }

        private async Task IntentDespedir(WaterfallStepContext stepContext, RecognizerResult luisResult, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivityAsync($"Espero verte pronto.", cancellationToken: cancellationToken);
        }

        private Task IntentNone(WaterfallStepContext stepContext, RecognizerResult luisResult, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
