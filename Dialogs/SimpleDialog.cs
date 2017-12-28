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
           
            var activity = await result as Activity; //result 是從simulator傳過來的訊息
            if (activity.Text.StartsWith("hi"))
            {
                await context.PostAsync($"嗨~你好 我叫野源新之助 今年五歲 喜歡吃恐龍餅乾 最喜歡娜娜子姐姐");//context為連接simulator和這個bot的管道 PostAsync則是傳回去的函式
                context.Wait(Step2);//將管道接到Step2函式下一次則變促發Step2而不是MessageReceivedAsync
            }
            else {
                await context.PostAsync($"幹");//context為連接simulator和這個bot的管道 PostAsync則是傳回去的函式
                context.Wait(Step2);//將管道接到Step2函式下一次則變促發Step2而不是MessageReceivedAsync
            }
            
        }
        private async Task Step2(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            if (activity.Text.StartsWith("所以")) {
                await context.PostAsync($"我想認識你啦大姊姊~");
                context.Wait(Step3);
            }
            else {
                await context.PostAsync($"你素水");
                context.Wait(Step3);
            }
           
        }
        private async Task Step3(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            await context.PostAsync("掰掰~");

            context.Wait(MessageReceivedAsync);
        }
    }
}