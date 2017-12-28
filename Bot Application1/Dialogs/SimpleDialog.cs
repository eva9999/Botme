using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Bot_Application1.Dialogs
{
    [Serializable]
    public class SimpleDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            //result 是從simulator傳過來的訊息
            var activity = await result as Activity;
            //context為連接simulator和這個bot的管道 PostAsync則是傳回去的函式
            
            //將管道接到Step2函式下一次則變促發Step2而不是MessageReceivedAsync
            context.Wait(Step2);
            if (activity.Text.StartsWith("hi")) {
                await context.PostAsync($"嗨~你好 請問你是誰");
            }
            else if(activity.Text.StartsWith("我摸摸你"))
            {
                await context.PostAsync($"ban 太");
            }
        }
        private async Task Step2(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            await context.PostAsync(activity.Text+$"妳好漂亮 ");

            context.Wait(Step3);
        }
        private async Task Step3(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            await context.PostAsync("帶你回家");

            context.Wait(MessageReceivedAsync);
        }
    }
}