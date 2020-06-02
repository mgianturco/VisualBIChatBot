using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace VisualBIChatBot.Common.Cards
{
    public class AlertOptionsCard
    {
        public static async Task ToShow(DialogContext dc, CancellationToken cancellationToken)
        {
            await dc.Context.SendActivityAsync(activity: CreateCarousel(), cancellationToken);
        }
        private static Activity CreateCarousel()
        {
            var cardEntregas = new HeroCard
            {
                Title = "Entregas",
                Subtitle = "Opciones",
                Images = new List<CardImage> { new CardImage("https://visualbistorage.blob.core.windows.net/images/entregas.png") },
                Buttons = new List<CardAction>()
                {
                    new CardAction(){Title = "Suscribir Alerta", Value = "Suscribir Alerta Entregas", Type = ActionTypes.ImBack},
                    new CardAction(){Title = "Recibir por Mail", Value = "Suscribir Mail Entregas", Type = ActionTypes.ImBack}
                }
            };
            var cardIngresos = new HeroCard
            {
                Title = "Ingresos",
                Subtitle = "Opciones",
                Images = new List<CardImage> { new CardImage("https://visualbistorage.blob.core.windows.net/images/compras.png") },
                Buttons = new List<CardAction>()
                {
                    new CardAction(){Title = "Suscribir Alerta", Value = "Suscribir Alerta Ingresos", Type = ActionTypes.ImBack},
                    new CardAction(){Title = "Recibir por Mail", Value = "Suscribir Mail Ingresos", Type = ActionTypes.ImBack}
                }
            };
            var cardInventario = new HeroCard
            {
                Title = "Inventario",
                Subtitle = "Opciones",
                Images = new List<CardImage> { new CardImage("https://visualbistorage.blob.core.windows.net/images/stock.png") },
                Buttons = new List<CardAction>()
                {
                    new CardAction(){Title = "Suscribir Alerta", Value = "Suscribir Alerta Inventario", Type = ActionTypes.ImBack},
                    new CardAction(){Title = "Recibir por Mail", Value = "Suscribir Mail Inventario", Type = ActionTypes.ImBack}
                }
            };
            var cardPresupuesto = new HeroCard
            {
                Title = "Presupuesto",
                Subtitle = "Opciones",
                Images = new List<CardImage> { new CardImage("https://visualbistorage.blob.core.windows.net/images/presupuesto.png") },
                Buttons = new List<CardAction>()
                {
                    new CardAction(){Title = "Suscribir Alerta", Value = "Suscribir Alerta Presupuesto", Type = ActionTypes.ImBack},
                    new CardAction(){Title = "Recibir por Mail", Value = "Suscribir Mail Presupuesto", Type = ActionTypes.ImBack}
                }
            };
            var cardVentas = new HeroCard
            {
                Title = "Ventas",
                Subtitle = "Opciones",
                Images = new List<CardImage> { new CardImage("https://visualbistorage.blob.core.windows.net/images/venta.png") },
                Buttons = new List<CardAction>()
                {
                    new CardAction(){Title = "Suscribir Alerta", Value = "Suscribir Alerta Ventas", Type = ActionTypes.ImBack},
                    new CardAction(){Title = "Recibir por Mail", Value = "Suscribir Mail Ventas", Type = ActionTypes.ImBack}
                }
            };

            var optionsAttachments = new List<Attachment>()
            {
                cardEntregas.ToAttachment(),
                cardIngresos.ToAttachment(),
                cardInventario.ToAttachment(),
                cardPresupuesto.ToAttachment(),
                cardVentas.ToAttachment()
            };

            var reply = MessageFactory.Attachment(optionsAttachments);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            return reply as Activity;
        }
    }
}
